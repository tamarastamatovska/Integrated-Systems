using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieEvent.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCinemaHallToScreeningModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CinemaHall",
                table: "Screenings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CinemaHall",
                table: "Screenings");
        }
    }
}
