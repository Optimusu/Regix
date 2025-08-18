using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Regix.Domain.EntitiesGen;

namespace Regix.AppInfra.ModelConfig.EntitiesGen;

public class DiscapacidadConfig : IEntityTypeConfiguration<Discapacidad>
{
    public void Configure(EntityTypeBuilder<Discapacidad> builder)
    {
        builder.HasKey(e => e.DiscapacidadId);
        builder.HasIndex(e => new { e.Name }).IsUnique();
        builder.Property(e => e.Name).UseCollation("Latin1_General_CI_AS"); //Para poderlo volver Collation CI
    }
}