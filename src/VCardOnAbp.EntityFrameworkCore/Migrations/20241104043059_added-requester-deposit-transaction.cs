﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class addedrequesterdeposittransaction : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "Requester",
            table: "DepositTransactions",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Requester",
            table: "DepositTransactions");
    }
}
