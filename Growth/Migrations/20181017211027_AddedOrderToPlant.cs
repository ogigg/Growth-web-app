using Microsoft.EntityFrameworkCore.Migrations;

namespace Growth.Migrations
{
    public partial class AddedOrderToPlant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OderId",
                table: "Plants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plants_OderId",
                table: "Plants",
                column: "OderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Orders_OderId",
                table: "Plants",
                column: "OderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Orders_OderId",
                table: "Plants");

            migrationBuilder.DropIndex(
                name: "IX_Plants_OderId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "OderId",
                table: "Plants");
        }
    }
}
