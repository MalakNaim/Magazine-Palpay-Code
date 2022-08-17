using Microsoft.EntityFrameworkCore.Migrations;

namespace Magazine_Palpay.Data.Migrations
{
    public partial class m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostSubType",
                table: "Post",
                newName: "PostSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostSubTypeId",
                table: "Post",
                column: "PostSubTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_PostType_PostSubTypeId",
                table: "Post",
                column: "PostSubTypeId",
                principalTable: "PostType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_PostType_PostSubTypeId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_PostSubTypeId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "PostSubTypeId",
                table: "Post",
                newName: "PostSubType");
        }
    }
}
