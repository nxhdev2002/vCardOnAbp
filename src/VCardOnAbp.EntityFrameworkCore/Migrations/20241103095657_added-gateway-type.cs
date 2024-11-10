using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VCardOnAbp.Migrations
{
    /// <inheritdoc />
    public partial class addedgatewaytype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GatewayType",
                table: "PaymentMethods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GatewayType",
                table: "PaymentMethods");
        }
    }
}
