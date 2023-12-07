using FluentResults;
using Microsoft.Extensions.Logging;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser;
public class SbcParser<TObject>(ILogger<SbcParser<TObject>> logger) : ISbcParser<TObject> where TObject : IModel
{
    private readonly ILogger<SbcParser<TObject>> _logger = logger;

    public async Task<Result<IEnumerable<TObject>>> ParseAsync(string filename, CancellationToken cancellationToken = default)
    {
        var rootElement = GetRootElement(typeof(TObject));

        try
        {
            await using var fs = new FileStream(filename, FileMode.Open);
            var doc = await XDocument.LoadAsync(fs, LoadOptions.None, cancellationToken);
            var elements = doc.Descendants(rootElement).ToList();
            var objects = elements.Select(CreateObject<TObject>).ToList();

            objects.ForEach(o => o.FileName = filename);

            _logger.LogDebug("{number} Items found...", objects.Count);

            return Result.Ok(objects.AsEnumerable());
        }
        catch (FileNotFoundException ex)
        {
            return Result.Fail(ParserError.FileNotFouind.CausedBy(ex));
        }
        catch (IOException ex)
        {
            return Result.Fail(ParserError.ParsingError.CausedBy(ex));
        }

    }

    private static T CreateObject<T>(XElement element) where T : IModel
        => (T)CreateObject(typeof(T), element);

    private static object CreateObject(Type type, XElement element)
    {
        var outputObject = Activator.CreateInstance(type) ?? throw new Exception();
        var properties = outputObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var modelProperties = properties.Select(p => CreateModelProperty(p, outputObject)).ToList();

        foreach (var property in modelProperties)
        {
            var propertyElement = element.Descendants(property.PropertyName).FirstOrDefault();

            object? paramValue;

            switch (property.PropertyXMLType)
            {
                case ModelPropertyType.Array:
                    SetArrayValue(property, propertyElement);
                    continue;
                case ModelPropertyType.Attribute:
                    if (property.PropertyName is null)
                        continue;

                    paramValue = element.Attribute(property.PropertyName)?.Value;

                    if (paramValue == null)
                        continue;

                    paramValue = GetValue(property.PropertyType!, (string)paramValue);

                    break;
                case ModelPropertyType.Root:
                    paramValue = GetValue(property.PropertyType!, element.Value);
                    break;
                case ModelPropertyType.Element:
                default:
                    if (propertyElement is null)
                        continue;

                    if (property.IsIModel)
                        paramValue = CreateObject(property.PropertyType!, propertyElement);
                    else
                        paramValue = GetValue(property.PropertyType!, propertyElement.Value);
                    break;
            }

            property.SetValue!(outputObject, paramValue);

        }

        return outputObject;
    }

    private static ModelProperty CreateModelProperty(PropertyInfo propertyInfo, object? obj)
    {
        var model = new ModelProperty
        {
            PropertyInfo = propertyInfo,
            PropertyName = propertyInfo.Name,
            PropertyType = propertyInfo.PropertyType,
            PropertyXMLType = ModelPropertyType.Element,
            SetValue = propertyInfo.SetValue,
            IsIModel = propertyInfo.PropertyType.GetInterface(nameof(IModel)) != null
        };

        if (propertyInfo.GetCustomAttribute<XmlArrayAttribute>() is XmlArrayAttribute arrayAttribute)
        {
            model.PropertyXMLType = ModelPropertyType.Array;
            model.ArrayType = propertyInfo.PropertyType.GenericTypeArguments[0];
            model.SetArrayValue = propertyInfo.PropertyType.GetMethod("Add");
            model.ArrayInstance = propertyInfo.GetValue(obj);
            if (!string.IsNullOrEmpty(arrayAttribute.ElementName))
                model.PropertyName = arrayAttribute.ElementName;
        }

        if (propertyInfo.GetCustomAttribute<XmlAttributeAttribute>() is XmlAttributeAttribute attributeAttribute)
        {
            model.PropertyXMLType = ModelPropertyType.Attribute;
            if (!string.IsNullOrEmpty(attributeAttribute.AttributeName))
            {
                if (!string.IsNullOrEmpty(attributeAttribute.Namespace))
                    model.PropertyName = new XElement((XNamespace)attributeAttribute.Namespace + attributeAttribute.AttributeName).Name;
                else
                    model.PropertyName = attributeAttribute.AttributeName;
            }
        }

        if (propertyInfo.GetCustomAttribute<XmlElementAttribute>() is XmlElementAttribute elementAttribute)
        {
            if (!string.IsNullOrEmpty(elementAttribute.ElementName))
                model.PropertyName = elementAttribute.ElementName;
        }

        if (propertyInfo.GetCustomAttribute<XmlTextAttribute>() is not null)
        {
            model.PropertyXMLType = ModelPropertyType.Root;
        }

        return model;
    }

    private static void SetArrayValue(ModelProperty property, XElement? propertyElement)
    {
        if (propertyElement is null)
            return;

        var items = propertyElement.Elements().Select(e => CreateObject(property.ArrayType!, e)).ToList();
        foreach (var item in items)
        {
            property.SetArrayValue!.Invoke(property.ArrayInstance, new[] { item });
        }
    }

    private static object GetValue(Type type, string value) => type switch
    {
        Type _ when type == typeof(int) => int.Parse(value),
        Type _ when type == typeof(decimal) => decimal.Parse(value),
        Type _ when type == typeof(bool) => bool.Parse(value),
        _ => value
    };

    private static string GetRootElement(Type type)
    {
        var xmlAttribute = type.GetCustomAttributes(false).FirstOrDefault(c => c is XmlRootAttribute) as XmlRootAttribute;
        return xmlAttribute?.ElementName ?? type.Name;
    }

}
