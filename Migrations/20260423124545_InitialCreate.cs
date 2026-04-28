using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_ClavierDor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Joueurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pseudo = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joueurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Texte = table.Column<string>(type: "TEXT", nullable: false),
                    BonneReponse = table.Column<string>(type: "TEXT", nullable: false),
                    Difficulte = table.Column<string>(type: "TEXT", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    CategorieId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateDebut = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    Etat = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    JoueurId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parties_Joueurs_JoueurId",
                        column: x => x.JoueurId,
                        principalTable: "Joueurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jokers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Utilise = table.Column<bool>(type: "INTEGER", nullable: false),
                    PartieId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jokers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jokers_Parties_PartieId",
                        column: x => x.PartieId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReponseDonnee = table.Column<string>(type: "TEXT", nullable: false),
                    EstCorrecte = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateReponse = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PartieId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reponses_Parties_PartieId",
                        column: x => x.PartieId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Titre" },
                values: new object[,]
                {
                    { 1, "Questions sur le langage C#", "C#" },
                    { 2, "SQL, ORM, SQLite", "Base de données" },
                    { 3, "HTML, CSS, Blazor", "Web" },
                    { 4, "Logique et algorithmes", "Algorithmie" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "BonneReponse", "CategorieId", "Difficulte", "Points", "Texte" },
                values: new object[,]
                {
                    { 1, "int", 1, "Facile", 1, "Quel mot-clé permet de déclarer une variable entière en C# ?" },
                    { 2, "== compare les références, .Equals() compare les valeurs", 1, "Facile", 1, "Quelle est la différence entre '==' et '.Equals()' en C# ?" },
                    { 3, "Une classe qui ne peut pas être instanciée directement", 1, "Moyen", 2, "Qu'est-ce qu'une classe abstraite en C# ?" },
                    { 4, "Language Integrated Query, permet d'interroger des collections", 1, "Moyen", 2, "Qu'est-ce que le LINQ en C# ?" },
                    { 5, "Une classe enfant hérite des propriétés et méthodes de la classe parent", 1, "Boss", 3, "Expliquez le principe de l'héritage en POO." },
                    { 6, "Structured Query Language", 2, "Facile", 1, "Que signifie SQL ?" },
                    { 7, "SELECT", 2, "Facile", 1, "Quelle commande SQL permet de récupérer des données ?" },
                    { 8, "Une colonne qui référence la clé primaire d'une autre table", 2, "Moyen", 2, "Qu'est-ce qu'une clé étrangère ?" },
                    { 9, "Object Relational Mapper, fait le lien entre objets C# et tables SQL", 2, "Moyen", 2, "Qu'est-ce qu'un ORM ?" },
                    { 10, "INNER JOIN retourne uniquement les correspondances, LEFT JOIN retourne tout de la table gauche", 2, "Boss", 3, "Quelle est la différence entre INNER JOIN et LEFT JOIN ?" },
                    { 11, "HyperText Markup Language", 3, "Facile", 1, "Que signifie HTML ?" },
                    { 12, "Cascading Style Sheets, langage de mise en forme des pages web", 3, "Facile", 1, "Qu'est-ce que le CSS ?" },
                    { 13, "Framework C# pour créer des interfaces web interactives", 3, "Moyen", 2, "Qu'est-ce que Blazor ?" },
                    { 14, "Server s'exécute côté serveur, WebAssembly côté client dans le navigateur", 3, "Boss", 3, "Quelle est la différence entre Blazor Server et Blazor WebAssembly ?" },
                    { 15, "Une structure qui répète un bloc de code un nombre défini de fois", 4, "Facile", 1, "Qu'est-ce qu'une boucle for ?" },
                    { 16, "O(log n)", 4, "Boss", 3, "Quelle est la complexité d'une recherche binaire ?" },
                    { 17, "Une fonction qui s'appelle elle-même", 4, "Moyen", 2, "Qu'est-ce que la récursivité ?" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jokers_PartieId",
                table: "Jokers",
                column: "PartieId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_JoueurId",
                table: "Parties",
                column: "JoueurId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategorieId",
                table: "Questions",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_PartieId",
                table: "Reponses",
                column: "PartieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_QuestionId",
                table: "Reponses",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jokers");

            migrationBuilder.DropTable(
                name: "Reponses");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Joueurs");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
