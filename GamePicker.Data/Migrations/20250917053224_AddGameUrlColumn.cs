using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePicker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Game_url",
                table: "T001_RECOMENDATIONS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Game_url",
                table: "T001_RECOMENDATIONS");
        }
    }
}
