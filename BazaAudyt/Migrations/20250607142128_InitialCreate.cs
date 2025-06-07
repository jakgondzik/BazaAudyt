using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaAudyt.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CzlonkowieZespolu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inicjaly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CzyAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Warstwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CzyAudytor = table.Column<bool>(type: "bit", nullable: false),
                    NazwaUzytkownika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CzlonkowieZespolu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LPA_PlanAudytow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AudytorId = table.Column<int>(type: "int", nullable: true),
                    Towarzyszacy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stanowisko = table.Column<int>(type: "int", nullable: true),
                    DataPlanowana = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObszarAudytu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataZamkniecia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pozycja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wydzial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brygada = table.Column<int>(type: "int", nullable: true),
                    Audytowany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Komentarz = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LPA_PlanAudytow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LPA_PodsumowanieWynikow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCzesci = table.Column<int>(type: "int", nullable: true),
                    DataWykonania = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdAudytowanego = table.Column<int>(type: "int", nullable: true),
                    IdAudytu = table.Column<int>(type: "int", nullable: true),
                    Komentarz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audytowany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rozpoczety = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LPA_PodsumowanieWynikow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LPA_Pytania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pytanie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obszar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nr = table.Column<int>(type: "int", nullable: true),
                    Aktywne = table.Column<bool>(type: "bit", nullable: false),
                    Norma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Waga = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LPA_Pytania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LPA_Wyniki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pytanie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wynik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAudytu = table.Column<int>(type: "int", nullable: true),
                    Komentarz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wartosc = table.Column<int>(type: "int", nullable: true),
                    Uwagi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LPA_Wyniki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StanowiskaPracy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Wydzial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Proces = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gniazdo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrGniazda = table.Column<int>(type: "int", nullable: true),
                    RodzajStanowiska = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLidera = table.Column<int>(type: "int", nullable: true),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObszarLPA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanowiskaPracy", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CzlonkowieZespolu");

            migrationBuilder.DropTable(
                name: "Konta");

            migrationBuilder.DropTable(
                name: "LPA_PlanAudytow");

            migrationBuilder.DropTable(
                name: "LPA_PodsumowanieWynikow");

            migrationBuilder.DropTable(
                name: "LPA_Pytania");

            migrationBuilder.DropTable(
                name: "LPA_Wyniki");

            migrationBuilder.DropTable(
                name: "StanowiskaPracy");
        }
    }
}
