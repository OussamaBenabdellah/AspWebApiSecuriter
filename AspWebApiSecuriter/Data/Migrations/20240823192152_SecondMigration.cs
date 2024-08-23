using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspWebApiSecuriter.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayId",
                table: "Personne",
                type: "TEXT",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Personne_DisplayId",
                table: "Personne",
                column: "DisplayId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Personne_DisplayId",
                table: "Personne");

            migrationBuilder.DropColumn(
                name: "DisplayId",
                table: "Personne");
        }
    }
}
