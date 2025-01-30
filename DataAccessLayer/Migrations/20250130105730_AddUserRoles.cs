using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_SeatId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SeatId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeatId1",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26eeb29f-7bc2-4fae-a926-a59fc11c5dfe", null, "User", "USER" },
                    { "bb815bab-2485-47b1-b460-0639a8ba967b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26eeb29f-7bc2-4fae-a926-a59fc11c5dfe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb815bab-2485-47b1-b460-0639a8ba967b");

            migrationBuilder.AddColumn<int>(
                name: "SeatId1",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId1",
                table: "Tickets",
                column: "SeatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_SeatId1",
                table: "Tickets",
                column: "SeatId1",
                principalTable: "Seats",
                principalColumn: "id");
        }
    }
}
