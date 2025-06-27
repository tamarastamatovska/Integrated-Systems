using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieEvent.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedScreeningWithDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ScreeningDate",
                table: "Screenings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScreeningDate",
                table: "Screenings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
