using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumnium.Core.Migrations
{
    public partial class siteusedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteUsedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    UsedQuantity = table.Column<int>(type: "int", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUsedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteUsedItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteUsedItems_SiteRequests_SiteId",
                        column: x => x.SiteId,
                        principalTable: "SiteRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteUsedItems_ItemId",
                table: "SiteUsedItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUsedItems_SiteId",
                table: "SiteUsedItems",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteUsedItems");
        }
    }
}
