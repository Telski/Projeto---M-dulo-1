using LabSchool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabSchool.Configurations
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        private EntityTypeBuilder<Aluno> entityTypeBuilder;

        public AlunoConfiguration(EntityTypeBuilder<Aluno> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }

        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.HasKey(a => a.Codigo);

            builder.Property(a => a.Codigo)
                .HasColumnName("Codigo");

            builder.Property(a => a.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Telefone)
                .HasColumnName("Telefone")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(a => a.DataNascimento)
                .HasColumnName("DataNascimento")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(a => a.CPF)
                .HasColumnName("CPF")
                .IsRequired();

            builder.Property(a => a.Situacao)
                .HasColumnName("SituacaoMatricula")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.Nota)
                .HasColumnName("Nota")
                .IsRequired();

            builder.Property(a => a.Atendimentos)
                .HasColumnName("QtdAtendimentos")
                .IsRequired();
        }
    }
}
