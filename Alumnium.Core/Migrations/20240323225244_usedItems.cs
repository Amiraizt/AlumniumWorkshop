using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumnium.Core.Migrations
{
    public partial class usedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteRequests_AlumniumTypes_AlmuniumTypeId",
                table: "SiteRequests");

            migrationBuilder.DropIndex(
                name: "IX_SiteRequests_AlmuniumTypeId",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "AlmuniumTypeId",
                table: "SiteRequests");

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
                name: "IX_SiteUsedItems_AlmuniumTypeId",
                table: "SiteUsedItems",
                column: "AlmuniumTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUsedItems_SiteRequestId",
                table: "SiteUsedItems",
                column: "SiteRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteUsedItems");

            migrationBuilder.AddColumn<int>(
                name: "AlmuniumTypeId",
                table: "SiteRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SiteRequests_AlmuniumTypeId",
                table: "SiteRequests",
                column: "AlmuniumTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteRequests_AlumniumTypes_AlmuniumTypeId",
                table: "SiteRequests",
                column: "AlmuniumTypeId",
                principalTable: "AlumniumTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
