using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class multipleImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMainImage",
                table: "ProductImages",
                newName: "IsMain");

            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                table: "ProductImages",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMain",
                table: "ProductImages",
                newName: "IsMainImage");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ProductImages",
                newName: "ImageFileName");
        }
    }
}
