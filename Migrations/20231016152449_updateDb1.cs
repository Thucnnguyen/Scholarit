using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarit.Migrations
{
    public partial class updateDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Intro",
                table: "Chapter",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Chapter",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intro",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Chapter");
        }
    }
}
