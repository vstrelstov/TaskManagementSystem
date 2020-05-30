using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Infrastructure.Migrations
{
    public partial class TaskStateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ManagedTask",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "ManagedTask");
        }
    }
}
