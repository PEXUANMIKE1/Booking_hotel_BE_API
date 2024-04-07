using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    /// <inheritdoc />
    public partial class updatedbv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationsID",
                table: "Hotel_tbl",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations_tbl", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_tbl_LocationsID",
                table: "Hotel_tbl",
                column: "LocationsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_tbl_Locations_tbl_LocationsID",
                table: "Hotel_tbl",
                column: "LocationsID",
                principalTable: "Locations_tbl",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_tbl_Locations_tbl_LocationsID",
                table: "Hotel_tbl");

            migrationBuilder.DropTable(
                name: "Locations_tbl");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_tbl_LocationsID",
                table: "Hotel_tbl");

            migrationBuilder.DropColumn(
                name: "LocationsID",
                table: "Hotel_tbl");
        }
    }
}
