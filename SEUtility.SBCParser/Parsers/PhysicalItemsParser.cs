using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using System.Xml;

namespace SEUtility.Parser.Parsers;

[Parser("PhysicalItem")]
internal class PhysicalItemsParser : BaseParser, IParser
{
    public PhysicalItemsParser(ILogger<PhysicalItemsParser> logger, IMediator mediator) : base(logger, mediator)
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

            dataBuilder.AddPhysicalItem(GetItemType(item),
                                        GetTypeID(item),
                                        GetSubTypeID(item),
                                        GetDisplayName(item),
                                        file,
                                        Convert.ToDecimal(item.SelectSingleNode("Mass")?.InnerText),
                                        Convert.ToDecimal(item.SelectSingleNode("Volume")?.InnerText),
                                        Convert.ToBoolean(item.SelectSingleNode("CanPlayerOrder")?.InnerText));

            logger.LogDebug("Added {item}", GetDisplayName(item));
            nodeCount++;
        }

        OutputInfo($"{nodeCount} Items Added");
    }
    private static ItemType GetItemType(XmlNode item)
    {
        switch (GetTypeID(item).ToLower())
        {
            case "ore":
                return ItemType.ORE;
            case "ingot":
                return ItemType.INGOT;
            case "physicalgunobject":
                var subTypeID = GetSubTypeID(item);

                if (subTypeID.Contains("Welder") || subTypeID.Contains("Grinder") || subTypeID.Contains("Drill"))
                    return ItemType.TOOL;

                return ItemType.WEAPON;
            case "oxygencontainerobject":
            case "gascontainerobject":
                return ItemType.BOTTLE;
            case "consumableitem":
                return ItemType.CONSUMABLE;
            case "component":
                return ItemType.COMPONENT;
            default:
                return ItemType.OTHER;
        }
    }
}
