using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationAndDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteCities",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteCities", x => new { x.UserId, x.CityId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteCities_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteCities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: true),
                    LoginType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_AuthProviders_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "AuthProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuthProviders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "google" },
                    { 2, "facebook" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 524901, "RU", "Moscow" },
                    { 1248991, "LK", "Colombo" },
                    { 1796236, "CN", "Shanghai" },
                    { 1816670, "CN", "Beijing" },
                    { 1835848, "KR", "Seoul" },
                    { 1850147, "JP", "Tokyo" },
                    { 1880252, "SG", "Singapore" },
                    { 2147714, "AU", "Sydney" },
                    { 2643743, "GB", "London" },
                    { 2644210, "GB", "Liverpool" },
                    { 2950159, "DE", "Berlin" },
                    { 2988507, "FR", "Paris" },
                    { 3128760, "ES", "Barcelona" },
                    { 3143244, "NO", "Oslo" },
                    { 3169070, "IT", "Rome" },
                    { 4930956, "US", "Boston" },
                    { 5128581, "US", "New York" },
                    { 5368361, "US", "Los Angeles" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteCities_CityId",
                table: "UserFavoriteCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_ProviderId",
                table: "UserLogins",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId_ProviderId_SubId",
                table: "UserLogins",
                columns: new[] { "UserId", "ProviderId", "SubId" },
                unique: true,
                filter: "[SubId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteCities");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AuthProviders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
