using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Infrastructure.Migrations
{
    public partial class TasksTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManagedTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateOfIssue = table.Column<DateTime>(nullable: false),
                    DateOfCompletion = table.Column<DateTime>(nullable: true),
                    ParentTaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagedTask_ManagedTask_ParentTaskId",
                        column: x => x.ParentTaskId,
                        principalTable: "ManagedTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagedTask_ParentTaskId",
                table: "ManagedTask",
                column: "ParentTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagedTask");
        }
    }
}
