using Microsoft.EntityFrameworkCore.Migrations;

namespace ESKOBApi.Migrations
{
    public partial class AddHashtagsToIdeas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hashtags_Ideas_IdeaId",
                table: "Hashtags");

            migrationBuilder.DropForeignKey(
                name: "FK_Hashtags_Tasks_TaskId",
                table: "Hashtags");

            migrationBuilder.DropIndex(
                name: "IX_Hashtags_TaskId",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Hashtags");

            migrationBuilder.AlterColumn<int>(
                name: "IdeaId",
                table: "Hashtags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtags_Ideas_IdeaId",
                table: "Hashtags",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hashtags_Ideas_IdeaId",
                table: "Hashtags");

            migrationBuilder.AlterColumn<int>(
                name: "IdeaId",
                table: "Hashtags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Hashtags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hashtags_TaskId",
                table: "Hashtags",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtags_Ideas_IdeaId",
                table: "Hashtags",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtags_Tasks_TaskId",
                table: "Hashtags",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
