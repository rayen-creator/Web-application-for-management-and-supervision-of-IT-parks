using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace project.Data.Migrations2
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 1024, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(22)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(22)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pcs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AD = table.Column<string>(type: "nvarchar(20)", maxLength: 3, nullable: false),
                    Adress_Mac = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    ParcID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pcs_Parcs_ParcID",
                        column: x => x.ParcID,
                        principalTable: "Parcs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPcs",
                columns: table => new
                {
                    AppId = table.Column<int>(nullable: false),
                    PcId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPcs", x => new { x.AppId, x.PcId });
                    table.ForeignKey(
                        name: "FK_AppPcs_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPcs_Pcs_PcId",
                        column: x => x.PcId,
                        principalTable: "Pcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoNetworks",
                columns: table => new
                {
                    IdInfoNet = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IP_Adress = table.Column<string>(nullable: true),
                    MAC_Adress = table.Column<string>(nullable: true),
                    PcName = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoNetworks", x => x.IdInfoNet);
                    table.ForeignKey(
                        name: "FK_InfoNetworks_Pcs_PcName",
                        column: x => x.PcName,
                        principalTable: "Pcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfoSystems",
                columns: table => new
                {
                    IdSystem = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CPU_Logical_Core = table.Column<string>(nullable: true),
                    CPU_Physical_Core = table.Column<string>(nullable: true),
                    ComputerName = table.Column<string>(nullable: true),
                    CsUser = table.Column<string>(nullable: true),
                    OS_Architecture = table.Column<string>(nullable: true),
                    OsSystem = table.Column<string>(nullable: true),
                    PcName = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSystems", x => x.IdSystem);
                    table.ForeignKey(
                        name: "FK_InfoSystems_Pcs_PcName",
                        column: x => x.PcName,
                        principalTable: "Pcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    IdPerf = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CPU = table.Column<float>(nullable: false),
                    PcName = table.Column<int>(nullable: true),
                    RAM = table.Column<float>(nullable: false),
                    System_Up_Time = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.IdPerf);
                    table.ForeignKey(
                        name: "FK_Performances_Pcs_PcName",
                        column: x => x.PcName,
                        principalTable: "Pcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPcs_PcId",
                table: "AppPcs",
                column: "PcId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoNetworks_PcName",
                table: "InfoNetworks",
                column: "PcName");

            migrationBuilder.CreateIndex(
                name: "IX_InfoSystems_PcName",
                table: "InfoSystems",
                column: "PcName");

            migrationBuilder.CreateIndex(
                name: "IX_Pcs_ParcID",
                table: "Pcs",
                column: "ParcID");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_PcName",
                table: "Performances",
                column: "PcName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPcs");

            migrationBuilder.DropTable(
                name: "InfoNetworks");

            migrationBuilder.DropTable(
                name: "InfoSystems");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropTable(
                name: "Pcs");

            migrationBuilder.DropTable(
                name: "Parcs");
        }
    }
}
