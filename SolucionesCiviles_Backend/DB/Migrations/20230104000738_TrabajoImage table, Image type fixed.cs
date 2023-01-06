using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class TrabajoImagetableImagetypefixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrabajoImage_Image_TrabajoId",
                table: "TrabajoImage");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajoImage_ImageId",
                table: "TrabajoImage",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrabajoImage_Image_ImageId",
                table: "TrabajoImage",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrabajoImage_Image_ImageId",
                table: "TrabajoImage");

            migrationBuilder.DropIndex(
                name: "IX_TrabajoImage_ImageId",
                table: "TrabajoImage");

            migrationBuilder.AddForeignKey(
                name: "FK_TrabajoImage_Image_TrabajoId",
                table: "TrabajoImage",
                column: "TrabajoId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
