using Microsoft.EntityFrameworkCore.Migrations;

namespace DogT.Migrations
{
    public partial class FixModelDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTasks_Dogs_DogId",
                table: "TrainingTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTasks_Dogs_DogId",
                table: "TrainingTasks",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTasks_Dogs_DogId",
                table: "TrainingTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTasks_Dogs_DogId",
                table: "TrainingTasks",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
