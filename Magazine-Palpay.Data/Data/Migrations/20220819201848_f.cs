using Microsoft.EntityFrameworkCore.Migrations;

namespace Magazine_Palpay.Data.Migrations
{
    public partial class f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_PostType_PostSubTypeId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_PostSubTypeId",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "EmbedLink",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmbedLink",
                table: "Video");

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
    }
}
