using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Regix.Domain.EntitiesSoft;

namespace Regix.AppInfra.ModelConfig.EntitiesSoft;

public class PatientControlConfig : IEntityTypeConfiguration<PatientControl>
{
    public void Configure(EntityTypeBuilder<PatientControl> builder)
    {
        builder.HasIndex(e => e.PatientControlId);
        builder.Property(x => x.PatientControlId).HasDefaultValueSql("NEWSEQUENTIALID()");
        //Proteccion de Borrado en Cascada
        //builder.HasOne(e => e.Patien).WithMany(e => e.Patients).OnDelete(DeleteBehavior.Restrict);
    }
}