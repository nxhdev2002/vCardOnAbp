using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VCardOnAbp.Migrations
{
    /// <inheritdoc />
    public partial class added_currencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userCurrencies",
                table: "userCurrencies");

            migrationBuilder.RenameTable(
                name: "userCurrencies",
                newName: "UserCurrencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCurrencies",
                table: "UserCurrencies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Currencies",
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
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCurrencies",
                table: "UserCurrencies");

            migrationBuilder.RenameTable(
                name: "UserCurrencies",
                newName: "userCurrencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userCurrencies",
                table: "userCurrencies",
                column: "Id");
        }
    }
}
