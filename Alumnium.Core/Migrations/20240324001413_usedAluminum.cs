using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumnium.Core.Migrations
{
    public partial class usedAluminum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumniumTypes_Items_UsedItemId",
                table: "AlumniumTypes");

            migrationBuilder.DropTable(
                name: "SiteUsedItems");

            migrationBuilder.DropIndex(
                name: "IX_AlumniumTypes_UsedItemId",
                table: "AlumniumTypes");

            migrationBuilder.DropColumn(
                name: "UsedItemId",
                table: "AlumniumTypes");

            migrationBuilder.AddColumn<int>(
                name: "DoorsNumber",
                table: "SiteRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WindowsNumber",
                table: "SiteRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlumniumTypeId",
                table: "AlumniumTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SiteUsedAlumimums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlmuniumTypeId = table.Column<int>(type: "int", nullable: false),
                    SiteRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUsedAlumimums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteUsedAlumimums_AlumniumTypes_AlmuniumTypeId",
                        column: x => x.AlmuniumTypeId,
                        principalTable: "AlumniumTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteUsedAlumimums_SiteRequests_SiteRequestId",
                        column: x => x.SiteRequestId,
                        principalTable: "SiteRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumniumTypes_AlumniumTypeId",
                table: "AlumniumTypes",
                column: "AlumniumTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUsedAlumimums_AlmuniumTypeId",
                table: "SiteUsedAlumimums",
                column: "AlmuniumTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUsedAlumimums_SiteRequestId",
                table: "SiteUsedAlumimums",
                column: "SiteRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlumniumTypes_AlumniumTypes_AlumniumTypeId",
                table: "AlumniumTypes",
                column: "AlumniumTypeId",
                principalTable: "AlumniumTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumniumTypes_AlumniumTypes_AlumniumTypeId",
                table: "AlumniumTypes");

            migrationBuilder.DropTable(
                name: "SiteUsedAlumimums");

            migrationBuilder.DropIndex(
                name: "IX_AlumniumTypes_AlumniumTypeId",
                table: "AlumniumTypes");

            migrationBuilder.DropColumn(
                name: "DoorsNumber",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "WindowsNumber",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "AlumniumTypeId",
                table: "AlumniumTypes");

            migrationBuilder.AddColumn<int>(
                name: "UsedItemId",
                table: "AlumniumTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SiteUsedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlmuniumTypeId = table.Column<int>(type: "int", nullable: false),
                    SiteRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUsedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteUsedItems_AlumniumTypes_AlmuniumTypeId",
                        column: x => x.AlmuniumTypeId,
                        principalTable: "AlumniumTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteUsedItems_SiteRequests_SiteRequestId",
                        column: x => x.SiteRequestId,
                        principalTable: "SiteRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumniumTypes_UsedItemId",
                table: "AlumniumTypes",
                column: "UsedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUsedItems_AlmuniumTypeId",
                table: "SiteUsedItems",
                column: "AlmuniumTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUsedItems_SiteRequestId",
                table: "SiteUsedItems",
                column: "SiteRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlumniumTypes_Items_UsedItemId",
                table: "AlumniumTypes",
                column: "UsedItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
