using Microsoft.EntityFrameworkCore.Migrations;

namespace Truck.Management.Test.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(maxLength: 100, nullable: false),
                    TruckModel = table.Column<int>(nullable: false),
                    YearManufacture = table.Column<int>(nullable: false),
                    YearModel = table.Column<int>(nullable: false),
                    DigitalPanel = table.Column<bool>(nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Truck");
        }
    }
}
