using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProjeKampi.WebApi.Migrations
{
    public partial class UpdateGroupReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResponsiblePersonName",
                table: "GroupReservations",
                newName: "ResponsibleCustomerName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "GroupReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonCount",
                table: "GroupReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "GroupReservations");

            migrationBuilder.DropColumn(
                name: "PersonCount",
                table: "GroupReservations");

            migrationBuilder.RenameColumn(
                name: "ResponsibleCustomerName",
                table: "GroupReservations",
                newName: "ResponsiblePersonName");
        }
    }
}
