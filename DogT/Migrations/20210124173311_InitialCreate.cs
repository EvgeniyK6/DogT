using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogT.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DogHandlers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogHandlers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DogHandlers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DogHandlerId = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dogs_DogHandlers_DogHandlerId",
                        column: x => x.DogHandlerId,
                        principalTable: "DogHandlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dogs_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DogHandlerId = table.Column<int>(type: "int", nullable: false),
                    DogId = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estimate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_DogHandlers_DogHandlerId",
                        column: x => x.DogHandlerId,
                        principalTable: "DogHandlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainings_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trainings_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateTable(
                name: "TrainingComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    DogHandlerId = table.Column<int>(type: "int", nullable: false),
                    CommentContext = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingComments_DogHandlers_DogHandlerId",
                        column: x => x.DogHandlerId,
                        principalTable: "DogHandlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingComments_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Адміністратор" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Кінолог" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId" },
                values: new object[] { 1, "admin@mail", "admin", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_DogHandlers_UserId",
                table: "DogHandlers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_DogHandlerId",
                table: "Dogs",
                column: "DogHandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_SpecializationId",
                table: "Dogs",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingComments_DogHandlerId",
                table: "TrainingComments",
                column: "DogHandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingComments_TrainingId",
                table: "TrainingComments",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_DogHandlerId",
                table: "Trainings",
                column: "DogHandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_DogId",
                table: "Trainings",
                column: "DogId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_SpecializationId",
                table: "Trainings",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTasks_DogHandlerId",
                table: "TrainingTasks",
                column: "DogHandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTasks_DogId",
                table: "TrainingTasks",
                column: "DogId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingComments");

            migrationBuilder.DropTable(
                name: "TrainingTasks");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Dogs");

            migrationBuilder.DropTable(
                name: "DogHandlers");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
