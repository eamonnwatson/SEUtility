using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using System.Xml;

namespace SEUtility.Parser.Parsers;

[Parser("AmmoMagazine")]
internal class AmmoMagazineParser : BaseParser, IParser
{
    public AmmoMagazineParser(ILogger<AmmoMagazineParser> logger, IMediator mediator) : base(logger, mediator) { }

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

            dataBuilder.AddAmmoMagazine(ItemType.AMMO,
                                        GetTypeID(item),
                                        GetSubTypeID(item),
                                        GetDisplayName(item),
                                        file,
                                        Convert.ToDecimal(item.SelectSingleNode("Mass")?.InnerText),
                                        Convert.ToDecimal(item.SelectSingleNode("Volume")?.InnerText),
                                        Convert.ToBoolean(item.SelectSingleNode("CanPlayerOrder")?.InnerText),
                                        Convert.ToInt32(item.SelectSingleNode("Capacity")?.InnerText));

            logger.LogDebug("Added {item}", GetDisplayName(item));
            nodeCount++;
        }

        OutputInfo($"{nodeCount} Items Added");

    }
}
