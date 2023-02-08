namespace SEUtility.Data;

internal static class DatabaseCommands
{
    public const string CREATE_ITEM_TABLE = "DROP TABLE IF EXISTS Items;" +
        "CREATE TABLE Items " +
        "(ID INTEGER NOT NULL, Type TEXT NOT NULL, TypeId TEXT NOT NULL, SubTypeId TEXT NOT NULL, DisplayName TEXT NOT NULL, FileName TEXT NOT NULL, " +
        "Mass NUMERIC NOT NULL, Volume NUMERIC NOT NULL, CanPlayerOrder NUMERIC NOT NULL, Capacity NUMERIC NOT NULL, PRIMARY KEY(\"ID\" AUTOINCREMENT));";

    public const string CREATE_BLOCK_TABLE = "DROP TABLE IF EXISTS Blocks;" +
        "CREATE TABLE Blocks " +
        "(ID INTEGER NOT NULL, TypeId TEXT NOT NULL, SubTypeId TEXT NOT NULL, DisplayName TEXT NOT NULL, FileName TEXT NOT NULL, " +
        "Description TEXT NOT NULL, CubeSize TEXT NOT NULL, Public NUMERIC NOT NULL, BuildTimeSeconds NUMERIC NOT NULL, " +
        "Components TEXT NOT NULL, CriticalComponent TEXT NOT NULL, " +
        "PCU INTEGER NOT NULL, RefineSpeed NUMERIC, MaterialEfficiency NUMERIC, AssemblySpeed NUMERIC, BlueprintClasses TEXT NOT NULL, " +
        "PRIMARY KEY(\"ID\" AUTOINCREMENT));";

    public const string CREATE_BLUEPRINT_TABLE = "DROP TABLE IF EXISTS Blueprints;" +
        "CREATE TABLE Blueprints " +
        "(ID INTEGER NOT NULL, TypeId TEXT NOT NULL, SubTypeId TEXT NOT NULL, DisplayName TEXT NOT NULL, FileName TEXT NOT NULL, " +
        "BaseProductionTimeInSeconds NUMERIC NOT NULL, IsPrimary NUMERIC NOT NULL, Prerequisites TEXT NOT NULL, Results TEXT NOT NULL, PRIMARY KEY(\"ID\" AUTOINCREMENT));";

    public const string CREATE_BLUEPRINTCLASS_TABLE = "DROP TABLE IF EXISTS BlueprintsClasses;" +
        "CREATE TABLE BlueprintsClasses " +
        "(ID INTEGER NOT NULL, TypeId TEXT NOT NULL, SubTypeId TEXT NOT NULL, DisplayName TEXT NOT NULL, FileName TEXT NOT NULL, " +
        "Description TEXT NOT NULL, SubTypeIDList TEXT NOT NULL, PRIMARY KEY(\"ID\" AUTOINCREMENT));";

    public const string CREATE_BLOCKCATEGORY_TABLE = "DROP TABLE IF EXISTS BlockCategories;" +
        "CREATE TABLE BlockCategories " +
        "(ID INTEGER NOT NULL, TypeId TEXT NOT NULL, SubTypeId TEXT NOT NULL, DisplayName TEXT NOT NULL, FileName TEXT NOT NULL, " +
        "Name TEXT NOT NULL, ItemIDs TEXT NOT NULL, PRIMARY KEY(\"ID\" AUTOINCREMENT));";

    public const string INSERT_ITEM = "INSERT INTO Items (Type, TypeId, SubTypeId, DisplayName, FileName, Mass, Volume, " +
        "CanPlayerOrder, Capacity) VALUES (@Type, @TypeId, @SubTypeId, @DisplayName, @FileName, @Mass, @Volume, " +
        "@CanPlayerOrder, @Capacity)";

    public const string INSERT_BLOCK = "INSERT INTO Blocks (TypeId, SubTypeId, DisplayName, FileName, Description, " +
        "CubeSize, Public, BuildTimeSeconds, PCU, RefineSpeed, MaterialEfficiency, AssemblySpeed, BlueprintClasses, " +
        "Components, CriticalComponent) VALUES " +
        "(@TypeId, @SubTypeId, @DisplayName, @FileName, @Description, @CubeSize, @Public, @BuildTimeSeconds, @PCU, " +
        "@RefineSpeed, @MaterialEfficiency, @AssemblySpeed, @BlueprintClasses, @Components, @CriticalComponent)";

    public const string INSERT_BLUEPRINT = "INSERT INTO Blueprints (TypeId, SubTypeId, DisplayName, FileName, " +
        "BaseProductionTimeInSeconds, IsPrimary, Prerequisites, Results) VALUES " +
        "(@TypeId, @SubTypeId, @DisplayName, @FileName, @BaseProductionTimeInSeconds, @IsPrimary, @Prerequisites, @Results)";

    public const string INSERT_BLUEPRINTCLASS = "INSERT INTO BlueprintsClasses (TypeId, SubTypeId, DisplayName, FileName, " +
        "Description, SubTypeIDList) VALUES " +
        "(@TypeId, @SubTypeId, @DisplayName, @FileName, @Description, @SubTypeIDList)";

    public const string INSERT_BLOCKCATEGORY = "INSERT INTO BlockCategories (TypeId, SubTypeId, DisplayName, FileName, " +
        "Name, ItemIDs) VALUES " +
        "(@TypeId, @SubTypeId, @DisplayName, @FileName, @Name, @ItemIDs)";

    public const string SELECT_ITEMS = "SELECT Type, TypeId, SubTypeId, DisplayName, FileName, CAST(Mass AS REAL) AS Mass, CAST(Volume AS REAL) AS Volume, CanPlayerOrder, Capacity FROM Items";
    public const string SELECT_BLOCKS = "SELECT TypeId, SubTypeId, DisplayName, FileName, Description, CubeSize, Public, BuildTimeSeconds, PCU, RefineSpeed, MaterialEfficiency, AssemblySpeed, BlueprintClasses, Components, CriticalComponent FROM Blocks";
    public const string SELECT_BLUEPRINTS = "SELECT TypeId, SubTypeId, DisplayName, FileName, CAST(BaseProductionTimeInSeconds AS REAL) AS BaseProductionTimeInSeconds, IsPrimary, Prerequisites, Results FROM Blueprints";
    public const string SELECT_BLUEPRINTCLASS = "SELECT TypeId, SubTypeId, DisplayName, FileName, Description, SubTypeIDList FROM BlueprintsClasses";
    public const string SELECT_BLOCKCATEGORY = "SELECT TypeId, SubTypeId, DisplayName, FileName, Name, ItemIDs FROM BlockCategories";

}
