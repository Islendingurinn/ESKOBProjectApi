using Microsoft.EntityFrameworkCore.Migrations;

namespace ESKOBApi.Migrations
{
    public partial class ManagerEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Displayname",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Managers",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Managers",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "Displayname",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
