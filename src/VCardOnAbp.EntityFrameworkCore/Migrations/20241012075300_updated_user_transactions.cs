using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VCardOnAbp.Migrations
{
    /// <inheritdoc />
    public partial class updated_user_transactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTransactions",
                table: "UserTransactions");

            migrationBuilder.DropColumn(
                name: "TrxId",
                table: "UserTransactions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTransactions",
                table: "UserTransactions",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTransactions",
                table: "UserTransactions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTransactions");

            migrationBuilder.AddColumn<string>(
                name: "TrxId",
                table: "UserTransactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTransactions",
                table: "UserTransactions",
                column: "TrxId");
        }
    }
}
