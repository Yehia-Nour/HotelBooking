using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToOneRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Reservations_ReservationID",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ReservationID",
                table: "Feedbacks");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ReservationID",
                table: "Feedbacks",
                column: "ReservationID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Reservations_ReservationID",
                table: "Feedbacks",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Reservations_ReservationID",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ReservationID",
                table: "Feedbacks");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ReservationID",
                table: "Feedbacks",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Reservations_ReservationID",
                table: "Feedbacks",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
