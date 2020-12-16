using Microsoft.EntityFrameworkCore.Migrations;

namespace ESKOBApi.Migrations
{
    public partial class AddEmployeeNoToIdea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tenants_TenantId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Hashtags_Tenants_TenantId",
                table: "Hashtags");

            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Tenants_TenantId",
                table: "Ideas");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Tenants_TenantId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Ideas_IdeaId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tenants_TenantId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TenantId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_TenantId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Hashtags_TenantId",
                table: "Hashtags");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TenantId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Managers");

            migrationBuilder.AlterColumn<int>(
                name: "IdeaId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Employee",
                table: "Ideas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Tenants_TenantId",
                table: "Managers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Ideas_IdeaId",
                table: "Tasks",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Tenants_TenantId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Ideas_IdeaId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Employee",
                table: "Ideas");

            migrationBuilder.AlterColumn<int>(
                name: "IdeaId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Managers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TenantId",
                table: "Tasks",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tenants_TenantId",
                table: "Comments",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtags_Tenants_TenantId",
                table: "Hashtags",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Tenants_TenantId",
                table: "Ideas",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Tenants_TenantId",
                table: "Managers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Ideas_IdeaId",
                table: "Tasks",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tenants_TenantId",
                table: "Tasks",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
