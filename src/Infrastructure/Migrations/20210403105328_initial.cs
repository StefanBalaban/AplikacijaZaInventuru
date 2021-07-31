using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "DietPlans",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_DietPlans", x => x.Id); });

            migrationBuilder.CreateTable(
                "UnitOfMeasures",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Measure = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_UnitOfMeasures", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>("nvarchar(max)", nullable: true),
                    LastName = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "DietPlanPeriods",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietPlanId = table.Column<int>("int", nullable: true),
                    StartDate = table.Column<DateTime>("datetime2", nullable: false),
                    EndDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlanPeriods", x => x.Id);
                    table.ForeignKey(
                        "FK_DietPlanPeriods_DietPlans_DietPlanId",
                        x => x.DietPlanId,
                        "DietPlans",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Meals",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    DietPlanId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        "FK_Meals_DietPlans_DietPlanId",
                        x => x.DietPlanId,
                        "DietPlans",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "FoodProducts",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    UnitOfMeasureId = table.Column<int>("int", nullable: true),
                    Calories = table.Column<float>("real", nullable: false),
                    Protein = table.Column<float>("real", nullable: false),
                    Carbohydrates = table.Column<float>("real", nullable: false),
                    Fats = table.Column<float>("real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodProducts", x => x.Id);
                    table.ForeignKey(
                        "FK_FoodProducts_UnitOfMeasures_UnitOfMeasureId",
                        x => x.UnitOfMeasureId,
                        "UnitOfMeasures",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "FoodStocks",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodProductId = table.Column<int>("int", nullable: true),
                    Amount = table.Column<float>("real", nullable: false),
                    UpperAmount = table.Column<float>("real", nullable: true),
                    LowerAmount = table.Column<float>("real", nullable: true),
                    DateOfPurchase = table.Column<DateTime>("datetime2", nullable: true),
                    BestUseByDate = table.Column<DateTime>("datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodStocks", x => x.Id);
                    table.ForeignKey(
                        "FK_FoodStocks_FoodProducts_FoodProductId",
                        x => x.FoodProductId,
                        "FoodProducts",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "MealItems",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodProductId = table.Column<int>("int", nullable: true),
                    Amount = table.Column<float>("real", nullable: false),
                    MealId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealItems", x => x.Id);
                    table.ForeignKey(
                        "FK_MealItems_FoodProducts_FoodProductId",
                        x => x.FoodProductId,
                        "FoodProducts",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_MealItems_Meals_MealId",
                        x => x.MealId,
                        "Meals",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "NotificationRules",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodProductId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRules", x => x.Id);
                    table.ForeignKey(
                        "FK_NotificationRules_FoodProducts_FoodProductId",
                        x => x.FoodProductId,
                        "FoodProducts",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "UserContactInfos",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contact = table.Column<string>("nvarchar(max)", nullable: true),
                    NotificationRuleId = table.Column<int>("int", nullable: true),
                    UserId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContactInfos", x => x.Id);
                    table.ForeignKey(
                        "FK_UserContactInfos_NotificationRules_NotificationRuleId",
                        x => x.NotificationRuleId,
                        "NotificationRules",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_UserContactInfos_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_DietPlanPeriods_DietPlanId",
                "DietPlanPeriods",
                "DietPlanId");

            migrationBuilder.CreateIndex(
                "IX_FoodProducts_UnitOfMeasureId",
                "FoodProducts",
                "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                "IX_FoodStocks_FoodProductId",
                "FoodStocks",
                "FoodProductId");

            migrationBuilder.CreateIndex(
                "IX_MealItems_FoodProductId",
                "MealItems",
                "FoodProductId");

            migrationBuilder.CreateIndex(
                "IX_MealItems_MealId",
                "MealItems",
                "MealId");

            migrationBuilder.CreateIndex(
                "IX_Meals_DietPlanId",
                "Meals",
                "DietPlanId");

            migrationBuilder.CreateIndex(
                "IX_NotificationRules_FoodProductId",
                "NotificationRules",
                "FoodProductId");

            migrationBuilder.CreateIndex(
                "IX_UserContactInfos_NotificationRuleId",
                "UserContactInfos",
                "NotificationRuleId");

            migrationBuilder.CreateIndex(
                "IX_UserContactInfos_UserId",
                "UserContactInfos",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "DietPlanPeriods");

            migrationBuilder.DropTable(
                "FoodStocks");

            migrationBuilder.DropTable(
                "MealItems");

            migrationBuilder.DropTable(
                "UserContactInfos");

            migrationBuilder.DropTable(
                "Meals");

            migrationBuilder.DropTable(
                "NotificationRules");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "DietPlans");

            migrationBuilder.DropTable(
                "FoodProducts");

            migrationBuilder.DropTable(
                "UnitOfMeasures");
        }
    }
}