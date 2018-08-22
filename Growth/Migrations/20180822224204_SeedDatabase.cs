using Microsoft.EntityFrameworkCore.Migrations;

namespace Growth.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MakeId",
                table: "Plants");

            migrationBuilder.Sql("INSERT INTO Orders (Name) VALUES ('astrowce')");
            migrationBuilder.Sql("INSERT INTO Orders (Name) VALUES ('różowce')");
            migrationBuilder.Sql("INSERT INTO Orders (Name) VALUES ('goryczkowce')");

            migrationBuilder.Sql("INSERT INTO Plants (Name, OrderId) VALUES ('Mniszek pospolity', (SELECT ID FROM Orders WHERE Name='astrowce'))");
            migrationBuilder.Sql("INSERT INTO Plants (Name, OrderId) VALUES ('Pokrzywa', (SELECT ID FROM Orders WHERE Name='różowce'))");
            migrationBuilder.Sql("INSERT INTO Plants (Name, OrderId) VALUES ('Kawowiec', (SELECT ID FROM Orders WHERE Name='goryczkowce'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MakeId",
                table: "Plants",
                nullable: false,
                defaultValue: 0);
        }
    }
}
