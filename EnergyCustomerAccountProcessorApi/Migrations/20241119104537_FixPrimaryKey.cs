using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyCustomerAccountProcessorApi.Migrations
{
    /// <inheritdoc />
    public partial class FixPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "MeterReadings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserAccountID = table.Column<int>(type: "INTEGER", nullable: false),
                    MeterReadingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MeterReadingValue = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterReadings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MeterReadings_Users_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "Users",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_UserAccountID",
                table: "MeterReadings",
                column: "UserAccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeterReadings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
