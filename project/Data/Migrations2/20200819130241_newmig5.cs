using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace project.Data.Migrations2
{
    public partial class newmig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AD",
                table: "Pcs",
                type: "nvarchar(20)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AD",
                table: "Pcs",
                type: "nvarchar(20)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 3,
                oldNullable: true);
        }
    }
}
