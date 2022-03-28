using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timelog.WebApp.Migrations
{
    public partial class TimelogActivityAddTypeReferens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ActivityTypeId",
                table: "UserActivities",
                column: "ActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_ActivityTypes_ActivityTypeId",
                table: "UserActivities",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_ActivityTypes_ActivityTypeId",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_ActivityTypeId",
                table: "UserActivities");
        }
    }
}
