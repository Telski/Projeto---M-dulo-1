using LabSchool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabSchool.Configurations
{
    public class PedagogoConfiguration : IEntityTypeConfiguration<Pedagogo>
    {
        private EntityTypeBuilder<Pedagogo> entityTypeBuilder;

        public PedagogoConfiguration(EntityTypeBuilder<Pedagogo> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }

        public void Configure(EntityTypeBuilder<Pedagogo> builder)
        {
            builder.ToTable("Pedagogo");

            builder.HasKey(p => p.Codigo);

            builder.Property(p => p.Codigo)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Telefone)
                .HasColumnName("Telefone")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.DataNascimento)
                .HasColumnName("DataNascimento")
                .IsRequired();

            builder.Property(p => p.CPF)
                .HasColumnName("CPF")
                .IsRequired();

            builder.Property(p => p.Atendimento)
                .HasColumnName("QtdAtendimentos")
                .IsRequired();
        }
    }
}
