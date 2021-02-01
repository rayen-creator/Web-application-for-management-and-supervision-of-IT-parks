using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace project.Data.Migrations2
{
    public partial class newmig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComputerName",
                table: "InfoNetworks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComputerName",
                table: "InfoNetworks");
        }
    }
}
