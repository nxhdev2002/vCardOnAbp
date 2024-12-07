﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class addeddeposittrans : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "DepositTransactions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TransactionStatus = table.Column<int>(type: "int", nullable: false),
                ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                table.PrimaryKey("PK_DepositTransactions", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DepositTransactions");
    }
}
