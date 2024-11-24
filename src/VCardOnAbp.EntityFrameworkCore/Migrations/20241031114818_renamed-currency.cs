using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class renamedcurrency : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "CurrencySymbol",
            table: "Currencies",
            newName: "Symbol");

        migrationBuilder.RenameColumn(
            name: "CurrencyName",
            table: "Currencies",
            newName: "Name");

        migrationBuilder.RenameColumn(
            name: "CurrencyCode",
            table: "Currencies",
            newName: "Code");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Symbol",
            table: "Currencies",
            newName: "CurrencySymbol");

        migrationBuilder.RenameColumn(
            name: "Name",
            table: "Currencies",
            newName: "CurrencyName");

        migrationBuilder.RenameColumn(
            name: "Code",
            table: "Currencies",
            newName: "CurrencyCode");
    }
}
