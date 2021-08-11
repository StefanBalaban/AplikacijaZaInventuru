using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class adddietplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPlanMeal_DietPlans_DietPlansId",
                table: "DietPlanMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_DietPlanMeal_Meals_MealsId",
                table: "DietPlanMeal");

            migrationBuilder.RenameColumn(
                name: "MealsId",
                table: "DietPlanMeal",
                newName: "MealId");

            migrationBuilder.RenameColumn(
                name: "DietPlansId",
                table: "DietPlanMeal",
                newName: "DietPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_DietPlanMeal_MealsId",
                table: "DietPlanMeal",
                newName: "IX_DietPlanMeal_MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlanMeal_DietPlans_DietPlanId",
                table: "DietPlanMeal",
                column: "DietPlanId",
                principalTable: "DietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlanMeal_Meals_MealId",
                table: "DietPlanMeal",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPlanMeal_DietPlans_DietPlanId",
                table: "DietPlanMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_DietPlanMeal_Meals_MealId",
                table: "DietPlanMeal");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "DietPlanMeal",
                newName: "MealsId");

            migrationBuilder.RenameColumn(
                name: "DietPlanId",
                table: "DietPlanMeal",
                newName: "DietPlansId");

            migrationBuilder.RenameIndex(
                name: "IX_DietPlanMeal_MealId",
                table: "DietPlanMeal",
                newName: "IX_DietPlanMeal_MealsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlanMeal_DietPlans_DietPlansId",
                table: "DietPlanMeal",
                column: "DietPlansId",
                principalTable: "DietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlanMeal_Meals_MealsId",
                table: "DietPlanMeal",
                column: "MealsId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
