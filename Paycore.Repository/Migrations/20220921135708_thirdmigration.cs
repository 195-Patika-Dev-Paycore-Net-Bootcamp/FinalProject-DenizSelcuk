using Microsoft.EntityFrameworkCore.Migrations;

namespace Paycore.Repository.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRefused",
                table: "Offers");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Offers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Offers");

            migrationBuilder.AddColumn<bool>(
                name: "IsRefused",
                table: "Offers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
