using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using System.Xml;

namespace SEUtility.Parser.Parsers;

[Parser("BlockCategory")]
internal class BlockCategoryParser : BaseParser, IParser
{
    public BlockCategoryParser(ILogger<BlockCategoryParser> logger, IMediator mediator) : base(logger, mediator)
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

            var itemIDs = item.SelectSingleNode("ItemIds")?
                .ChildNodes
                .Cast<XmlNode>()
                .Where(a => a.NodeType != XmlNodeType.Comment)
                .Select(a => a.InnerText)
                .ToList() ?? new List<string>();


            dataBuilder.AddBlockCategory(ItemType.BLOCKCATEGORY,
                                         GetTypeID(item),
                                         GetSubTypeID(item),
                                         GetDisplayName(item),
                                         file,
                                         item.SelectSingleNode("Name")?.InnerText ?? string.Empty,
                                         itemIDs);

            logger.LogDebug("Added {item}", GetDisplayName(item));
            nodeCount++;
        }

        OutputInfo($"{nodeCount} Items Added");

    }
}
