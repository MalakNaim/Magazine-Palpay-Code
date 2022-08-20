using Microsoft.EntityFrameworkCore.Migrations;

namespace Magazine_Palpay.Data.Migrations
{
    public partial class newdata1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Post",
                newName: "VideoLink");

            migrationBuilder.AddColumn<string>(
                name: "EmbedVideoLink",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmbedVideoLink",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "VideoLink",
                table: "Post",
                newName: "Video");
        }
    }
}
