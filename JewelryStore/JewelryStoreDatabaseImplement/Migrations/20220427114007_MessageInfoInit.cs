using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JewelryStoreDatabaseImplement.Migrations
{
    public partial class MessageInfoInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessagesInfo",
                columns: table => new
                {
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDelivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesInfo", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_MessagesInfo_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessagesInfo_ClientId",
                table: "MessagesInfo",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagesInfo");
        }
    }
}
