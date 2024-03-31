using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotel_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hotline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceLow = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel_tbl", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_tbl", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoomType_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType_tbl", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_tbl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_tbl_Role_tbl_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MaxPerson = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room_tbl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Room_tbl_Hotel_tbl_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotel_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_tbl_RoomType_tbl_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomType_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    PayMethod = table.Column<int>(type: "int", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking_tbl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Booking_tbl_Hotel_tbl_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotel_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_tbl_User_tbl_UserID",
                        column: x => x.UserID,
                        principalTable: "User_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<int>(type: "int", nullable: false),
                    CommentDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments_tbl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_tbl_Hotel_tbl_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotel_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_tbl_User_tbl_UserID",
                        column: x => x.UserID,
                        principalTable: "User_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmEmail_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeActive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmEmail_tbl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConfirmEmail_tbl_User_tbl_UserId",
                        column: x => x.UserId,
                        principalTable: "User_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<int>(type: "int", nullable: false),
                    PostDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts_tbl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Posts_tbl_User_tbl_UserID",
                        column: x => x.UserID,
                        principalTable: "User_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefeshToken_tbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefeshToken_tbl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RefeshToken_tbl_User_tbl_UserId",
                        column: x => x.UserId,
                        principalTable: "User_tbl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_tbl_HotelID",
                table: "Booking_tbl",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_tbl_UserID",
                table: "Booking_tbl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_tbl_HotelID",
                table: "Comments_tbl",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_tbl_UserID",
                table: "Comments_tbl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmEmail_tbl_UserId",
                table: "ConfirmEmail_tbl",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_tbl_UserID",
                table: "Posts_tbl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RefeshToken_tbl_UserId",
                table: "RefeshToken_tbl",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_tbl_HotelID",
                table: "Room_tbl",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Room_tbl_RoomTypeID",
                table: "Room_tbl",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_User_tbl_RoleId",
                table: "User_tbl",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking_tbl");

            migrationBuilder.DropTable(
                name: "Comments_tbl");

            migrationBuilder.DropTable(
                name: "ConfirmEmail_tbl");

            migrationBuilder.DropTable(
                name: "Posts_tbl");

            migrationBuilder.DropTable(
                name: "RefeshToken_tbl");

            migrationBuilder.DropTable(
                name: "Room_tbl");

            migrationBuilder.DropTable(
                name: "User_tbl");

            migrationBuilder.DropTable(
                name: "Hotel_tbl");

            migrationBuilder.DropTable(
                name: "RoomType_tbl");

            migrationBuilder.DropTable(
                name: "Role_tbl");
        }
    }
}
