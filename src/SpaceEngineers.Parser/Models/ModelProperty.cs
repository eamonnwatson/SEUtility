using System.Reflection;
using System.Xml.Linq;

namespace SpaceEngineers.Parser.Models;
internal class ModelProperty
{
    public PropertyInfo? PropertyInfo { get; set; }
    public XName? PropertyName { get; set; }
    public ModelPropertyType PropertyXMLType { get; set; }
    public XElement? XMLElement { get; set; }
    public Type? PropertyType { get; set; }
    public Type? ArrayType { get; set; }
    public Action<object?, object?>? SetValue { get; set; }
    public MethodInfo? SetArrayValue { get; set; }
    public object? ArrayInstance { get; set; }
    public bool IsIModel { get; set; }

}
