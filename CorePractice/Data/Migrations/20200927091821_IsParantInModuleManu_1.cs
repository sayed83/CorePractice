using Microsoft.EntityFrameworkCore.Migrations;

namespace CorePractice.Data.Migrations
{
    public partial class IsParantInModuleManu_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsParent",
                table: "ModuleMenu",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsParent",
                table: "ModuleMenu",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
