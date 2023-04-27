using LabSchool.Models;
using Microsoft.EntityFrameworkCore;
using LabSchool.Enums;

namespace LabSchool.Context
{
    public class LabSchoolContext : DbContext
    {
        public LabSchoolContext(DbContextOptions<LabSchoolContext> options) : base(options) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Pedagogo> Pedagogos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CESARTELSKI-NOT\\SQLEXPRESS;Database=Cesarlabschoolbd;User ID=sa;Password=123321;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>()
                .Property(x => x.FormacaoAcademica)
                .HasConversion<string>()
                .HasMaxLength(100);

            modelBuilder.Entity<Professor>()
                .Property(x => x.Estado)
                .HasConversion<string>()
                .HasMaxLength(100);

            modelBuilder.Entity<Professor>()
                .Property(x => x.Experiencia)
                .HasConversion<string>()
                .HasMaxLength(100);

            modelBuilder.Entity<Aluno>()
                .Property(x => x.Situacao)
                .HasConversion<int>()
                .HasMaxLength(80);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }


    }
}
