using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP24.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TP24InfrastractureEFContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    DebtorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
