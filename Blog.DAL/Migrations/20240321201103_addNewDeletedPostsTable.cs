using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DAL.Migrations
{
    public partial class addNewDeletedPostsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Post_PostId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "DeletedPostId",
                table: "PostTags",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DeletedPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pinned = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedPosts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_DeletedPostId",
                table: "PostTags",
                column: "DeletedPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_DeletedPosts_DeletedPostId",
                table: "PostTags",
                column: "DeletedPostId",
                principalTable: "DeletedPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_DeletedPosts_DeletedPostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropTable(
                name: "DeletedPosts");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_DeletedPostId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeletedPostId",
                table: "PostTags");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Post_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
