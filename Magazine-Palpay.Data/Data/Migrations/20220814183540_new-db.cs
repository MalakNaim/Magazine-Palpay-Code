using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Magazine_Palpay.Data.Migrations
{
    public partial class newdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MagazineLookup");

            migrationBuilder.DropColumn(
                name: "LookupId",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "PostPhoto",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Post",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "PostType",
                table: "Post",
                newName: "PostTypeId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Post",
                newName: "Head");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Post",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Menu",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "MagazineSetting",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Gallery",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Gallery",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Employee",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Ads",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Desription",
                table: "Ads",
                newName: "Owner");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Ads",
                newName: "IsDelete");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PostPhoto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PostPhoto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MagazineSetting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MagazineSetting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Gallery",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Gallery",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Head",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryId = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryPhotos_Gallery_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostTypeId",
                table: "Post",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryPhotos_GalleryId",
                table: "GalleryPhotos",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_PostId",
                table: "Video",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_PostType_PostTypeId",
                table: "Post",
                column: "PostTypeId",
                principalTable: "PostType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_PostType_PostTypeId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "GalleryPhotos");

            migrationBuilder.DropTable(
                name: "PostType");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Post_PostTypeId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PostPhoto");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PostPhoto");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MagazineSetting");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MagazineSetting");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Gallery");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Gallery");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Head",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Ads");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "PostPhoto",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Post",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "PostTypeId",
                table: "Post",
                newName: "PostType");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Post",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "Head",
                table: "Post",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Menu",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "MagazineSetting",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Gallery",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Gallery",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Employee",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Ads",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Ads",
                newName: "Desription");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Ads",
                newName: "Deleted");

            migrationBuilder.AddColumn<int>(
                name: "LookupId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MagazineLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    LookupChildId = table.Column<int>(type: "int", nullable: true),
                    LookupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagazineLookup", x => x.Id);
                });
        }
    }
}
