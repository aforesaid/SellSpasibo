using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SellSpasibo.API.Infrastructure.Migrations
{
    public partial class Configure_Tinkoff_Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TinkoffAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_TINKOFF_ACCOUNT", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TinkoffAccounts_Phone",
                table: "TinkoffAccounts",
                column: "Phone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinkoffAccounts");
        }
    }
}
