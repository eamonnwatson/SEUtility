using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using System.Xml;

namespace SEUtility.Parser.Parsers;

[Parser("BlueprintClass")]
internal class BlueprintClassParser : BaseParser, IParser
{
    public BlueprintClassParser(ILogger<BlueprintClassParser> logger, IMediator mediator) : base(logger, mediator)
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

        var nodeCount = 0;

        foreach (XmlNode item in node.ChildNodes)
        {
            if (item.NodeType == XmlNodeType.Comment)
                continue;

            logger.LogDebug("Node : {Node}", item.OuterXml);

            dataBuilder.AddBluePrintClass(ItemType.BLUEPRINTCLASS,
                                          GetTypeID(item),
                                          GetSubTypeID(item),
                                          GetDisplayName(item),
                                          file,
                                          item.SelectSingleNode("Description")?.InnerText ?? string.Empty);

            logger.LogDebug("Added {item}", GetDisplayName(item));
            nodeCount++;
        }

    }

}
