using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VCardOnAbp.Migrations;

/// <inheritdoc />
public partial class added_payment_method : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PaymentMethods",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                FixedFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                PercentageFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PaymentMethods", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PaymentMethods");
    }
}
