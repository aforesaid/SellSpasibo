using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SellSpasibo.API.Infrastructure.Migrations
{
    public partial class Add_TransactionHistoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberFrom = table.Column<string>(type: "text", nullable: true),
                    NumberTo = table.Column<string>(type: "text", nullable: true),
                    Commission = table.Column<double>(type: "double precision", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    TransactionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TransactionEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_TRANSACTION_HISTORY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionHistories_Transactions_TransactionEntityId",
                        column: x => x.TransactionEntityId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_NumberFrom_NumberTo",
                table: "TransactionHistories",
                columns: new[] { "NumberFrom", "NumberTo" });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_TransactionEntityId",
                table: "TransactionHistories",
                column: "TransactionEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionHistories");
        }
    }
}
