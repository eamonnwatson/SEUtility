using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Persistence.Configuration;
internal class CubeBlockConfiguration : IEntityTypeConfiguration<CubeBlock>
{
    public void Configure(EntityTypeBuilder<CubeBlock> builder)
    {
        builder.Navigation(e => e.Components).AutoInclude();
    }
}
