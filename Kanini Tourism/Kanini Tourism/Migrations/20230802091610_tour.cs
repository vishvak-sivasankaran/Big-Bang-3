using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanini_Tourism.Migrations
{
    /// <inheritdoc />
    public partial class tour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dummy",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_Number = table.Column<double>(type: "float", nullable: false),
                    Agency_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dummy", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ImageGallery",
                columns: table => new
                {
                    Image = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGallery", x => x.Image);
                });

            migrationBuilder.CreateTable(
                name: "Imagetbls",
                columns: table => new
                {
                    Imgid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imgname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagetbls", x => x.Imgid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_Number = table.Column<double>(type: "float", nullable: false),
                    Agency_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Daytwo",
                columns: table => new
                {
                    Pack_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pack_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Cost_per_person = table.Column<int>(type: "int", nullable: false),
                    Day1_Locations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day2_Locations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day1_Hotel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hotel_Image1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day2_Hotel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hotel_Image2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day1_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day2_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<double>(type: "float", nullable: false),
                    Agency_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daytwo", x => x.Pack_Id);
                    table.ForeignKey(
                        name: "FK_Daytwo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TourPackages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceForAdult = table.Column<int>(type: "int", nullable: false),
                    PriceForChild = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPackages", x => x.PackageId);
                    table.ForeignKey(
                        name: "FK_TourPackages_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adult = table.Column<int>(type: "int", nullable: false),
                    Child = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    TourPackagePackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_TourPackages_TourPackagePackageId",
                        column: x => x.TourPackagePackageId,
                        principalTable: "TourPackages",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    TourpackagePackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotels_TourPackages_TourpackagePackageId",
                        column: x => x.TourpackagePackageId,
                        principalTable: "TourPackages",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "Restaurents",
                columns: table => new
                {
                    RestaurentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurentImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    TourpackagePackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurents", x => x.RestaurentId);
                    table.ForeignKey(
                        name: "FK_Restaurents_TourPackages_TourpackagePackageId",
                        column: x => x.TourpackagePackageId,
                        principalTable: "TourPackages",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "Spots",
                columns: table => new
                {
                    SpotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpotImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    TourpackagePackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spots", x => x.SpotId);
                    table.ForeignKey(
                        name: "FK_Spots_TourPackages_TourpackagePackageId",
                        column: x => x.TourpackagePackageId,
                        principalTable: "TourPackages",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourPackagePackageId",
                table: "Bookings",
                column: "TourPackagePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Daytwo_UserId",
                table: "Daytwo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_TourpackagePackageId",
                table: "Hotels",
                column: "TourpackagePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurents_TourpackagePackageId",
                table: "Restaurents",
                column: "TourpackagePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Spots_TourpackagePackageId",
                table: "Spots",
                column: "TourpackagePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPackages_UserId1",
                table: "TourPackages",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Daytwo");

            migrationBuilder.DropTable(
                name: "Dummy");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "ImageGallery");

            migrationBuilder.DropTable(
                name: "Imagetbls");

            migrationBuilder.DropTable(
                name: "Restaurents");

            migrationBuilder.DropTable(
                name: "Spots");

            migrationBuilder.DropTable(
                name: "TourPackages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
