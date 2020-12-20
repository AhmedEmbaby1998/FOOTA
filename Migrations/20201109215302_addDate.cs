﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KhaledMohsen.Migrations
{
    public partial class addDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveTill",
                table: "Consumers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTill",
                table: "Consumers");
        }
    }
}
