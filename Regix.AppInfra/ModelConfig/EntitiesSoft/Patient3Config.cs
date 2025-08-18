using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Regix.Domain.EntitiesSoft;

namespace Regix.AppInfra.ModelConfig.EntitiesSoft;

public class Patient3Config : IEntityTypeConfiguration<Patient3>
{
    public void Configure(EntityTypeBuilder<Patient3> builder)
    {
        builder.HasIndex(e => e.Patient3Id);
        builder.Property(x => x.Patient3Id).HasDefaultValueSql("NEWSEQUENTIALID()");
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.PatientControl).WithMany(e => e.Patient3s).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.EtniaRaza).WithMany(e => e.Patient3s).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Discapacidad).WithMany(e => e.Patient3s).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Veteran).WithMany(e => e.Patient3s).OnDelete(DeleteBehavior.Restrict);
    }
}