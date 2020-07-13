using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerApp.Server.Data.Migrations
{
    public partial class AddCompletionDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "PlannerItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "PlannerItem");
        }
    }
}
