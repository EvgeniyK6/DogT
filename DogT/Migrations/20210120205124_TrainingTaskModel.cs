using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogT.Migrations
{
    public partial class TrainingTaskModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DogId = table.Column<int>(type: "int", nullable: false),
                    DogHandlerId = table.Column<int>(type: "int", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTasks_DogHandlers_DogHandlerId",
                        column: x => x.DogHandlerId,
                        principalTable: "DogHandlers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingTasks_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTasks_DogHandlerId",
                table: "TrainingTasks",
                column: "DogHandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTasks_DogId",
                table: "TrainingTasks",
                column: "DogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingTasks");
        }
    }
}
