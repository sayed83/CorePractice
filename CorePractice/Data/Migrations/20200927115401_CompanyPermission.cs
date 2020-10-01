using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CorePractice.Data.Migrations
{
    public partial class CompanyPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyPermissions",
                columns: table => new
                {
                    CompanyPermissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComId = table.Column<Guid>(maxLength: 128, nullable: false),
                    isDefault = table.Column<int>(nullable: false),
                    isChecked = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPermissions", x => x.CompanyPermissionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPermissions");
        }
    }
}
