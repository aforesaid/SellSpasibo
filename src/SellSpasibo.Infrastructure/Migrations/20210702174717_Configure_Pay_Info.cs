using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SellSpasibo.API.Infrastructure.Migrations
{
    public partial class Configure_Pay_Info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NumberTo",
                table: "TransactionHistories",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumberFrom",
                table: "TransactionHistories",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PayInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_PAY_INFO", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayInfo_Status",
                table: "PayInfo",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayInfo");

            migrationBuilder.AlterColumn<string>(
                name: "NumberTo",
                table: "TransactionHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumberFrom",
                table: "TransactionHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true);
        }
    }
}
