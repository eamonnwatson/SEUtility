using AutoMapper;
using SEUtility.Common;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using SEUtility.Data.Interfaces;
using SEUtility.Data.Models;
using SEUtility.Data.Repositories;

using CommonModels = SEUtility.Common.Models;
using DataModels = SEUtility.Data.Models;

namespace SEUtility.Data;

internal class DataService : IDataService
{
    private IItemRepository ItemRepository { get; init; }
    private IBlockRepository BlockRepository { get; init; }
    private IBlueprintRepository BlueprintRepository { get; init; }
    private IBPClassRepository BPClassRepository { get; init; }
    private IBlockCategoryRepository BlockCategoryRepository { get; init; }

    private readonly IMapper mapper;

    public DataService(IDatabase database, IMapper mapper)
    {
        ItemRepository = new ItemRepository(database);
        BlockRepository = new BlockRepository(database);
        BlueprintRepository = new BlueprintRepository(database);
        BPClassRepository = new BPClassRepository(database);
        BlockCategoryRepository = new BlockCategoryRepository(database);

        Database = database;
        this.mapper = mapper;
    }

    public IDatabase Database { get; init; }

    public SpaceEngineersData GetData()
    {
        var ammo = mapper.Map<List<AmmoMagazine>>(ItemRepository.GetAll().Where(i => i.Type == "AMMO"));
        var items = mapper.Map<List<PhysicalItem>>(ItemRepository.GetAll().Where(i => i.Type != "AMMO"));
        var blocks = mapper.Map<List<CubeBlock>>(BlockRepository.GetAll());
        var bp = mapper.Map<List<CommonModels.Blueprint>>(BlueprintRepository.GetAll());
        var bpc = mapper.Map<List<CommonModels.BlueprintClass>>(BPClassRepository.GetAll());
        var bc = mapper.Map<List<CommonModels.BlockCategory>>(BlockCategoryRepository.GetAll());

        var data = DataBuilder.Create()
            .AddAmmoMagazines(ammo)
            .AddBlockCategories(bc)
            .AddBlueprints(bp)
            .AddBlueprintClasses(bpc)
            .AddPhysicalItems(items)
            .AddCubeBlocks(blocks)
            .BuildData();

        return data;
    }

    public void SaveData(SpaceEngineersData data)
    {
        Database.EnsureCreated();
        ItemRepository.Add(mapper.Map<IEnumerable<Item>>(data.PhysicalItems));
        ItemRepository.Add(mapper.Map<IEnumerable<Item>>(data.AmmoMagazines));
        BlockRepository.Add(mapper.Map<IEnumerable<Block>>(data.CubeBlocks));
        BlueprintRepository.Add(mapper.Map<IEnumerable<DataModels.Blueprint>>(data.Blueprints));
        BPClassRepository.Add(mapper.Map<IEnumerable<DataModels.BlueprintClass>>(data.BlueprintClasses));
        BlockCategoryRepository.Add(mapper.Map<IEnumerable<DataModels.BlockCategory>>(data.BlockCategories));
    }
}
