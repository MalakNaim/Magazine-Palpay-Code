using Microsoft.EntityFrameworkCore.Migrations;

namespace Magazine_Palpay.Data.Migrations
{
    public partial class nmkm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Post",
                newName: "OrderPlace");

            migrationBuilder.RenameColumn(
                name: "IsPublished",
                table: "Post",
                newName: "PublishedPost");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishedPost",
                table: "Post",
                newName: "IsPublished");

            migrationBuilder.RenameColumn(
                name: "OrderPlace",
                table: "Post",
                newName: "Order");
        }
    }
}
