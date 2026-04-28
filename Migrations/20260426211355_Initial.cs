using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_ClavierDor.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FausseReponse1",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FausseReponse2",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "whole", "nombre" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Aucune, c'est exactement pareil, les deux font la même chose", "== est réservé aux nombres, .Equals() aux strings uniquement" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Une classe qui n'a pas encore été codée, comme le projet de fin d'année", "Une classe qui s'efface toute seule après utilisation" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Un framework pour faire des jeux vidéo en 3D", "Un langage concurrent de C# inventé par Microsoft en 2023" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Une classe reçoit de l'argent d'une classe plus ancienne", "Deux classes fusionnent pour n'en former qu'une seule" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Super Quick Loading", "System Quality Loop" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "FETCH (comme un chien qui rapporte les données)", "GIMME" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Une clé USB perdue à l'étranger lors d'un voyage scolaire", "Un mot de passe chiffré stocké dans la base de données" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Un outil pour optimiser les réunions de management", "Un antivirus spécialisé pour les bases de données" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "LEFT JOIN est pour les gauchers, INNER JOIN pour tout le monde", "Aucune différence, c'est juste une question de préférence personnelle" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Hyper Text Making Lunch", "High Tech Modern Layout" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Computer Styling System, un logiciel de design graphique", "Cool Site Stuff, les trucs sympas qu'on rajoute sur un site" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Un nouveau smartphone concurrent de l'iPhone", "Un éditeur de code concurrent de VS Code" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Blazor Server est payant, WebAssembly est gratuit", "WebAssembly est plus vieux et Server est la nouvelle version moderne" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "FausseReponse1", "FausseReponse2" },
                values: new object[] { "Une boucle réservée aux développeurs qui s'appellent Théo ou Léa", "Un raccourci clavier pour dupliquer du code automatiquement" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "BonneReponse", "Difficulte", "FausseReponse1", "FausseReponse2", "Points", "Texte" },
                values: new object[] { "Une fonction qui s'appelle elle-même", "Moyen", "Un algorithme qui tourne en rond jusqu'à ce que le PC explose", "Une technique pour copier-coller du code en boucle", 2, "Qu'est-ce que la récursivité ?" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BonneReponse", "Difficulte", "FausseReponse1", "FausseReponse2", "Points", "Texte" },
                values: new object[] { "O(log n)", "Boss", "O(n²) parce que chercher c'est toujours compliqué", "O(café) — ça dépend du nombre de pauses prises", 3, "Quelle est la complexité d'une recherche binaire ?" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FausseReponse1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "FausseReponse2",
                table: "Questions");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "BonneReponse", "Difficulte", "Points", "Texte" },
                values: new object[] { "O(log n)", "Boss", 3, "Quelle est la complexité d'une recherche binaire ?" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BonneReponse", "Difficulte", "Points", "Texte" },
                values: new object[] { "Une fonction qui s'appelle elle-même", "Moyen", 2, "Qu'est-ce que la récursivité ?" });
        }
    }
}
