using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SellSpasibo.API.Infrastructure.Migrations
{
    public partial class CleanUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SentAmout",
                table: "PayInfo",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionEntityId",
                table: "PayInfo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PayInfo_TransactionEntityId",
                table: "PayInfo",
                column: "TransactionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayInfo_Transactions_TransactionEntityId",
                table: "PayInfo",
                column: "TransactionEntityId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayInfo_Transactions_TransactionEntityId",
                table: "PayInfo");

            migrationBuilder.DropIndex(
                name: "IX_PayInfo_TransactionEntityId",
                table: "PayInfo");

            migrationBuilder.DropColumn(
                name: "SentAmout",
                table: "PayInfo");

            migrationBuilder.DropColumn(
                name: "TransactionEntityId",
                table: "PayInfo");
        }
    }
}
