using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DAL.Migrations
{
    public partial class AddUserIDToPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Posts",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "UserName");
        }
    }
}
