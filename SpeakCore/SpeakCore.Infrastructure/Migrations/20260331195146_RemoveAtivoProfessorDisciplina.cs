using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpeakCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAtivoProfessorDisciplina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoTurmas_Alunos_AlunoId",
                table: "AlunoTurmas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Disciplinas");

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoTurmas_Alunos_AlunoId",
                table: "AlunoTurmas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoTurmas_Alunos_AlunoId",
                table: "AlunoTurmas");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Professores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Disciplinas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoTurmas_Alunos_AlunoId",
                table: "AlunoTurmas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
