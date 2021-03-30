using Microsoft.EntityFrameworkCore.Migrations;

namespace TMAS.DB.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionObjectId",
                table: "Histories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionObjectId",
                table: "Histories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
