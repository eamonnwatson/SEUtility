using FluentResults;

namespace SpaceEngineers.Parser.Errors;
internal static class ParserError
{
    public static Error NoAmmoMagazine => new("Ammo Magazine Results are empty.");
    public static Error GeneralAmmoError => new("An Error occured Procssing Ammo Magazines.");

    public static Error NoBlueprints => new("Blueprint Results are empty.");
    public static Error GeneralBlueprintError => new("An Error occured Procssing Blueprints.");

    public static Error NoBlueprintClasses => new("Blueprint Class Results are empty.");
    public static Error GeneralBlueprintClassError => new("An Error occured Procssing Blueprint Classes.");

    public static Error NoBlueprintClassEntries => new("Blueprint Class Entry Results are empty.");
    public static Error GeneralBlueprintClassEntryError => new("An Error occured Procssing Blueprint Classes Entries.");

    public static Error NoComponents => new("Component Results are empty.");
    public static Error GeneralComponentError => new("An Error occured Procssing Components.");

    public static Error NoCubeBlock => new("CubeBlock Results are empty.");
    public static Error GeneralCubeBlockError => new("An Error occured Procssing CubeBlocks.");

    public static Error NoLocalization => new("Localization Results are empty.");
    public static Error GeneralLocalizationError => new("An Error occured Procssing Localization.");

    public static Error NoPhysicalItems => new("Physical Item Results are empty.");
    public static Error GeneralPhysicalItemError => new("An Error occured Procssing Physical Items.");

    public static Error DirectoryNotFound => new("The Directory specified was not found.");
    public static Error FileNotFouind => new("The File specified was not found.");
    public static Error ParsingError => new("Error parsing Space Engineers files.");
}
