using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DutchTreat.Data.Migrations
{
    /// <inheritdoc />
    public partial class PropertiesNameChangedToMatchRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QorC",
                table: "Contacts",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Contacts",
                newName: "DateCreated");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Contacts",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Contacts",
                newName: "QorC");
        }
    }
}
