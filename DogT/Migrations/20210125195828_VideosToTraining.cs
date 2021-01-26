using Microsoft.EntityFrameworkCore.Migrations;

namespace DogT.Migrations
{
    public partial class VideosToTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Trainings");
        }
    }
}
