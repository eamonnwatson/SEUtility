using SEUtility.Common.Interfaces;

namespace SEUtility.Parser.Parsers;

internal interface IParser
{
    void Parse(string file, IDataBuilder dataBuilder, ParserType options);
}
