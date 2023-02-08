using AutoMapper;
using SEUtility.Common.Models;
using SEUtility.Data.Models;
using System.Text.Json;

namespace SEUtility.Data.MapperConfig;

internal class DataProfile : Profile
{
    public DataProfile()
    {

        CreateMap<PhysicalItem, Item>().ReverseMap();
        CreateMap<AmmoMagazine, Item>().ReverseMap();
        CreateMap<CubeBlock, Block>()
            .ForMember(a => a.Components, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s.Components)))
            .ForMember(a => a.CriticalComponent, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s.CriticalComponent)))
            .ForMember(a => a.BlueprintClasses, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s.BlueprintClasses)))
            .ReverseMap()
            .ForMember(a => a.Components, opt => opt.MapFrom((s, d) => JsonSerializer.Deserialize<List<ComponentItem>>(s.Components)))
            .ForMember(a => a.CriticalComponent, opt => opt.MapFrom((s, d) => JsonSerializer.Deserialize<ComponentItem>(s.CriticalComponent)))
            .ForMember(a => a.BlueprintClasses, opt => opt.MapFrom((s, d) => JsonSerializer.Deserialize<List<string>>(s.BlueprintClasses)))
            .ForMember(a => a.Type, opt => opt.MapFrom(src => ItemType.BLOCK));

        CreateMap<Common.Models.BlueprintClass, Models.BlueprintClass>()
            .ForMember(a => a.SubTypeIDList, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s.SubTypeIDList)))
            .ReverseMap()
            .ForMember(a => a.SubTypeIDList, opt => opt.MapFrom((s, d) => JsonSerializer.Deserialize<List<string>>(s.SubTypeIDList)))
            .ForMember(a => a.Type, opt => opt.MapFrom(src => ItemType.BLUEPRINTCLASS));

        CreateMap<Common.Models.Blueprint, Models.Blueprint>()
            .ForMember(a => a.Prerequisites, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s.Prerequisites)))
            .ForMember(a => a.Results, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s.Results)))
            .ReverseMap()
            .ForMember(a => a.Prerequisites, opt => opt.MapFrom((s, d) => JsonSerializer.Deserialize<List<BlueprintItem>>(s.Prerequisites)))
            .ForMember(a => a.Results, opt => opt.MapFrom((s, d) => JsonSerializer.Deserialize<List<BlueprintItem>>(s.Results)))
            .ForMember(a => a.Type, opt => opt.MapFrom(src => ItemType.BLUEPRINT));

        CreateMap<Common.Models.BlockCategory, Models.BlockCategory>()
            .ForMember(a => a.ItemIDs, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s.ItemIDs)))
            .ReverseMap()
            .ForMember(a => a.ItemIDs, opt => opt.MapFrom((s, d) => JsonSerializer.Deserialize<List<string>>(s.ItemIDs)))
            .ForMember(a => a.Type, opt => opt.MapFrom(src => ItemType.BLOCKCATEGORY));

    }
}
