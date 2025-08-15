using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Regix.Domain.EntitiesSoft;

namespace Regix.AppInfra.ModelConfig.EntitiesSoft;

public class Patient2Config : IEntityTypeConfiguration<Patient2>
{
    public void Configure(EntityTypeBuilder<Patient2> builder)
    {
        builder.HasIndex(e => e.Patient2Id);
        builder.Property(x => x.Patient2Id).HasDefaultValueSql("NEWSEQUENTIALID()");
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.PatientControl).WithMany(e => e.Patient2s).OnDelete(DeleteBehavior.Restrict);
    }
}