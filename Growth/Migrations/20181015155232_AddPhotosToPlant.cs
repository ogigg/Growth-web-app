using Microsoft.EntityFrameworkCore.Migrations;

namespace Growth.Migrations
{
    public partial class AddPhotosToPlant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Images",
                newName: "Caption");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Plants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Images",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_ImageId",
                table: "Plants",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Images_ImageId",
                table: "Plants",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Images_ImageId",
                table: "Plants");

            migrationBuilder.DropIndex(
                name: "IX_Plants_ImageId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Caption",
                table: "Images",
                newName: "Path");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Images",
                nullable: true);
        }
    }
}
