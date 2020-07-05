using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerApp.Server.Data.UserDbMigrations
{
    public partial class AddTenantID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantID",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "User");
        }
    }
}
