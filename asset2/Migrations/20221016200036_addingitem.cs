using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asset2.Migrations
{
    public partial class addingitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    purchase_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    modelname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    office = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    todayprice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");
        }
    }
}
