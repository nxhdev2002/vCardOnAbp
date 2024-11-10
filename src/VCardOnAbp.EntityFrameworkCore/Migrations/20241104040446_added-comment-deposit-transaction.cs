using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VCardOnAbp.Migrations
{
    /// <inheritdoc />
    public partial class addedcommentdeposittransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "DepositTransactions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "DepositTransactions");
        }
    }
}
