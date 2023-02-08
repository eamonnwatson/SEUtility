namespace SEUtility.Common.Models;

public class Recipe
{
    private readonly Dictionary<ItemType, List<RecipeItem>> items = new();
    private readonly List<string> errors = new();

    public IEnumerable<RecipeItem> Blocks { get => items[ItemType.BLOCK]; }
    public IEnumerable<RecipeItem> Components { get => items[ItemType.COMPONENT]; }
    public IEnumerable<RecipeItem> Ores { get => items[ItemType.ORE]; }
    public IEnumerable<RecipeItem> Ingots { get => items[ItemType.INGOT]; }
    public IEnumerable<RecipeItem> AllItems { get => items.SelectMany(a => a.Value); }
    public IEnumerable<string> Errors { get => errors; }

    public Recipe()
    {
        foreach (var item in Enum.GetValues<ItemType>())
        {
            items.Add(item, new List<RecipeItem>());
        }
    }

    public void AddItem(BaseItem item)
    {
        AddItems(1, item);
    }

    public void AddItems(decimal qty, BaseItem item)
    {
        var recipeList = items[item.Type];

        var recipeItem = recipeList.FirstOrDefault(a => a.Item == item);
        if (recipeItem is null)
        {
            recipeItem = new RecipeItem(0, item);
            recipeList.Add(recipeItem);
        }

        recipeItem.Quantity += qty;
    }

    public void AddError(string message)
    {
        errors.Add(message);
    }
}
