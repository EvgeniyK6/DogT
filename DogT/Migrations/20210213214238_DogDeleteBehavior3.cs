using Microsoft.EntityFrameworkCore.Migrations;

namespace DogT.Migrations
{
    public partial class DogDeleteBehavior3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingComments_DogHandlers_DogHandlerId",
                table: "TrainingComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingComments_Trainings_TrainingId",
                table: "TrainingComments");

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
                name: "FK_TrainingComments_DogHandlers_DogHandlerId",
                table: "TrainingComments",
                column: "DogHandlerId",
                principalTable: "DogHandlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingComments_Trainings_TrainingId",
                table: "TrainingComments",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingComments_DogHandlers_DogHandlerId",
                table: "TrainingComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingComments_Trainings_TrainingId",
                table: "TrainingComments");

            migrationBuilder.AlterColumn<int>(
                name: "DogId",
                table: "Trainings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingComments_DogHandlers_DogHandlerId",
                table: "TrainingComments",
                column: "DogHandlerId",
                principalTable: "DogHandlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingComments_Trainings_TrainingId",
                table: "TrainingComments",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }
    }
}
