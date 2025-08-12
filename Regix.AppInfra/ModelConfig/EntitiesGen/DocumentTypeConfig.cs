using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Regix.Domain.EntitiesGen;

namespace Regix.AppInfra.ModelConfig.EntitiesGen;

public class DocumentTypeConfig : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.HasKey(e => e.DocumentTypeId);
        builder.HasIndex(e => new { e.DocumentName }).IsUnique();
        builder.Property(e => e.DocumentName).UseCollation("Latin1_General_CI_AS"); //Para poderlo volver Collation CI
    }
}