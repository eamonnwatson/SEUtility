using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Persistence.Configuration;

internal class PhysicalItemListItemConfiguration : IEntityTypeConfiguration<PhysicalItemListItem>
{
    public void Configure(EntityTypeBuilder<PhysicalItemListItem> builder)
    {
        builder.Navigation(e => e.Component).AutoInclude();
    }
}
