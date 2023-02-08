using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using System.Xml;

namespace SEUtility.Parser.Parsers;

[Parser("CubeBlock")]
internal class CubeBlockParser : BaseParser, IParser
{
    public CubeBlockParser(ILogger<CubeBlockParser> logger, IMediator mediator) : base(logger, mediator)
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

            var components = item.SelectSingleNode("Components")?
                                 .ChildNodes.Cast<XmlNode>()
                                 .Where(a => a.NodeType == XmlNodeType.Element && a.Attributes?.Count > 0 && a.Attributes!["Subtype"] is not null)
                                 .Select(a => new ComponentItem(a.Attributes!["Subtype"]?.Value!, Convert.ToInt32(a.Attributes!["Count"]?.Value)))
                                 .ToList() ?? new();

            var blueprintClasses = item.SelectSingleNode("BlueprintClasses")?
                                       .ChildNodes.Cast<XmlNode>()
                                       .Where(a => a.NodeType == XmlNodeType.Element)
                                       .Select(a => a.InnerText)
                                       .ToList() ?? new();

            var criticalComponent = new ComponentItem(item.SelectSingleNode("CriticalComponent")?.Attributes?["Subtype"]?.Value ?? String.Empty, Convert.ToInt32(item.SelectSingleNode("CriticalComponent")?.Attributes?["Index"]?.Value));


            dataBuilder.AddCubeBlock(ItemType.BLOCK,
                                     GetTypeID(item),
                                     GetSubTypeID(item),
                                     GetDisplayName(item),
                                     file,
                                     GetCubeSize(item.SelectSingleNode("CubeSize")?.InnerText),
                                     Convert.ToInt32(item.SelectSingleNode("PCU")?.InnerText),
                                     GetValueOrNull(item.SelectSingleNode("RefineSpeed")?.InnerText),
                                     GetValueOrNull(item.SelectSingleNode("MaterialEfficiency")?.InnerText),
                                     GetValueOrNull(item.SelectSingleNode("AssemblySpeed")?.InnerText),
                                     Convert.ToBoolean(item.SelectSingleNode("Public")?.InnerText),
                                     Convert.ToDecimal(item.SelectSingleNode("BuildTimeSeconds")?.InnerText),
                                     item.SelectSingleNode("Description")?.InnerText ?? string.Empty,
                                     components,
                                     blueprintClasses,
                                     criticalComponent);

            logger.LogDebug("Added {item}", GetDisplayName(item));
            nodeCount++;
        }

        OutputInfo($"{nodeCount} Items Added");
    }

    private static decimal? GetValueOrNull(string? text)
    {
        return text is null ? null : Convert.ToDecimal(text!);
    }

    private static CubeSize GetCubeSize(string? innerText)
    {
        return innerText?.ToLower() == "large" ? CubeSize.LARGE : CubeSize.SMALL;
    }
}
