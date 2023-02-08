using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using System.Xml;

namespace SEUtility.Parser.Parsers;

[Parser("BlueprintClassEntry")]
internal class BlueprintClassEntriesParser : BaseParser, IParser
{
    public BlueprintClassEntriesParser(ILogger<BlueprintClassEntriesParser> logger, IMediator mediator) : base(logger, mediator)
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

            var entryClass = item.Attributes?["Class"]?.Value;
            var entry = item.Attributes?["BlueprintSubtypeId"]?.Value;

            if (entryClass is not null && entry is not null)
            {
                dataBuilder.AddBluePrintClassEntry(entryClass, entry);
            }

            nodeCount++;
        }

        OutputInfo($"{nodeCount} Items Added");
    }
}

