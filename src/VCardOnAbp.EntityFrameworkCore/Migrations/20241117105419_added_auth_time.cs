using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class added_auth_time : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "AuthTime",
            table: "CardTransactions",
            type: "datetime2",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "AuthTime",
            table: "CardTransactions");
    }
}
