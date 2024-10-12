using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class added_card_transaction_audit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "CreationTime",
            table: "CardTransactions",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "CreatorId",
            table: "CardTransactions",
            type: "uniqueidentifier",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreationTime",
            table: "CardTransactions");

        migrationBuilder.DropColumn(
            name: "CreatorId",
            table: "CardTransactions");
    }
}
