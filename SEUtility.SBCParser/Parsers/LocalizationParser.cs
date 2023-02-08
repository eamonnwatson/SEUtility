using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using System.Xml;

namespace SEUtility.Parser.Parsers;

[Parser("Localization")]
internal class LocalizationParser : BaseParser, IParser
{
    public LocalizationParser(ILogger<LocalizationParser> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    public void Parse(string file, IDataBuilder dataBuilder, ParserType options)
    {
        var node = GetXmlNode(file, options.XmlNode);

        if (node is null)
        {
            OutputWarning("XMLNode {0} Not Found..", options.XmlNode);
            return;
        }

        var nodes = node.SelectNodes("data")?.Cast<XmlNode>().Where(a => a.NodeType == XmlNodeType.Element) ?? Enumerable.Empty<XmlNode>();

        if (!nodes.Any())
            return;

        var nodeCount = 0;

        foreach (var item in nodes)
        {
            if (item.Attributes?["name"] is not null)
            {
                dataBuilder.AddLocalization(item.Attributes["name"]?.Value!, item.SelectSingleNode("value")?.InnerText ?? String.Empty);
                nodeCount++;
            }
        }

    }
}
