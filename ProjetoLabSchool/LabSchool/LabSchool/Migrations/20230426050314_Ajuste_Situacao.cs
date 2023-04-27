using LabSchool.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabSchool.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste_Situacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.AddColumn<int>(
           name: "NovaColunaSituacaoMatricula",
           table: "Alunos",
           type: "int",
           nullable: false,
           defaultValue: (int)EnumSituacaoMatricula.ATIVO);
           migrationBuilder.Sql("UPDATE Alunos SET NovaColunaSituacaoMatricula = CASE SituacaoMatricula " +
                      "WHEN 'ATIVO' THEN 0 " +
                      "WHEN 'IRREGULAR' THEN 1 " +
                      "WHEN 'ATENDIMENTO_PEDAGOGICO' THEN 2 " +
                      "WHEN 'INATIVO' THEN 3 " +
                      "END");
            migrationBuilder.DropColumn(
            name: "SituacaoMatricula",
            table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "NovaColunaSituacaoMatricula",
                table: "Alunos",
                newName: "SituacaoMatricula");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "NovaColunaSituacaoMatricula",
            table: "Alunos",
            type: "nvarchar(80)",
            maxLength: 80,
            nullable: false,
            defaultValue: "");

            // Copia os dados da coluna antiga para a nova
            migrationBuilder.Sql("UPDATE Alunos SET NovaColunaSituacaoMatricula = CASE SituacaoMatricula " +
                                  "WHEN 0 THEN 'ATIVO' " +
                                  "WHEN 1 THEN 'IRREGULAR' " +
                                  "WHEN 2 THEN 'ATENDIMENTO_PEDAGOGICO' " +
                                  "WHEN 3 THEN 'INATIVO' " +
                                  "END");

            migrationBuilder.DropColumn(
                name: "SituacaoMatricula",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "NovaColunaSituacaoMatricula",
                table: "Alunos",
                newName: "SituacaoMatricula");
        }
    }
}