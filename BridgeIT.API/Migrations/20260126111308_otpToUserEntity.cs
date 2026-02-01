using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeIT.API.Migrations
{
    /// <inheritdoc />
    public partial class otpToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "otpCcode",
                table: "User",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "otpCreatedAt",
                table: "User",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "otpExpiresAt",
                table: "User",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "otpType",
                table: "User",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "otpCcode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "otpCreatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "otpExpiresAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "otpType",
                table: "User");
        }
    }
}
