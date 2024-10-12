using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class added_bin_fee : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "CreationFixedFee",
            table: "Bins",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<decimal>(
            name: "CreationPercentFee",
            table: "Bins",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<decimal>(
            name: "FundingFixedFee",
            table: "Bins",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<decimal>(
            name: "FundingPercentFee",
            table: "Bins",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreationFixedFee",
            table: "Bins");

        migrationBuilder.DropColumn(
            name: "CreationPercentFee",
            table: "Bins");

        migrationBuilder.DropColumn(
            name: "FundingFixedFee",
            table: "Bins");

        migrationBuilder.DropColumn(
            name: "FundingPercentFee",
            table: "Bins");
    }
}
