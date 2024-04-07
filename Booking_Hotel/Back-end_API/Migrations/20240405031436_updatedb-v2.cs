using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    /// <inheritdoc />
    public partial class updatedbv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtraServiceID",
                table: "Bookings_tbl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExtraServicesID",
                table: "Bookings_tbl",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExtraServices_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meals = table.Column<bool>(type: "bit", nullable: false),
                    Spa = table.Column<bool>(type: "bit", nullable: false),
                    CarRental = table.Column<bool>(type: "bit", nullable: false),
                    Maximum = table.Column<bool>(type: "bit", nullable: false),
                    Parking = table.Column<bool>(type: "bit", nullable: false),
                    TourGuide = table.Column<bool>(type: "bit", nullable: false),
                    Gym = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraServices_tbl", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_tbl_ExtraServicesID",
                table: "Bookings_tbl",
                column: "ExtraServicesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_tbl_ExtraServices_tbl_ExtraServicesID",
                table: "Bookings_tbl",
                column: "ExtraServicesID",
                principalTable: "ExtraServices_tbl",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_tbl_ExtraServices_tbl_ExtraServicesID",
                table: "Bookings_tbl");

            migrationBuilder.DropTable(
                name: "ExtraServices_tbl");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_tbl_ExtraServicesID",
                table: "Bookings_tbl");

            migrationBuilder.DropColumn(
                name: "ExtraServiceID",
                table: "Bookings_tbl");

            migrationBuilder.DropColumn(
                name: "ExtraServicesID",
                table: "Bookings_tbl");
        }
    }
}
