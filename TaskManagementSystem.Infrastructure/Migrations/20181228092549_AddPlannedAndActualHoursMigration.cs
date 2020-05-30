using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Infrastructure.Migrations
{
    public partial class AddPlannedAndActualHoursMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ActualCompletionHours",
                table: "ManagedTask",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Executors",
                table: "ManagedTask",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlannedCompletionHours",
                table: "ManagedTask",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualCompletionHours",
                table: "ManagedTask");

            migrationBuilder.DropColumn(
                name: "Executors",
                table: "ManagedTask");

            migrationBuilder.DropColumn(
                name: "PlannedCompletionHours",
                table: "ManagedTask");
        }
    }
}
