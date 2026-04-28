using Microsoft.EntityFrameworkCore; //
using Projet_ClavierDor.Models; // accès aux classes Joueur, Partie, Question, Reponse, Joker

namespace Projet_ClavierDor.Data;

public class AppDbContext : DbContext
{
    // BAREME ! Déclaration de toutes mes tables  à partir de mes classes modèles (AppDbContext gère bien toutes les entités) 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
    public DbSet<Joueur> Joueurs { get; set; }
    public DbSet<Partie> Parties { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Categorie> Categories { get; set; }
    public DbSet<Reponse> Reponses { get; set; }
    public DbSet<Joker> Jokers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
    // données de départ : catégories
    modelBuilder.Entity<Categorie>().HasData(
        new Categorie { Id = 1, Titre = "C#", Description = "Questions sur le langage C#" },
        new Categorie { Id = 2, Titre = "Base de données", Description = "SQL, ORM, SQLite" },
        new Categorie { Id = 3, Titre = "Web", Description = "HTML, CSS, Blazor" },
        new Categorie { Id = 4, Titre = "Algorithmie", Description = "Logique et algorithmes" }
    );

    // Données de départ : questions
    modelBuilder.Entity<Question>().HasData(
       
        // C# — QUESTIONS FACILES
            new Question {
                Id = 1,
                Texte = "Quel mot-clé permet de déclarer une variable entière en C# ?",
                BonneReponse = "int",
                FausseReponse1 = "whole",
                FausseReponse2 = "nombre",
                Difficulte = "Facile", Points = 1, CategorieId = 1
            },
            new Question {
                Id = 2,
                Texte = "Quelle est la différence entre '==' et '.Equals()' en C# ?",
                BonneReponse = "== compare les références, .Equals() compare les valeurs",
                FausseReponse1 = "Aucune, c'est exactement pareil, les deux font la même chose",
                FausseReponse2 = "== est réservé aux nombres, .Equals() aux strings uniquement",
                Difficulte = "Facile", Points = 1, CategorieId = 1
            },

        // C# — QUESTIONS MOYENNES
            new Question {
                Id = 3,
                Texte = "Qu'est-ce qu'une classe abstraite en C# ?",
                BonneReponse = "Une classe qui ne peut pas être instanciée directement",
                FausseReponse1 = "Une classe qui n'a pas encore été codée, comme le projet de fin d'année",
                FausseReponse2 = "Une classe qui s'efface toute seule après utilisation",
                Difficulte = "Moyen", Points = 2, CategorieId = 1
            },
            new Question {
                Id = 4,
                Texte = "Qu'est-ce que le LINQ en C# ?",
                BonneReponse = "Language Integrated Query, permet d'interroger des collections",
                FausseReponse1 = "Un framework pour faire des jeux vidéo en 3D",
                FausseReponse2 = "Un langage concurrent de C# inventé par Microsoft en 2023",
                Difficulte = "Moyen", Points = 2, CategorieId = 1
            },

    // C# — QUESTIONS BOSS
            new Question {
                Id = 5,
                Texte = "Expliquez le principe de l'héritage en POO.",
                BonneReponse = "Une classe enfant hérite des propriétés et méthodes de la classe parent",
                FausseReponse1 = "Une classe reçoit de l'argent d'une classe plus ancienne",
                FausseReponse2 = "Deux classes fusionnent pour n'en former qu'une seule",
                Difficulte = "Boss", Points = 3, CategorieId = 1
            },

    // Base de données — Facile
            new Question {
                Id = 6,
                Texte = "Que signifie SQL ?",
                BonneReponse = "Structured Query Language",
                FausseReponse1 = "Super Quick Loading",
                FausseReponse2 = "System Quality Loop",
                Difficulte = "Facile", Points = 1, CategorieId = 2
            },
            new Question {
                Id = 7,
                Texte = "Quelle commande SQL permet de récupérer des données ?",
                BonneReponse = "SELECT",
                FausseReponse1 = "FETCH (comme un chien qui rapporte les données)",
                FausseReponse2 = "GIMME",
                Difficulte = "Facile", Points = 1, CategorieId = 2
            },

        // Base de données — Moyen
            new Question {
                Id = 8,
                Texte = "Qu'est-ce qu'une clé étrangère ?",
                BonneReponse = "Une colonne qui référence la clé primaire d'une autre table",
                FausseReponse1 = "Une clé USB perdue à l'étranger lors d'un voyage scolaire",
                FausseReponse2 = "Un mot de passe chiffré stocké dans la base de données",
                Difficulte = "Moyen", Points = 2, CategorieId = 2
            },
            new Question {
                Id = 9,
                Texte = "Qu'est-ce qu'un ORM ?",
                BonneReponse = "Object Relational Mapper, fait le lien entre objets C# et tables SQL",
                FausseReponse1 = "Un outil pour optimiser les réunions de management",
                FausseReponse2 = "Un antivirus spécialisé pour les bases de données",
                Difficulte = "Moyen", Points = 2, CategorieId = 2
            },

    // Base de données — Boss
            new Question {
                Id = 10,
                Texte = "Quelle est la différence entre INNER JOIN et LEFT JOIN ?",
                BonneReponse = "INNER JOIN retourne uniquement les correspondances, LEFT JOIN retourne tout de la table gauche",
                FausseReponse1 = "LEFT JOIN est pour les gauchers, INNER JOIN pour tout le monde",
                FausseReponse2 = "Aucune différence, c'est juste une question de préférence personnelle",
                Difficulte = "Boss", Points = 3, CategorieId = 2
            },

    // Web — QUESTIONS FACILES
            new Question {
                Id = 11,
                Texte = "Que signifie HTML ?",
                BonneReponse = "HyperText Markup Language",
                FausseReponse1 = "Hyper Text Making Lunch",
                FausseReponse2 = "High Tech Modern Layout",
                Difficulte = "Facile", Points = 1, CategorieId = 3
            },
            new Question {
                Id = 12,
                Texte = "Qu'est-ce que le CSS ?",
                BonneReponse = "Cascading Style Sheets, langage de mise en forme des pages web",
                FausseReponse1 = "Computer Styling System, un logiciel de design graphique",
                FausseReponse2 = "Cool Site Stuff, les trucs sympas qu'on rajoute sur un site",
                Difficulte = "Facile", Points = 1, CategorieId = 3
            },

    // Web — QUESTIONS MOYENNES
            new Question {
                Id = 13,
                Texte = "Qu'est-ce que Blazor ?",
                BonneReponse = "Framework C# pour créer des interfaces web interactives",
                FausseReponse1 = "Un nouveau smartphone concurrent de l'iPhone",
                FausseReponse2 = "Un éditeur de code concurrent de VS Code",
                Difficulte = "Moyen", Points = 2, CategorieId = 3
            },

    // Web — QUESTIONS BOSS
            new Question {
                Id = 14,
                Texte = "Quelle est la différence entre Blazor Server et Blazor WebAssembly ?",
                BonneReponse = "Server s'exécute côté serveur, WebAssembly côté client dans le navigateur",
                FausseReponse1 = "Blazor Server est payant, WebAssembly est gratuit",
                FausseReponse2 = "WebAssembly est plus vieux et Server est la nouvelle version moderne",
                Difficulte = "Boss", Points = 3, CategorieId = 3
            },

    // Algorithmie — QUESTIONS FACILES
            new Question {
                Id = 15,
                Texte = "Qu'est-ce qu'une boucle for ?",
                BonneReponse = "Une structure qui répète un bloc de code un nombre défini de fois",
                FausseReponse1 = "Une boucle réservée aux développeurs qui s'appellent Théo ou Léa",
                FausseReponse2 = "Un raccourci clavier pour dupliquer du code automatiquement",
                Difficulte = "Facile", Points = 1, CategorieId = 4
            },

    // Algorithmie — QUESTIONS MOYENNES
            new Question {
                Id = 16,
                Texte = "Qu'est-ce que la récursivité ?",
                BonneReponse = "Une fonction qui s'appelle elle-même",
                FausseReponse1 = "Un algorithme qui tourne en rond jusqu'à ce que le PC explose",
                FausseReponse2 = "Une technique pour copier-coller du code en boucle",
                Difficulte = "Moyen", Points = 2, CategorieId = 4
            },

    // Algorithmie — QUESTIONS BOSS
            new Question {
                Id = 17,
                Texte = "Quelle est la complexité d'une recherche binaire ?",
                BonneReponse = "O(log n)",
                FausseReponse1 = "O(n²) parce que chercher c'est toujours compliqué",
                FausseReponse2 = "O(café) — ça dépend du nombre de pauses prises",
                Difficulte = "Boss", Points = 3, CategorieId = 4
            }
        );
    }
}
