using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskAssignUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeUserId",
                table: "Tasks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeUserId",
                table: "Tasks",
                column: "AssigneeUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AssigneeUserId",
                table: "Tasks",
                column: "AssigneeUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AssigneeUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AssigneeUserId",
                table: "Tasks");
        }
    }
}
