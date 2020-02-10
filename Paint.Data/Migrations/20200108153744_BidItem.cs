using Microsoft.EntityFrameworkCore.Migrations;

namespace Paint.Data.Migrations
{
    public partial class BidItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidItem_BidArea_BidAreaId",
                table: "BidItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BidItem",
                table: "BidItem");

            migrationBuilder.RenameTable(
                name: "BidItem",
                newName: "BidItems");

            migrationBuilder.RenameIndex(
                name: "IX_BidItem_BidAreaId",
                table: "BidItems",
                newName: "IX_BidItems_BidAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BidItems",
                table: "BidItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BidItems_BidArea_BidAreaId",
                table: "BidItems",
                column: "BidAreaId",
                principalTable: "BidArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidItems_BidArea_BidAreaId",
                table: "BidItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BidItems",
                table: "BidItems");

            migrationBuilder.RenameTable(
                name: "BidItems",
                newName: "BidItem");

            migrationBuilder.RenameIndex(
                name: "IX_BidItems_BidAreaId",
                table: "BidItem",
                newName: "IX_BidItem_BidAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BidItem",
                table: "BidItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BidItem_BidArea_BidAreaId",
                table: "BidItem",
                column: "BidAreaId",
                principalTable: "BidArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
