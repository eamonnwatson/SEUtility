using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Persistence.Configuration;
internal class PhysicalItemConfiguration : IEntityTypeConfiguration<PhysicalItem>
{
    public void Configure(EntityTypeBuilder<PhysicalItem> builder)
    {
    }
}
