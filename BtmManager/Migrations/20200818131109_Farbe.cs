using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BtmManager.Migrations
{
    public partial class Farbe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projekte",
                columns: table => new
                {
                    ProjektId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BtmBestandsbuchNr = table.Column<string>(nullable: false),
                    Stufenanzahl = table.Column<int>(nullable: false),
                    Produktbezeichnung = table.Column<string>(nullable: true),
                    ProduktNr = table.Column<int>(nullable: false),
                    Zeitraum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekte", x => x.ProjektId);
                });

            migrationBuilder.CreateTable(
                name: "Stufen",
                columns: table => new
                {
                    StufId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StufenNummer = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(nullable: true),
                    AnzahlEinträge = table.Column<int>(nullable: false),
                    ProjektId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stufen", x => x.StufId);
                    table.ForeignKey(
                        name: "FK_Stufen_Projekte_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekte",
                        principalColumn: "ProjektId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Einträge",
                columns: table => new
                {
                    EintragId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Einheit = table.Column<byte>(nullable: false),
                    Bezeichnung = table.Column<string>(nullable: false),
                    IsFirst = table.Column<bool>(nullable: false),
                    LfdNr = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Anfangsbestand = table.Column<float>(nullable: false),
                    TheroZugang = table.Column<float>(nullable: false),
                    PrakZugang = table.Column<float>(nullable: false),
                    Arbeitsverlust = table.Column<float>(nullable: false),
                    Abgang = table.Column<float>(nullable: false),
                    AktuellerBestand = table.Column<float>(nullable: false),
                    Bemerkung = table.Column<string>(nullable: true),
                    WurdeMarkiert = table.Column<bool>(nullable: false),
                    Farbe = table.Column<string>(nullable: true),
                    StufId = table.Column<int>(nullable: false),
                    StufenStufId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Einträge", x => x.EintragId);
                    table.ForeignKey(
                        name: "FK_Einträge_Stufen_StufenStufId",
                        column: x => x.StufenStufId,
                        principalTable: "Stufen",
                        principalColumn: "StufId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Einträge_StufenStufId",
                table: "Einträge",
                column: "StufenStufId");

            migrationBuilder.CreateIndex(
                name: "IX_Stufen_ProjektId",
                table: "Stufen",
                column: "ProjektId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Einträge");

            migrationBuilder.DropTable(
                name: "Stufen");

            migrationBuilder.DropTable(
                name: "Projekte");
        }
    }
}
