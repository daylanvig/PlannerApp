using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerApp.Server.Data.Migrations
{
    public partial class SwitchToSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlannerItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    PlannedActionDate = table.Column<DateTime>(nullable: false),
                    PlannedEndTime = table.Column<DateTime>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: true),
                    CategoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannerItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlannerItem_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannerItem_CategoryID",
                table: "PlannerItem",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannerItem");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
