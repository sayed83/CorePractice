using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CorePractice.Data.Migrations
{
    public partial class Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessType",
                columns: table => new
                {
                    BusinessTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessTypeCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessType", x => x.BusinessTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    ComId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanySecretCode = table.Column<string>(maxLength: 128, nullable: false),
                    AppKey = table.Column<string>(maxLength: 128, nullable: false),
                    CompanyCode = table.Column<string>(maxLength: 128, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 80, nullable: false),
                    CompanyNameBangla = table.Column<string>(maxLength: 80, nullable: true),
                    CompanyShortName = table.Column<string>(maxLength: 20, nullable: false),
                    PrimaryAddress = table.Column<string>(maxLength: 300, nullable: false),
                    CompanyAddressBangla = table.Column<string>(maxLength: 300, nullable: true),
                    SecoundaryAddress = table.Column<string>(maxLength: 300, nullable: true),
                    comPhone = table.Column<string>(maxLength: 20, nullable: false),
                    comPhone2 = table.Column<string>(maxLength: 20, nullable: false),
                    comFax = table.Column<string>(maxLength: 20, nullable: true),
                    comEmail = table.Column<string>(maxLength: 20, nullable: false),
                    comWeb = table.Column<string>(maxLength: 80, nullable: true),
                    BusinessTypeId = table.Column<int>(nullable: false),
                    BaseComId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    DecimalField = table.Column<int>(nullable: false),
                    ContPerson = table.Column<string>(maxLength: 150, nullable: true),
                    ContDesig = table.Column<string>(maxLength: 150, nullable: true),
                    IsShowCurrencySymbol = table.Column<bool>(nullable: false),
                    IsInActive = table.Column<bool>(nullable: false),
                    IsGroup = table.Column<bool>(nullable: false),
                    isBarcode = table.Column<bool>(nullable: false),
                    isProduct = table.Column<bool>(nullable: false),
                    isCorporate = table.Column<bool>(nullable: false),
                    isPOSprint = table.Column<bool>(nullable: false),
                    IsService = table.Column<bool>(nullable: false),
                    isOldDue = table.Column<bool>(nullable: false),
                    isShortcutSale = table.Column<bool>(nullable: false),
                    isRestaurantSale = table.Column<bool>(nullable: false),
                    isTouch = table.Column<bool>(nullable: false),
                    isShoeSale = table.Column<bool>(nullable: false),
                    isIMEISale = table.Column<bool>(nullable: false),
                    isMultipleWh = table.Column<bool>(nullable: false),
                    isMultiCurrency = table.Column<bool>(nullable: false),
                    isMultiDebitCredit = table.Column<bool>(nullable: false),
                    isVoucherDistributionEntry = table.Column<bool>(nullable: false),
                    isChequeDetails = table.Column<bool>(nullable: false),
                    ComImageHeader = table.Column<byte[]>(nullable: true),
                    HeaderImagePath = table.Column<string>(nullable: true),
                    HeaderFileExtension = table.Column<string>(nullable: true),
                    ComLogo = table.Column<byte[]>(nullable: true),
                    LogoImagePath = table.Column<string>(nullable: true),
                    LogoFileExtension = table.Column<string>(nullable: true),
                    Addvertise = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.ComId);
                    table.ForeignKey(
                        name: "FK_Companys_BusinessType_BusinessTypeId",
                        column: x => x.BusinessTypeId,
                        principalTable: "BusinessType",
                        principalColumn: "BusinessTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companys_BusinessTypeId",
                table: "Companys",
                column: "BusinessTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropTable(
                name: "BusinessType");
        }
    }
}
