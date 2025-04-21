using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Family.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbAppAddCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Apps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Apps_Code",
                table: "Apps",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apps_Code",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Apps");
        }
    }
}
