using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataStore.EF.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventAdministrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAdministrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventAdministrators_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnteredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventAdministrators",
                columns: new[] { "Id", "Address", "Age", "FirstName", "LastName", "PhoneNumber", "ProjectId" },
                values: new object[,]
                {
                    { 1, "London", 0, "First_1", "Last_1", "777-255-712", null },
                    { 2, "Nottingham", 0, "First_2", "Last_2", "777-255-712", null },
                    { 3, "London", 0, "First_3", "Last_3", "777-255-712", null }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Project 1" },
                    { 2, "Project 2" },
                    { 3, "Project 3" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "EnteredDate", "EventDate", "Owner", "Price", "ProjectId", "Title" },
                values: new object[] { 1, "Ticket For Project 1", null, null, null, 0.0, 1, "Ticket 1" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "EnteredDate", "EventDate", "Owner", "Price", "ProjectId", "Title" },
                values: new object[] { 2, "Ticket For Project 2", null, null, null, 0.0, 2, "Ticket 2" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "EnteredDate", "EventDate", "Owner", "Price", "ProjectId", "Title" },
                values: new object[] { 3, "Ticket For Project 3", null, null, null, 0.0, 3, "Ticket 3" });

            migrationBuilder.CreateIndex(
                name: "IX_EventAdministrators_ProjectId",
                table: "EventAdministrators",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ProjectId",
                table: "Tickets",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAdministrators");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
