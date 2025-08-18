using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Regix.Domain.EntitiesGen;

namespace Regix.AppInfra.ModelConfig.EntitiesGen;

public class VeteranConfig : IEntityTypeConfiguration<Veteran>
{
    public void Configure(EntityTypeBuilder<Veteran> builder)
    {
        builder.HasKey(e => e.VeteranId);
        builder.HasIndex(e => new { e.Name }).IsUnique();
        builder.Property(e => e.Name).UseCollation("Latin1_General_CI_AS"); //Para poderlo volver Collation CI
    }
}