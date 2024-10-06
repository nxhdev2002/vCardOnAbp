using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VCardOnAbp.Migrations
{
    /// <inheritdoc />
    public partial class updated_supplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "Supplier",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "Cards");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "Cards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
