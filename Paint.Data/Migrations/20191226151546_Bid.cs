using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Paint.Data.Migrations
{
    public partial class Bid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    ImageName = table.Column<string>(maxLength: 50, nullable: true),
                    Logo = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LowCoverage = table.Column<int>(nullable: false),
                    HiCoverage = table.Column<int>(nullable: false),
                    GallonPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FiveGallonPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: true),
                    ClientTypeId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: true),
                    BillingAddress_Line1 = table.Column<string>(maxLength: 255, nullable: true),
                    BillingAddress_Line2 = table.Column<string>(maxLength: 255, nullable: true),
                    BillingAddress_City = table.Column<string>(maxLength: 50, nullable: true),
                    BillingAddress_State = table.Column<string>(maxLength: 2, nullable: true),
                    BillingAddress_ZipCode = table.Column<string>(maxLength: 10, nullable: true),
                    BillingAddress_Latitude = table.Column<double>(maxLength: 13, nullable: true),
                    BillingAddress_Longitude = table.Column<double>(nullable: true),
                    Notes = table.Column<string>(type: "ntext", nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_ClientType_ClientTypeId",
                        column: x => x.ClientTypeId,
                        principalTable: "ClientType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Clients_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Address_Line1 = table.Column<string>(maxLength: 255, nullable: true),
                    Address_Line2 = table.Column<string>(maxLength: 255, nullable: true),
                    Address_City = table.Column<string>(maxLength: 50, nullable: true),
                    Address_State = table.Column<string>(maxLength: 2, nullable: true),
                    Address_ZipCode = table.Column<string>(maxLength: 10, nullable: true),
                    Address_Latitude = table.Column<double>(maxLength: 13, nullable: true),
                    Address_Longitude = table.Column<double>(nullable: true),
                    SquareFootage = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    RenovationBudget = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    PhoneNumberTypeId = table.Column<int>(nullable: false),
                    PhoneNumberType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => new { x.ClientId, x.Id });
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BidSheet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    ProgjectManager = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    SquareFoot = table.Column<string>(nullable: true),
                    BedBath = table.Column<string>(nullable: true),
                    LockBox = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    RenoTotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidSheet_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaintList",
                columns: table => new
                {
                    JobId = table.Column<int>(nullable: false),
                    CeilingId = table.Column<int>(nullable: false),
                    TrimId = table.Column<int>(nullable: false),
                    WallId = table.Column<int>(nullable: false),
                    TaxRate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaintList", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_PaintList_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DoorsSF = table.Column<int>(nullable: false),
                    WindowSF = table.Column<int>(nullable: false),
                    BaseBoardHeight = table.Column<int>(nullable: false),
                    ChairRailHeight = table.Column<int>(nullable: false),
                    CrownMoldingHeight = table.Column<int>(nullable: false),
                    JobId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BidArea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidSheetId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidArea_BidSheet_BidSheetId",
                        column: x => x.BidSheetId,
                        principalTable: "BidSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BidItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidAreaId = table.Column<int>(nullable: false),
                    Sub = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidItem_BidArea_BidAreaId",
                        column: x => x.BidAreaId,
                        principalTable: "BidArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClientType",
                columns: new[] { "Id", "ImageName", "Logo", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "REO" },
                    { 2, null, null, "Comercial" },
                    { 3, null, null, "Residential" }
                });

            migrationBuilder.InsertData(
                table: "Paints",
                columns: new[] { "Id", "FiveGallonPrice", "GallonPrice", "Grade", "HiCoverage", "LowCoverage", "Name", "Type" },
                values: new object[,]
                {
                    { 1, 44.98m, 11.98m, 1, 400, 300, "SPEED-WALL Flat", 1 },
                    { 2, 70.98m, 19.98m, 2, 400, 350, "PPG ULTRA-HIDE Zero Flat", 1 },
                    { 3, 0m, 21.98m, 1, 400, 300, "GLIDDEN ESSENTIALS SG", 2 },
                    { 4, 89.98m, 19.98m, 2, 400, 350, "PPG Ultra-Hide Zero SG", 2 },
                    { 5, 102m, 22.98m, 3, 400, 400, "Glidden Premium SG", 2 },
                    { 6, 121m, 25.98m, 4, 400, 300, "PPG DIAMOND Eggshell", 2 },
                    { 7, 168m, 36.98m, 4, 400, 400, "PPG TIMELESS Eggshell", 2 },
                    { 8, 76.98m, 14.98m, 1, 400, 400, "GLIDDEN ESSENTIALS Eggshell", 3 },
                    { 9, 79.98m, 17.98m, 2, 400, 350, "PPG Ultra-Hide Zero Eggshell", 3 },
                    { 10, 97.98m, 20.98m, 3, 400, 400, "Glidden Premium Eggshell", 3 },
                    { 11, 112m, 25.98m, 4, 400, 350, "PPG DIAMOND Eggshell", 3 },
                    { 12, 159m, 34.98m, 4, 400, 400, "PPG TIMELESS Eggshell", 3 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Active", "ClientTypeId", "CompanyName", "FirstName", "LastName", "Notes", "ParentId" },
                values: new object[] { 1, true, 1, "OfferPad", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Active", "ClientTypeId", "CompanyName", "FirstName", "LastName", "Notes", "ParentId" },
                values: new object[] { 2, true, 1, "OfferPad", null, null, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BidArea_BidSheetId",
                table: "BidArea",
                column: "BidSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_BidItem_BidAreaId",
                table: "BidItem",
                column: "BidAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_BidSheet_JobId",
                table: "BidSheet",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientTypeId",
                table: "Clients",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ParentId",
                table: "Clients",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ClientId",
                table: "Jobs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_JobId",
                table: "Room",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidItem");

            migrationBuilder.DropTable(
                name: "PaintList");

            migrationBuilder.DropTable(
                name: "Paints");

            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "BidArea");

            migrationBuilder.DropTable(
                name: "BidSheet");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ClientType");
        }
    }
}
