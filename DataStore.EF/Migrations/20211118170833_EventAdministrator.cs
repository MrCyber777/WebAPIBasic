using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataStore.EF.Migrations
{
    public partial class EventAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "EventAdministrators",
                columns: new[] { "Id", "Address", "Age", "FirstName", "LastName", "PhoneNumber", "ProjectId" },
                values: new object[] { 1, "London", 0, "First_1", "Last_1", "777-255-712", null });

            migrationBuilder.InsertData(
                table: "EventAdministrators",
                columns: new[] { "Id", "Address", "Age", "FirstName", "LastName", "PhoneNumber", "ProjectId" },
                values: new object[] { 2, "Nottingham", 0, "First_2", "Last_2", "777-255-712", null });

            migrationBuilder.InsertData(
                table: "EventAdministrators",
                columns: new[] { "Id", "Address", "Age", "FirstName", "LastName", "PhoneNumber", "ProjectId" },
                values: new object[] { 3, "London", 0, "First_3", "Last_3", "777-255-712", null });

            migrationBuilder.CreateIndex(
                name: "IX_EventAdministrators_ProjectId",
                table: "EventAdministrators",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAdministrators");
        }
    }
}
