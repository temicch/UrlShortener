using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations;

public partial class Alias_IsUnique : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_ShortLinks_Alias",
            table: "ShortLinks",
            column: "Alias",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_ShortLinks_Alias",
            table: "ShortLinks");
    }
}
