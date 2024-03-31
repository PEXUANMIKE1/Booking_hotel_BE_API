using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_tbl_Hotel_tbl_HotelID",
                table: "Booking_tbl");

            migrationBuilder.RenameColumn(
                name: "HotelID",
                table: "Booking_tbl",
                newName: "RoomID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_tbl_HotelID",
                table: "Booking_tbl",
                newName: "IX_Booking_tbl_RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_tbl_Room_tbl_RoomID",
                table: "Booking_tbl",
                column: "RoomID",
                principalTable: "Room_tbl",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_tbl_Room_tbl_RoomID",
                table: "Booking_tbl");

            migrationBuilder.RenameColumn(
                name: "RoomID",
                table: "Booking_tbl",
                newName: "HotelID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_tbl_RoomID",
                table: "Booking_tbl",
                newName: "IX_Booking_tbl_HotelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_tbl_Hotel_tbl_HotelID",
                table: "Booking_tbl",
                column: "HotelID",
                principalTable: "Hotel_tbl",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
