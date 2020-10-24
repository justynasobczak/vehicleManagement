using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleManagementApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: false),
                    WeightFrom = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    WeightUpTo = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Icon = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<int>(nullable: false),
                    OwnerName = table.Column<string>(nullable: false),
                    ManufactureYear = table.Column<int>(nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(8, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Icon", "WeightFrom", "WeightUpTo" },
                values: new object[,]
                {
                    { 1, "Light", "Icon1", 0.00m, 500.00m },
                    { 2, "Medium", "Icon2", 500.00m, 2500.00m },
                    { 3, "Heavy", "Icon3", 2500.00m, 5000.00m }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "ManufactureYear", "Manufacturer", "OwnerName", "Weight" },
                values: new object[,]
                {
                    { 1, 2020, 3, "Owner1", 100.00m },
                    { 2, 1998, 2, "Owner2", 10.00m },
                    { 3, 1899, 4, "Owner3", 50.00m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
