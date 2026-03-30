using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpeakCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAlunoAtivoTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "AlunoTurmas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "AlunoTurmas");
        }
    }
}
