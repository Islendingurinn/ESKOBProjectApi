using Microsoft.EntityFrameworkCore.Migrations;

namespace ESKOBApi.Migrations
{
    public partial class TenantSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Managers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Ideas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Hashtags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Added_Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TenantId",
                table: "Tasks",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_TenantId",
                table: "Managers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_TenantId",
                table: "Ideas",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashtags_TenantId",
                table: "Hashtags",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TenantId",
                table: "Comments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TenantId",
                table: "Attachments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Added_Users_TenantId",
                table: "Added_Users",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Added_Users_Tenant_TenantId",
                table: "Added_Users",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Tenant_TenantId",
                table: "Attachments",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tenant_TenantId",
                table: "Comments",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtags_Tenant_TenantId",
                table: "Hashtags",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Tenant_TenantId",
                table: "Ideas",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Tenant_TenantId",
                table: "Managers",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tenant_TenantId",
                table: "Tasks",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Added_Users_Tenant_TenantId",
                table: "Added_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Tenant_TenantId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tenant_TenantId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Hashtags_Tenant_TenantId",
                table: "Hashtags");

            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Tenant_TenantId",
                table: "Ideas");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Tenant_TenantId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tenant_TenantId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TenantId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Managers_TenantId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_TenantId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Hashtags_TenantId",
                table: "Hashtags");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TenantId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_TenantId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Added_Users_TenantId",
                table: "Added_Users");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Added_Users");
        }
    }
}
