using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Notifications;
using System.Xml;

namespace SEUtility.Parser.Parsers;

internal abstract class BaseParser
{
    protected const string TYPEID_XPATH = "Id//TypeId";
    protected const string SUBTYPEID_XPATH = "Id//SubtypeId";
    protected const string DISPLAYNAME_XPATH = "DisplayName";

    protected readonly ILogger logger;
    private readonly IMediator mediator;
    public BaseParser(ILogger logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }
    protected static XmlNode? GetXmlNode(string filename, string nodeName)
    {
        var xml = new XmlDocument();
        xml.Load(filename);
        return xml.SelectSingleNode($"//{nodeName}");
    }
    protected static string GetTypeID(XmlNode item)
    {
        return item.SelectSingleNode(TYPEID_XPATH)?.InnerText ?? string.Empty;
    }
    protected static string GetSubTypeID(XmlNode item)
    {
        return item.SelectSingleNode(SUBTYPEID_XPATH)?.InnerText ?? string.Empty;
    }
    protected static string GetDisplayName(XmlNode item)
    {
        return item.SelectSingleNode(DISPLAYNAME_XPATH)?.InnerText ?? string.Empty;
    }

    protected void OutputWarning(string message, params object?[] args)
    {
        logger.LogWarning(message, args);
        mediator.Publish(new ConsoleNotification(string.Format(message, args), ConsoleStatus.WARNING));
    }
    protected void OutputInfo(string message, params object?[] args)
    {
        mediator.Publish(new ConsoleNotification(string.Format(message, args)));
    }
}
