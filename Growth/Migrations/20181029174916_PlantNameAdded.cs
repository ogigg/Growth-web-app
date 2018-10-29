using Microsoft.EntityFrameworkCore.Migrations;

namespace Growth.Migrations
{
    public partial class PlantNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Plants",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Plants");

        }
    }
}
