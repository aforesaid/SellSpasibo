using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SellSpasibo.API.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_BANK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cost = table.Column<double>(type: "double precision", nullable: false),
                    TransactionType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Content = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Hash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_TRANSACTION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Phone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    PhoneIsValid = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneLinkId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_USER_INFO", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_MemberId",
                table: "Banks",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Time_TransactionType",
                table: "Transactions",
                columns: new[] { "Time", "TransactionType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_Phone",
                table: "UserInfos",
                column: "Phone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
