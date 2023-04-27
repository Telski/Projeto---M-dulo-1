using LabSchool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabSchool.Configurations
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        private EntityTypeBuilder<Professor> entityTypeBuilder;

        public ProfessorConfiguration(EntityTypeBuilder<Professor> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }

        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professor");

            builder.HasKey(p => p.Codigo);

            builder.Property(p => p.Nome)
                   .IsRequired();

            builder.Property(p => p.Telefone)
                   .IsRequired();

            builder.Property(p => p.DataNascimento)
                   .IsRequired();

            builder.Property(p => p.CPF)
                   .IsRequired();

            builder.Property(p => p.Estado);

            builder.Property(p => p.Experiencia);

            builder.Property(p => p.FormacaoAcademica)
                   .HasColumnName("FormacaoAcademica")
                   .IsRequired();
        }
    }
}
