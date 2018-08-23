using Microsoft.EntityFrameworkCore.Migrations;

namespace Growth.Migrations
{
    public partial class PlantClassUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Orders_OrderId",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Plants",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Orders_OrderId",
                table: "Plants",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Orders_OrderId",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Plants",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Orders_OrderId",
                table: "Plants",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
