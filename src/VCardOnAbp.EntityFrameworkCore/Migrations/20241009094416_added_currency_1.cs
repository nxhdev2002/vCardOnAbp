using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class added_currency_1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Bins_Currency_CurrencyId",
            table: "Bins");

        migrationBuilder.DropForeignKey(
            name: "FK_userCurrencies_Currency_CurrencyId",
            table: "userCurrencies");

        migrationBuilder.DropTable(
            name: "Currency");

        migrationBuilder.DropIndex(
            name: "IX_userCurrencies_CurrencyId",
            table: "userCurrencies");

        migrationBuilder.DropIndex(
            name: "IX_Bins_CurrencyId",
            table: "Bins");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Currency",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CurrencySymbol = table.Column<string>(type: "nvarchar(1)", nullable: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UsdRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Currency", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_userCurrencies_CurrencyId",
            table: "userCurrencies",
            column: "CurrencyId");

        migrationBuilder.CreateIndex(
            name: "IX_Bins_CurrencyId",
            table: "Bins",
            column: "CurrencyId");

        migrationBuilder.AddForeignKey(
            name: "FK_Bins_Currency_CurrencyId",
            table: "Bins",
            column: "CurrencyId",
            principalTable: "Currency",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_userCurrencies_Currency_CurrencyId",
            table: "userCurrencies",
            column: "CurrencyId",
            principalTable: "Currency",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
