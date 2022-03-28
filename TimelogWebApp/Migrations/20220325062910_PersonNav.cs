using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timelog.WebApp.Migrations
{
    public partial class PersonNav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "UserActivities",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "UserActivities",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ProjectId",
                table: "UserActivities",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Projects_ProjectId",
                table: "UserActivities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Projects_ProjectId",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_ProjectId",
                table: "UserActivities");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "UserActivities",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "UserActivities",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
