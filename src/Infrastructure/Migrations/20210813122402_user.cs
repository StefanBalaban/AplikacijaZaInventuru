using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietPlanPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietPlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlanPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPlanPeriod_DietPlans_DietPlanId",
                        column: x => x.DietPlanId,
                        principalTable: "DietPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationRule_FoodProducts_FoodProductId",
                        column: x => x.FoodProductId,
                        principalTable: "FoodProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserContactInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationRuleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserContactInfo_NotificationRule_NotificationRuleId",
                        column: x => x.NotificationRuleId,
                        principalTable: "NotificationRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserContactInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationRuleUserContactInfos",
                columns: table => new
                {
                    UserContactInfosId = table.Column<int>(type: "int", nullable: false),
                    NotificationRuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRuleUserContactInfos", x => new { x.UserContactInfosId, x.NotificationRuleId });
                    table.ForeignKey(
                        name: "FK_NotificationRuleUserContactInfos_NotificationRule_NotificationRuleId",
                        column: x => x.NotificationRuleId,
                        principalTable: "NotificationRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationRuleUserContactInfos_UserContactInfo_UserContactInfosId",
                        column: x => x.UserContactInfosId,
                        principalTable: "UserContactInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietPlanPeriod_DietPlanId",
                table: "DietPlanPeriod",
                column: "DietPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRule_FoodProductId",
                table: "NotificationRule",
                column: "FoodProductId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRuleUserContactInfos_NotificationRuleId",
                table: "NotificationRuleUserContactInfos",
                column: "NotificationRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContactInfo_NotificationRuleId",
                table: "UserContactInfo",
                column: "NotificationRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContactInfo_UserId",
                table: "UserContactInfo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietPlanPeriod");

            migrationBuilder.DropTable(
                name: "NotificationRuleUserContactInfos");

            migrationBuilder.DropTable(
                name: "UserContactInfo");

            migrationBuilder.DropTable(
                name: "NotificationRule");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
