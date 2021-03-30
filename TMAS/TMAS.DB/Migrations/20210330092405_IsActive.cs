using Microsoft.EntityFrameworkCore.Migrations;

namespace TMAS.DB.Migrations
{
    public partial class IsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Columns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Boards",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Boards");
        }
    }
}
