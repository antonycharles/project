using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Family.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbAppAddType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Apps",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Apps");
        }
    }
}
