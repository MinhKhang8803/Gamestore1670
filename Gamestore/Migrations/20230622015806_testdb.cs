using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamestore.Migrations
{
    /// <inheritdoc />
    public partial class testdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Game_GameID",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "GameID",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Game_GameID",
                table: "OrderDetails",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Game_GameID",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "GameID",
                table: "OrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Game_GameID",
                table: "OrderDetails",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "GameID");
        }
    }
}
