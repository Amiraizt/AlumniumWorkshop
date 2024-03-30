using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumnium.Core.Migrations
{
    public partial class newTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumniumTypes_AlumniumTypes_AlumniumTypeId",
                table: "AlumniumTypes");

            migrationBuilder.DropIndex(
                name: "IX_AlumniumTypes_AlumniumTypeId",
                table: "AlumniumTypes");

            migrationBuilder.DropColumn(
                name: "AlumniumTypeId",
                table: "AlumniumTypes");

            migrationBuilder.CreateTable(
                name: "AluminumUsedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    AluminumTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AluminumUsedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AluminumUsedItems_AlumniumTypes_AluminumTypeId",
                        column: x => x.AluminumTypeId,
                        principalTable: "AlumniumTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AluminumUsedItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AluminumUsedItems_AluminumTypeId",
                table: "AluminumUsedItems",
                column: "AluminumTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AluminumUsedItems_ItemId",
                table: "AluminumUsedItems",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AluminumUsedItems");

            migrationBuilder.AddColumn<int>(
                name: "AlumniumTypeId",
                table: "AlumniumTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlumniumTypes_AlumniumTypeId",
                table: "AlumniumTypes",
                column: "AlumniumTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlumniumTypes_AlumniumTypes_AlumniumTypeId",
                table: "AlumniumTypes",
                column: "AlumniumTypeId",
                principalTable: "AlumniumTypes",
                principalColumn: "Id");
        }
    }
}
