using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerApp.Server.Data.Migrations
{
    public partial class AddTenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantID",
                table: "PlannerItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantID",
                table: "Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "PlannerItem");

            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "Category");
        }
    }
}
