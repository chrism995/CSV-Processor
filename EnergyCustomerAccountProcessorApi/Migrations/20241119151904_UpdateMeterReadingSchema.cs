using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyCustomerAccountProcessorApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeterReadingSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Users_UserAccountID",
                table: "MeterReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "MeterReadings");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "MeterReadings",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "MeterReadingValue",
                table: "MeterReadings",
                newName: "MeterReadingDateTime");

            migrationBuilder.RenameColumn(
                name: "MeterReadingDate",
                table: "MeterReadings",
                newName: "MeterReadValue");

            migrationBuilder.AlterColumn<int>(
                name: "UserAccountID",
                table: "MeterReadings",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "MeterReadings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Users_UserAccountID",
                table: "MeterReadings",
                column: "UserAccountID",
                principalTable: "Users",
                principalColumn: "AccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Users_UserAccountID",
                table: "MeterReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "MeterReadings",
                newName: "AccountID");

            migrationBuilder.RenameColumn(
                name: "MeterReadingDateTime",
                table: "MeterReadings",
                newName: "MeterReadingValue");

            migrationBuilder.RenameColumn(
                name: "MeterReadValue",
                table: "MeterReadings",
                newName: "MeterReadingDate");

            migrationBuilder.AlterColumn<int>(
                name: "UserAccountID",
                table: "MeterReadings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountID",
                table: "MeterReadings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "MeterReadings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Users_UserAccountID",
                table: "MeterReadings",
                column: "UserAccountID",
                principalTable: "Users",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
