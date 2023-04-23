using Microsoft.EntityFrameworkCore.Migrations;

namespace loginRegister.Migrations
{
    public partial class addconfirmtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "emailConfirmed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emailConfirmed",
                table: "Users");
        }
    }
}
