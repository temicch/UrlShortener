using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations;

public partial class Init : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ShortLinks",
            columns: table => new {
                Id = table.Column<string>(type: "TEXT", nullable: false),
                LinkFull = table.Column<string>(type: "TEXT", nullable: true),
                LinkShort = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ShortLinks", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ShortLinks");
    }
}
