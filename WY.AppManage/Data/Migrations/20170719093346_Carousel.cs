using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WY.AppManage.Data.Migrations
{
    public partial class Carousel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Carousel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "App",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Carousel");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "App");
        }
    }
}
