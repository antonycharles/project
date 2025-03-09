using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Family.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbAppCallbackAndFavicon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CallbackUrl",
                table: "Apps",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaviconUrl",
                table: "Apps",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallbackUrl",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "FaviconUrl",
                table: "Apps");
        }
    }
}
