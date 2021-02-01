using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace project.Data.Migrations2
{
    public partial class new3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CsUser",
                table: "InfoSystems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CsUser",
                table: "InfoSystems",
                nullable: true);
        }
    }
}
