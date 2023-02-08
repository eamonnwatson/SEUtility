namespace SEUtility.Parser.Parsers;

[AttributeUsage(AttributeTargets.Class)]
internal class ParserAttribute : Attribute
{
    public string Name { get; }
    public ParserAttribute(string name)
    {
        Name = name;
    }

}
