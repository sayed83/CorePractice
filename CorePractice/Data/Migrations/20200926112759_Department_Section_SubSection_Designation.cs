using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CorePractice.Data.Migrations
{
    public partial class Department_Section_SubSection_Designation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cat_Department",
                columns: table => new
                {
                    DeptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComId = table.Column<string>(nullable: true),
                    DeptCode = table.Column<string>(nullable: true),
                    DeptName = table.Column<string>(maxLength: 25, nullable: false),
                    DeptBangla = table.Column<string>(maxLength: 25, nullable: true),
                    Slno = table.Column<short>(nullable: true),
                    PcName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(maxLength: 128, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    UpdateByUserId = table.Column<string>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    DtInput = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Department", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "Cat_Grade",
                columns: table => new
                {
                    GradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeName = table.Column<string>(maxLength: 25, nullable: false),
                    GradeNameB = table.Column<string>(maxLength: 25, nullable: true),
                    MinGS = table.Column<double>(nullable: true),
                    TtlManpower = table.Column<int>(nullable: true),
                    SalaryRange = table.Column<string>(maxLength: 30, nullable: true),
                    TtlManPowerWorker = table.Column<int>(nullable: true),
                    SlNo = table.Column<int>(nullable: true),
                    ComId = table.Column<string>(nullable: true),
                    PcName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    UpdateByUserId = table.Column<string>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    DtInput = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Grade", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "Cat_Section",
                columns: table => new
                {
                    SectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComId = table.Column<string>(nullable: true),
                    SectName = table.Column<string>(maxLength: 25, nullable: false),
                    SectNameB = table.Column<string>(maxLength: 25, nullable: true),
                    DeptId = table.Column<int>(nullable: true),
                    Slno = table.Column<int>(nullable: false),
                    PcName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(maxLength: 128, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    UpdateByUserId = table.Column<string>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    DtInput = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Section", x => x.SectId);
                    table.ForeignKey(
                        name: "FK_Cat_Section_Cat_Department_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Cat_Department",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cat_Designation",
                columns: table => new
                {
                    DesigId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComId = table.Column<string>(nullable: true),
                    DesigName = table.Column<string>(maxLength: 25, nullable: false),
                    DesigNameB = table.Column<string>(maxLength: 25, nullable: true),
                    SalaryRange = table.Column<string>(maxLength: 40, nullable: true),
                    SlNo = table.Column<int>(nullable: true),
                    Gsmin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GradeId = table.Column<int>(nullable: true),
                    AttBonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TtlManpower = table.Column<int>(nullable: true),
                    ProposedManPower = table.Column<int>(nullable: true),
                    PcName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    UpdateByUserId = table.Column<string>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    DtInput = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Designation", x => x.DesigId);
                    table.ForeignKey(
                        name: "FK_Cat_Designation_Cat_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Cat_Grade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cat_SubSection",
                columns: table => new
                {
                    SubSectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComId = table.Column<string>(nullable: true),
                    SubSectName = table.Column<string>(maxLength: 25, nullable: false),
                    SubSectNameB = table.Column<string>(nullable: true),
                    SectId = table.Column<int>(nullable: false),
                    DeptId = table.Column<int>(nullable: false),
                    Slno = table.Column<short>(nullable: false),
                    PcName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    UpdateByUserId = table.Column<string>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    DtInput = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_SubSection", x => x.SubSectId);
                    table.ForeignKey(
                        name: "FK_Cat_SubSection_Cat_Department_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Cat_Department",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cat_SubSection_Cat_Section_SectId",
                        column: x => x.SectId,
                        principalTable: "Cat_Section",
                        principalColumn: "SectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Designation_GradeId",
                table: "Cat_Designation",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Section_DeptId",
                table: "Cat_Section",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_SubSection_DeptId",
                table: "Cat_SubSection",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_SubSection_SectId",
                table: "Cat_SubSection",
                column: "SectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat_Designation");

            migrationBuilder.DropTable(
                name: "Cat_SubSection");

            migrationBuilder.DropTable(
                name: "Cat_Grade");

            migrationBuilder.DropTable(
                name: "Cat_Section");

            migrationBuilder.DropTable(
                name: "Cat_Department");
        }
    }
}
