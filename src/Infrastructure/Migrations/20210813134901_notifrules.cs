using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class notifrules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserContactInfo_NotificationRule_NotificationRuleId",
                table: "UserContactInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserContactInfo_NotificationRuleId",
                table: "UserContactInfo");

            migrationBuilder.DropColumn(
                name: "NotificationRuleId",
                table: "UserContactInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationRuleId",
                table: "UserContactInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserContactInfo_NotificationRuleId",
                table: "UserContactInfo",
                column: "NotificationRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserContactInfo_NotificationRule_NotificationRuleId",
                table: "UserContactInfo",
                column: "NotificationRuleId",
                principalTable: "NotificationRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
