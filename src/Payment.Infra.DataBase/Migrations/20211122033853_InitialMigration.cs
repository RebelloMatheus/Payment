using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Infra.DataBase.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Antecipations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    AnalysisStartDate = table.Column<DateTime>(nullable: true),
                    AnalysisEndDate = table.Column<DateTime>(nullable: true),
                    AnalysisResult = table.Column<int>(type: "int", nullable: true),
                    RequestedAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    GrantedAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_Antecipations_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    NotApprovedDate = table.Column<DateTime>(nullable: true),
                    AntecipationStatus = table.Column<int>(type: "int", nullable: true),
                    BankConfirmation = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    FlatRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    InstallmentsNumber = table.Column<int>(nullable: false),
                    FourLastCardNumber = table.Column<string>(nullable: true),
                    AntecipationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_Transactions_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Antecipations_AntecipationId",
                        column: x => x.AntecipationId,
                        principalTable: "Antecipations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Number = table.Column<int>(nullable: false),
                    AntecipationValue = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    ExpectedPaymentDate = table.Column<DateTime>(nullable: false),
                    AntecipationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_Installments_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installments_Transactions_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installments_PaymentId",
                table: "Installments",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AntecipationId",
                table: "Transactions",
                column: "AntecipationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Antecipations");
        }
    }
}
