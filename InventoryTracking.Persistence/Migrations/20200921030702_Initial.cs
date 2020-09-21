using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryTracking.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedOn", "Name", "Quantity" },
                values: new object[] { 1, new DateTime(2020, 9, 20, 20, 7, 2, 90, DateTimeKind.Local).AddTicks(9874), "Apples", 3 });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedOn", "Name", "Quantity" },
                values: new object[] { 2, new DateTime(2020, 9, 20, 20, 7, 2, 92, DateTimeKind.Local).AddTicks(5731), "Oranges", 7 });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedOn", "Name", "Quantity" },
                values: new object[] { 3, new DateTime(2020, 9, 20, 20, 7, 2, 92, DateTimeKind.Local).AddTicks(5742), "Pomegranates", 55 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}
