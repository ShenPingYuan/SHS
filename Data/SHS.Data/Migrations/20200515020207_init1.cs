using Microsoft.EntityFrameworkCore.Migrations;

namespace SHS.Data.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_teacher_tb_college_ColleageId",
                table: "tb_teacher");

            migrationBuilder.DropIndex(
                name: "IX_tb_teacher_ColleageId",
                table: "tb_teacher");

            migrationBuilder.DropColumn(
                name: "ColleageId",
                table: "tb_teacher");

            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "tb_teacher",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_teacher_CollegeId",
                table: "tb_teacher",
                column: "CollegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_teacher_tb_college_CollegeId",
                table: "tb_teacher",
                column: "CollegeId",
                principalTable: "tb_college",
                principalColumn: "CollegeId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_teacher_tb_college_CollegeId",
                table: "tb_teacher");

            migrationBuilder.DropIndex(
                name: "IX_tb_teacher_CollegeId",
                table: "tb_teacher");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "tb_teacher");

            migrationBuilder.AddColumn<int>(
                name: "ColleageId",
                table: "tb_teacher",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_teacher_ColleageId",
                table: "tb_teacher",
                column: "ColleageId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_teacher_tb_college_ColleageId",
                table: "tb_teacher",
                column: "ColleageId",
                principalTable: "tb_college",
                principalColumn: "CollegeId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
