using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Regix.Domain.EntitiesSoft;

namespace Regix.AppInfra.ModelConfig.EntitiesSoft;

public class PatientConfig : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasIndex(e => e.PatientId);
        builder.Property(x => x.PatientId).HasDefaultValueSql("NEWSEQUENTIALID()");
        builder.HasIndex(e => e.UserName).IsUnique();
        builder.HasIndex(e => new { e.FullName, e.CorporationId }).IsUnique();
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.SexoAsignado).WithMany(e => e.Patients).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.IdentidadGenero).WithMany(e => e.Patients).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.DocumentType).WithMany(e => e.Patients).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Idioma).WithMany(e => e.Patients).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.EstadoCivil).WithMany(e => e.Patients).OnDelete(DeleteBehavior.Restrict);
    }
}