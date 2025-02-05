using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Family.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbProfileApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppId",
                table: "Profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AppId",
                table: "Profiles",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Apps_AppId",
                table: "Profiles",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Apps_AppId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_AppId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "Profiles");
        }
    }
}
