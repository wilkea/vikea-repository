using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dpcleague_2.Migrations
{
    /// <inheritdoc />
    public partial class d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eveniments",
                columns: table => new
                {
                    EvenimentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Disciplina = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataInceput = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Locatia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eveniments", x => x.EvenimentId);
                });

            migrationBuilder.CreateTable(
                name: "Organizaties",
                columns: table => new
                {
                    OrganizatieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCrearii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Originea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizaties", x => x.OrganizatieId);
                });

            migrationBuilder.CreateTable(
                name: "Bilets",
                columns: table => new
                {
                    BiletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataProcurarii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EvenimentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilets", x => x.BiletId);
                    table.ForeignKey(
                        name: "FK_Bilets_Eveniments_EvenimentId",
                        column: x => x.EvenimentId,
                        principalTable: "Eveniments",
                        principalColumn: "EvenimentId");
                });

            migrationBuilder.CreateTable(
                name: "Rosters",
                columns: table => new
                {
                    RosterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizatieId = table.Column<int>(type: "int", nullable: false),
                    Disciplina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataFormare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rosters", x => x.RosterId);
                    table.ForeignKey(
                        name: "FK_Rosters_Organizaties_OrganizatieId",
                        column: x => x.OrganizatieId,
                        principalTable: "Organizaties",
                        principalColumn: "OrganizatieId");
                });

            migrationBuilder.CreateTable(
                name: "Sportivs",
                columns: table => new
                {
                    SportivId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataNasterii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizatieId = table.Column<int>(type: "int", nullable: false),
                    Porecla = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sportivs", x => x.SportivId);
                    table.ForeignKey(
                        name: "FK_Sportivs_Organizaties_OrganizatieId",
                        column: x => x.OrganizatieId,
                        principalTable: "Organizaties",
                        principalColumn: "OrganizatieId");
                });

            migrationBuilder.CreateTable(
                name: "RosterEveniments",
                columns: table => new
                {
                    RosterEvenimentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvenimentId = table.Column<int>(type: "int", nullable: false),
                    RosterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterEveniments", x => x.RosterEvenimentId);
                    table.ForeignKey(
                        name: "FK_RosterEveniments_Eveniments_EvenimentId",
                        column: x => x.EvenimentId,
                        principalTable: "Eveniments",
                        principalColumn: "EvenimentId");
                    table.ForeignKey(
                        name: "FK_RosterEveniments_Rosters_RosterId",
                        column: x => x.RosterId,
                        principalTable: "Rosters",
                        principalColumn: "RosterId");
                });

            migrationBuilder.CreateTable(
                name: "RosterSportivs",
                columns: table => new
                {
                    RosterSportivId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RosterId = table.Column<int>(type: "int", nullable: false),
                    SportivId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterSportivs", x => x.RosterSportivId);
                    table.ForeignKey(
                        name: "FK_RosterSportivs_Rosters_RosterId",
                        column: x => x.RosterId,
                        principalTable: "Rosters",
                        principalColumn: "RosterId");
                    table.ForeignKey(
                        name: "FK_RosterSportivs_Sportivs_SportivId",
                        column: x => x.SportivId,
                        principalTable: "Sportivs",
                        principalColumn: "SportivId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bilets_EvenimentId",
                table: "Bilets",
                column: "EvenimentId");

            migrationBuilder.CreateIndex(
                name: "IX_RosterEveniments_EvenimentId",
                table: "RosterEveniments",
                column: "EvenimentId");

            migrationBuilder.CreateIndex(
                name: "IX_RosterEveniments_RosterId",
                table: "RosterEveniments",
                column: "RosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rosters_OrganizatieId",
                table: "Rosters",
                column: "OrganizatieId");

            migrationBuilder.CreateIndex(
                name: "IX_RosterSportivs_RosterId",
                table: "RosterSportivs",
                column: "RosterId");

            migrationBuilder.CreateIndex(
                name: "IX_RosterSportivs_SportivId",
                table: "RosterSportivs",
                column: "SportivId");

            migrationBuilder.CreateIndex(
                name: "IX_Sportivs_OrganizatieId",
                table: "Sportivs",
                column: "OrganizatieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bilets");

            migrationBuilder.DropTable(
                name: "RosterEveniments");

            migrationBuilder.DropTable(
                name: "RosterSportivs");

            migrationBuilder.DropTable(
                name: "Eveniments");

            migrationBuilder.DropTable(
                name: "Rosters");

            migrationBuilder.DropTable(
                name: "Sportivs");

            migrationBuilder.DropTable(
                name: "Organizaties");
        }
    }
}
