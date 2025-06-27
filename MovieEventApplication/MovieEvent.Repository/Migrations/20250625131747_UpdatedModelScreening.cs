using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieEvent.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModelScreening : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservedSeats",
                table: "Screenings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScreeningDate",
                table: "Screenings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalSeats",
                table: "Screenings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedSeats",
                table: "Screenings");

            migrationBuilder.DropColumn(
                name: "ScreeningDate",
                table: "Screenings");

            migrationBuilder.DropColumn(
                name: "TotalSeats",
                table: "Screenings");
        }
    }
}
