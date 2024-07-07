using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aukcjapl.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Listy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    Obraz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sprzedane = table.Column<bool>(type: "bit", nullable: false),
                    IdUzytkownika = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listy_AspNetUsers_IdUzytkownika",
                        column: x => x.IdUzytkownika,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentarze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUzytkownika = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdListy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentarze", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentarze_AspNetUsers_IdUzytkownika",
                        column: x => x.IdUzytkownika,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komentarze_Listy_IdListy",
                        column: x => x.IdListy,
                        principalTable: "Listy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Oferty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    IdUzytkownika = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdListy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oferty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oferty_AspNetUsers_IdUzytkownika",
                        column: x => x.IdUzytkownika,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oferty_Listy_IdListy",
                        column: x => x.IdListy,
                        principalTable: "Listy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Komentarze_IdListy",
                table: "Komentarze",
                column: "IdListy");

            migrationBuilder.CreateIndex(
                name: "IX_Komentarze_IdUzytkownika",
                table: "Komentarze",
                column: "IdUzytkownika");

            migrationBuilder.CreateIndex(
                name: "IX_Listy_IdUzytkownika",
                table: "Listy",
                column: "IdUzytkownika");

            migrationBuilder.CreateIndex(
                name: "IX_Oferty_IdListy",
                table: "Oferty",
                column: "IdListy");

            migrationBuilder.CreateIndex(
                name: "IX_Oferty_IdUzytkownika",
                table: "Oferty",
                column: "IdUzytkownika");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentarze");

            migrationBuilder.DropTable(
                name: "Oferty");

            migrationBuilder.DropTable(
                name: "Listy");
        }
    }
}
