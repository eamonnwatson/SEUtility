using System.ComponentModel.DataAnnotations;

namespace SEUtility.Parser;

public class ParserType
{
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public string SearchPattern { get; set; } = default!;
    [Required]
    public string XmlNode { get; set; } = default!;

}
