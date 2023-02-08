using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using System.Xml;

namespace SEUtility.Parser.Parsers;

[Parser("Blueprint")]
internal class BlueprintsParser : BaseParser, IParser
{
    private const string PRODTIME_XPATH = "BaseProductionTimeInSeconds";

    public BlueprintsParser(ILogger<BlueprintsParser> logger, IMediator mediator) : base(logger, mediator)
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


            var pre = item.SelectSingleNode("Prerequisites")?.ChildNodes.Cast<XmlNode>()
                .Select(a => new BlueprintItem(a.Attributes?["TypeId"]?.Value ?? string.Empty, a.Attributes?["SubtypeId"]?.Value ?? string.Empty, Convert.ToDecimal(a.Attributes?["Amount"]?.Value)))
                .ToList();

            var res = item.SelectSingleNode("Results")?.ChildNodes.Cast<XmlNode>()
                .Select(a => new BlueprintItem(a.Attributes?["TypeId"]?.Value ?? string.Empty, a.Attributes?["SubtypeId"]?.Value ?? string.Empty, Convert.ToDecimal(a.Attributes?["Amount"]?.Value)))
                .ToList();

            pre ??= new();

            res ??= new List<BlueprintItem>() { new BlueprintItem(item.SelectSingleNode("Result")?.Attributes?["TypeId"]?.Value ?? string.Empty,
                                                                  item.SelectSingleNode("Result")?.Attributes?["SubtypeId"]?.Value ?? string.Empty,
                                                                  Convert.ToDecimal(item.SelectSingleNode("Result")?.Attributes?["Amount"]?.Value)) };

            dataBuilder.AddBlueprint(ItemType.BLUEPRINT,
                                     GetTypeID(item),
                                     GetSubTypeID(item),
                                     GetDisplayName(item),
                                     file,
                                     Convert.ToDecimal(item.SelectSingleNode(PRODTIME_XPATH)?.InnerText),
                                     Convert.ToBoolean(item.SelectSingleNode("IsPrimary")?.InnerText),
                                     pre,
                                     res);

            logger.LogDebug("Added {item}", GetDisplayName(item));
            nodeCount++;
        }

        OutputInfo($"{nodeCount} Items Added");
    }
}
