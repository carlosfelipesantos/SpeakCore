using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpeakCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDisciplinaDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoTurmas_Alunos_AlunoId",
                table: "AlunoTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_AlunoTurmas_Turmas_TurmaId",
                table: "AlunoTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Disciplinas_DisciplinaId",
                table: "Turmas");

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoTurmas_Alunos_AlunoId",
                table: "AlunoTurmas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoTurmas_Turmas_TurmaId",
                table: "AlunoTurmas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Disciplinas_DisciplinaId",
                table: "Turmas",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoTurmas_Alunos_AlunoId",
                table: "AlunoTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_AlunoTurmas_Turmas_TurmaId",
                table: "AlunoTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Disciplinas_DisciplinaId",
                table: "Turmas");

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoTurmas_Alunos_AlunoId",
                table: "AlunoTurmas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoTurmas_Turmas_TurmaId",
                table: "AlunoTurmas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Disciplinas_DisciplinaId",
                table: "Turmas",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
