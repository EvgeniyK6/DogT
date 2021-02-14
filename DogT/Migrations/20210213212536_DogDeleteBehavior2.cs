using Microsoft.EntityFrameworkCore.Migrations;

namespace DogT.Migrations
{
    public partial class DogDeleteBehavior2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_DogHandlers_DogHandlerId",
                table: "Dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Dogs_DogId",
                table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTasks_Dogs_DogId",
                table: "TrainingTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_DogHandlers_DogHandlerId",
                table: "Dogs",
                column: "DogHandlerId",
                principalTable: "DogHandlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Dogs_DogId",
                table: "Trainings",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTasks_Dogs_DogId",
                table: "TrainingTasks",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_DogHandlers_DogHandlerId",
                table: "Dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Dogs_DogId",
                table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTasks_Dogs_DogId",
                table: "TrainingTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_DogHandlers_DogHandlerId",
                table: "Dogs",
                column: "DogHandlerId",
                principalTable: "DogHandlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Dogs_DogId",
                table: "Trainings",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTasks_Dogs_DogId",
                table: "TrainingTasks",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id");
        }
    }
}
