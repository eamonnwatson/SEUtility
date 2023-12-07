using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Persistence.Configuration;
internal class BlueprintConfiguration : IEntityTypeConfiguration<Blueprint>
{
    public void Configure(EntityTypeBuilder<Blueprint> builder)
    {
        builder.Navigation(e => e.Prerequisites).AutoInclude();
        builder.Navigation(e => e.Results).AutoInclude();
    }
}
