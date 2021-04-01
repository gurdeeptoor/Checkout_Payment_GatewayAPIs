using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.PaymentGateway.Data.Migrations
{
    public partial class CheckoutDB_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    BankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.BankID);
                });

            migrationBuilder.CreateTable(
                name: "CardDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNum = table.Column<string>(type: "nvarchar(19)", maxLength: 19, nullable: false, collation: "Latin1_General_BIN2"),
                    ExpMonth = table.Column<int>(type: "int", nullable: false),
                    ExpYear = table.Column<int>(type: "int", nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, collation: "Latin1_General_BIN2"),
                    CVV = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, collation: "Latin1_General_BIN2"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Symbol = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MerchantAPIKey",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantID = table.Column<int>(type: "int", nullable: false),
                    APIKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantAPIKey", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MerchantAPIKey_Merchant",
                        column: x => x.MerchantID,
                        principalTable: "Merchant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantID = table.Column<int>(type: "int", nullable: false),
                    MerchantRef = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CardDetailID = table.Column<int>(type: "int", nullable: false),
                    BankID = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TransactionStatusID = table.Column<int>(type: "int", nullable: false),
                    AuthCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BankRef = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transaction_Bank",
                        column: x => x.BankID,
                        principalTable: "Bank",
                        principalColumn: "BankID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_CardDetail",
                        column: x => x.CardDetailID,
                        principalTable: "CardDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Currency",
                        column: x => x.CurrencyID,
                        principalTable: "Currency",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Merchant",
                        column: x => x.MerchantID,
                        principalTable: "Merchant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionStatus",
                        column: x => x.TransactionStatusID,
                        principalTable: "TransactionStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionStatusID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Transaction",
                        column: x => x.TransactionID,
                        principalTable: "Transaction",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_TransactionStatus",
                        column: x => x.TransactionStatusID,
                        principalTable: "TransactionStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantAPIKey_MerchantID",
                table: "MerchantAPIKey",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BankID",
                table: "Transaction",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardDetailID",
                table: "Transaction",
                column: "CardDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CurrencyID",
                table: "Transaction",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_MerchantID",
                table: "Transaction",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionStatusID",
                table: "Transaction",
                column: "TransactionStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_TransactionID",
                table: "TransactionHistory",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_TransactionStatusID",
                table: "TransactionHistory",
                column: "TransactionStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MerchantAPIKey");

            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "CardDetail");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.DropTable(
                name: "TransactionStatus");
        }
    }
}
