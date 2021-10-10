using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations
{
    public partial class Link_Required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkClicks_ShortLinks_LinkId",
                table: "LinkClicks");

            migrationBuilder.AlterColumn<string>(
                name: "LinkId",
                table: "LinkClicks",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkClicks_ShortLinks_LinkId",
                table: "LinkClicks",
                column: "LinkId",
                principalTable: "ShortLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkClicks_ShortLinks_LinkId",
                table: "LinkClicks");

            migrationBuilder.AlterColumn<string>(
                name: "LinkId",
                table: "LinkClicks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkClicks_ShortLinks_LinkId",
                table: "LinkClicks",
                column: "LinkId",
                principalTable: "ShortLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
