using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarit.Migrations
{
    public partial class updateEnrollEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Enroll",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Enroll");
        }
    }
}
