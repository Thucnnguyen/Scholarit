using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarit.Migrations
{
    public partial class updateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UsersId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UsersId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UsersId",
                table: "Order",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UsersId",
                table: "Order",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
