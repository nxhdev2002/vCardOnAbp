using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class added_currency : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "CurrencyId",
            table: "Bins",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateTable(
            name: "Currency",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CurrencySymbol = table.Column<string>(type: "nvarchar(1)", nullable: false),
                UsdRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Currency", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "userCurrencies",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_userCurrencies", x => x.Id);
                table.ForeignKey(
                    name: "FK_userCurrencies_Currency_CurrencyId",
                    column: x => x.CurrencyId,
                    principalTable: "Currency",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Bins_CurrencyId",
            table: "Bins",
            column: "CurrencyId");

        migrationBuilder.CreateIndex(
            name: "IX_userCurrencies_CurrencyId",
            table: "userCurrencies",
            column: "CurrencyId");

        migrationBuilder.AddForeignKey(
            name: "FK_Bins_Currency_CurrencyId",
            table: "Bins",
            column: "CurrencyId",
            principalTable: "Currency",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Bins_Currency_CurrencyId",
            table: "Bins");

        migrationBuilder.DropTable(
            name: "userCurrencies");

        migrationBuilder.DropTable(
            name: "Currency");

        migrationBuilder.DropIndex(
            name: "IX_Bins_CurrencyId",
            table: "Bins");

        migrationBuilder.DropColumn(
            name: "CurrencyId",
            table: "Bins");
    }
}
