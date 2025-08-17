using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Regix.Domain.EntitiesSoft;

namespace Regix.AppInfra.ModelConfig.EntitiesSoft;

public class GinecologicoConfig : IEntityTypeConfiguration<Ginecologico>
{
    public void Configure(EntityTypeBuilder<Ginecologico> builder)
    {
        builder.HasIndex(e => e.GinecologicoId);
        builder.Property(x => x.GinecologicoId).HasDefaultValueSql("NEWSEQUENTIALID()");
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.PatientControl).WithMany(e => e.Ginecologicos).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Regular).WithMany(e => e.Ginecologicos).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Anticonception).WithMany(e => e.Ginecologicos).OnDelete(DeleteBehavior.Restrict);
    }
}