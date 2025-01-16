using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIBData.Migrations
{
    /// <inheritdoc />
    public partial class allentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operatingsystemen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operatingsystemen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uitleenobjecten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jaar = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Kostprijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Auteur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aantalpaginas = table.Column<int>(type: "int", nullable: true),
                    OperatingSysteemId = table.Column<int>(type: "int", nullable: true),
                    Schermgrootte = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uitleenobjecten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uitleenobjecten_Operatingsystemen_OperatingSysteemId",
                        column: x => x.OperatingSysteemId,
                        principalTable: "Operatingsystemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserveringen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UitleenobjectId = table.Column<int>(type: "int", nullable: false),
                    LenerId = table.Column<int>(type: "int", nullable: false),
                    GereserveerdOp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserveringen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserveringen_Leners_LenerId",
                        column: x => x.LenerId,
                        principalTable: "Leners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserveringen_Uitleenobjecten_UitleenobjectId",
                        column: x => x.UitleenobjectId,
                        principalTable: "Uitleenobjecten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uitleningen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UitleenobjectId = table.Column<int>(type: "int", nullable: false),
                    LenerId = table.Column<int>(type: "int", nullable: false),
                    Van = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tot = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uitleningen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uitleningen_Leners_LenerId",
                        column: x => x.LenerId,
                        principalTable: "Leners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uitleningen_Uitleenobjecten_UitleenobjectId",
                        column: x => x.UitleenobjectId,
                        principalTable: "Uitleenobjecten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserveringen_LenerId",
                table: "Reserveringen",
                column: "LenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserveringen_UitleenobjectId",
                table: "Reserveringen",
                column: "UitleenobjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Uitleenobjecten_OperatingSysteemId",
                table: "Uitleenobjecten",
                column: "OperatingSysteemId");

            migrationBuilder.CreateIndex(
                name: "IX_Uitleningen_LenerId",
                table: "Uitleningen",
                column: "LenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Uitleningen_UitleenobjectId",
                table: "Uitleningen",
                column: "UitleenobjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserveringen");

            migrationBuilder.DropTable(
                name: "Uitleningen");

            migrationBuilder.DropTable(
                name: "Uitleenobjecten");

            migrationBuilder.DropTable(
                name: "Operatingsystemen");
        }
    }
}
