using Microsoft.EntityFrameworkCore.Migrations;

namespace PlutusDb.Migrations
{
    public partial class SchedulerFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "ScheduledPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "ScheduledPayments");
        }
    }
}
