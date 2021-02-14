using Microsoft.EntityFrameworkCore.Migrations;

namespace DogT.Migrations
{
    public partial class DogDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Dogs_DogId",
                table: "Trainings");

            migrationBuilder.AlterColumn<int>(
                name: "DogId",
                table: "Trainings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Dogs_DogId",
                table: "Trainings",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Dogs_DogId",
                table: "Trainings");

            migrationBuilder.AlterColumn<int>(
                name: "DogId",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Dogs_DogId",
                table: "Trainings",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id");
        }
    }
}
