using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketStore.Data.Migrations
{
    public partial class AddEventOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Organizer",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organizer",
                table: "Events");
        }
    }
}
