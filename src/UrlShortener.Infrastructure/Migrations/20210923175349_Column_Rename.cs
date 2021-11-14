using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations;

public partial class Column_Rename : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "LinkShort",
            table: "ShortLinks",
            newName: "Link");

        migrationBuilder.RenameColumn(
            name: "LinkFull",
            table: "ShortLinks",
            newName: "Alias");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Link",
            table: "ShortLinks",
            newName: "LinkShort");

        migrationBuilder.RenameColumn(
            name: "Alias",
            table: "ShortLinks",
            newName: "LinkFull");
    }
}
