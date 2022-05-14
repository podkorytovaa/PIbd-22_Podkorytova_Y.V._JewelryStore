using Microsoft.EntityFrameworkCore.Migrations;

namespace JewelryStoreDatabaseImplement.Migrations
{
    public partial class MessageReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "MessagesInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReplyText",
                table: "MessagesInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checked",
                table: "MessagesInfo");

            migrationBuilder.DropColumn(
                name: "ReplyText",
                table: "MessagesInfo");
        }
    }
}
