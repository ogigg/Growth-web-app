using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Growth.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Species_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PlantId = table.Column<int>(nullable: false),
                    SpeciesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Features_SpeciesId",
                table: "Features",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Species_OrderId",
                table: "Species",
                column: "OrderId");

            migrationBuilder.Sql("INSERT INTO Orders (Name) VALUES ('astrowce')");
            migrationBuilder.Sql("INSERT INTO Orders (Name) VALUES ('różowce')");
            migrationBuilder.Sql("INSERT INTO Orders (Name) VALUES ('goryczkowce')");

            migrationBuilder.Sql("INSERT INTO Species (Name, OrderId) VALUES ('Mniszek pospolity', (SELECT ID FROM Orders WHERE Name='astrowce'))");
            migrationBuilder.Sql("INSERT INTO Species (Name, OrderId) VALUES ('Pokrzywa', (SELECT ID FROM Orders WHERE Name='różowce'))");
            migrationBuilder.Sql("INSERT INTO Species (Name, OrderId) VALUES ('Kawowiec', (SELECT ID FROM Orders WHERE Name='goryczkowce'))");

        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
