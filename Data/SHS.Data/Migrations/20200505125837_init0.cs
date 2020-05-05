using Microsoft.EntityFrameworkCore.Migrations;

namespace SHS.Data.Migrations
{
    public partial class init0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "IdentityUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NickName",
                table: "IdentityUser");
        }
    }
}
