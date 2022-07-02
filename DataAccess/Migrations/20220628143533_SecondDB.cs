using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SecondDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_MemberId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "MemberId1",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_MemberId1",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "MemberId1");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_MemberId1");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_MemberId1",
                table: "Orders",
                column: "MemberId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
