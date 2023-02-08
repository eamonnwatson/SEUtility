using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Exceptions;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using System.Xml;

namespace SEUtility.Parser.Parsers;

internal class ShipBlueprintParser : BaseParser, IShipParser
{
    private const string BP_PREFIX = "MyObjectBuilder_";

    public ShipBlueprintParser(ILogger<ShipBlueprintParser> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    public void Parse(string filename, IShipBlueprintBuilder builder)
    {
        var node = GetXmlNode(filename, "ShipBlueprints");

        if (node is null || !node.HasChildNodes)
        {
            throw new SEException("No Blueprints found");
        }

        var sbp = node.FirstChild!;
        builder.SetName(sbp.SelectSingleNode("Id")?.Attributes?["Subtype"]?.Value ?? string.Empty);

        var grid = sbp.SelectSingleNode("CubeGrids//CubeGrid");

        if (grid is null)
            throw new SEException("No Grid found");

        var size = grid.SelectSingleNode("GridSizeEnum")?.InnerText ?? string.Empty;

        builder.SetSize(size.ToUpper() == "LARGE" ? CubeSize.LARGE : CubeSize.SMALL);

        var cubes = grid.SelectSingleNode("CubeBlocks")?.ChildNodes;

        if (cubes is null || cubes.Count == 0)
            throw new SEException("No Cubes found");

        foreach (XmlNode item in cubes)
        {
            var name = item.Attributes?["xsi:type"]?.Value;
            var subtypeName = item.SelectSingleNode("SubtypeName")?.InnerText;

            if (name is null || subtypeName is null)
                continue;

            logger.LogDebug("{name} Added", name);
            builder.AddItem(name.Replace(BP_PREFIX, string.Empty), subtypeName);

        }

    }
}
