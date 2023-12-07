using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Persistence.Configuration;
internal class ProductionCubeBlockConfiguration : IEntityTypeConfiguration<ProductionCubeBlock>
{
    public void Configure(EntityTypeBuilder<ProductionCubeBlock> builder)
    {
        builder.Navigation(e => e.Blueprints).AutoInclude();
    }
}
