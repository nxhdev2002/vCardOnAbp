﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class added_card_lastview : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "LastView",
            table: "Cards",
            type: "datetime2",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "LastView",
            table: "Cards");
    }
}
