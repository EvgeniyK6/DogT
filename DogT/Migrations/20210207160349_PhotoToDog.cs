using Microsoft.EntityFrameworkCore.Migrations;

namespace DogT.Migrations
{
    public partial class PhotoToDog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Dogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "Dogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "Dogs");
        }
    }
}
