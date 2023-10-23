using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarit.Migrations
{
    public partial class updateQuizAttemptQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "QuizAttemptQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttemptQuestion_UserId",
                table: "QuizAttemptQuestion",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttemptQuestion_User_UserId",
                table: "QuizAttemptQuestion",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttemptQuestion_User_UserId",
                table: "QuizAttemptQuestion");

            migrationBuilder.DropIndex(
                name: "IX_QuizAttemptQuestion_UserId",
                table: "QuizAttemptQuestion");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuizAttemptQuestion");
        }
    }
}
