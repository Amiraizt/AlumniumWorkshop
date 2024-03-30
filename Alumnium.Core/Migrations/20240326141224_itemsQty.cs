using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumnium.Core.Migrations
{
    public partial class itemsQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemQuantity",
                table: "AluminumUsedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemQuantity",
                table: "AluminumUsedItems");
        }
    }
}
