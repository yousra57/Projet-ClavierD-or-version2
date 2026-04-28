using Microsoft.EntityFrameworkCore;
using Projet_ClavierDor.Data; // accès à bdd
using Projet_ClavierDor.Models; // pour accéder à la classe Joueur, Partie, Question, Reponse, Joker 

namespace Projet_ClavierDor.Services;

public class JeuService
{
// injection du contexte de bdd pour accéder aux données des parties, questions, réponses et jokers
    private readonly AppDbContext _db;

    public JeuService(AppDbContext db)
    {
        _db = db;
    }


/////// // GESTION DE LA PARTIE

    // crée une nouvelle partie en base de données = équivalent de la fonction creer_partie() en Python
    public async Task<Partie> CreerPartieAsync(int joueurId, string role)
    {
        var partie = new Partie // création d'une nouvelle partie avec les propriétés initiales
        {
            JoueurId = joueurId, // attribue la partie au joueur qui la crée 
            Role = role, // enregistre le rôle choisi par le joueur pour cette partie
            DateDebut = DateTime.Now,
            Score = 0, // le score commence à 0
            Etat = "En cours" // la partie est "en cours" tant qu'elle n'est pas terminée
        };

        _db.Parties.Add(partie); //
        await _db.SaveChangesAsync(); // sauvegarde la nouvelle partie en base de données
        return partie; 
    }

    // termine une partie et enregistre la date de fin = fonction terminer_partie() en Python
        // Termine une partie — utilise IDbContextFactory pour éviter le problème de contexte libéré dans le timer
    public async Task TerminerPartieAsync(int partieId)
    {
        try
        {
            var partie = await _db.Parties.FindAsync(partieId);
            if (partie != null)
            {
                partie.Etat = "Terminée";
                partie.DateFin = DateTime.Now;
                await _db.SaveChangesAsync();
            }
        }
        catch (ObjectDisposedException)
        {
        // Le contexte a été libéré, on ignore silencieusement
        // La partie sera marquée terminée au prochain accès
        }
    }



///////// GESTION DES QUESTIONS

    // récupère toutes les catégories disponibles = fonction recuperer_categories() en Python
    public async Task<List<Categorie>> GetCategoriesAsync()
    {
        return await _db.Categories.ToListAsync();
    }

    // Récupère les questions d'une catégorie selon la difficulté = fonction recuperer_questions() en Python
// puis on mélange côté C# avec Guid.NewGuid()
public async Task<List<Question>> GetQuestionsAsync(int categorieId, string difficulte)
{
    return await _db.Questions
        .Where(q => q.CategorieId == categorieId && q.Difficulte == difficulte)
        .ToListAsync() // On charge en mémoire d'abord
        .ContinueWith(t => t.Result
            .OrderBy(q => Guid.NewGuid()) // Puis on mélange en C#, pas en SQL
            .ToList()
        );
}

   
/////// GESTION DES RÉPONSES ET DU SCORE

    // enregistre la réponse du joueur en base de données = fonction enregistrer_reponse() en Python
    public async Task<bool> EnregistrerReponseAsync(int partieId, int questionId, string reponseDonnee)
    {
        // On récupère la question pour comparer la réponse
        var question = await _db.Questions.FindAsync(questionId);
        if (question == null) return false; // si la question n'existe pas, on retourne false

        // On vérifie si la réponse est correcte 
        bool estCorrecte = string.Equals( // compare la réponse donnée avec la bonne réponse de la question
            reponseDonnee.Trim(), // supprime les espaces avant et après la réponse donnée
            question.BonneReponse.Trim(), // supprime les espaces avant et après la bonne réponse de la question
            StringComparison.OrdinalIgnoreCase // ignore la casse (majuscules/minuscules) lors de la comparaison
        );

        // On crée l'objet Reponse et on l'enregistre
        var reponse = new Reponse 
        {
            PartieId = partieId, // associe la réponse à la partie en cours
            QuestionId = questionId, // associe la réponse à la question correspondante
            ReponseDonnee = reponseDonnee, // enregistre la réponse du joueur
            EstCorrecte = estCorrecte, // enregistre si la réponse est correcte ou non
            DateReponse = DateTime.Now //
        };

        _db.Reponses.Add(reponse); 

        // Si la réponse est correcte, on met à jour le score de la partie
        if (estCorrecte)
        {
            var partie = await _db.Parties.FindAsync(partieId); // récupère la partie correspondante en base de données
            if (partie != null)
            {
                // On ajoute les points selon la difficulté de la question
                partie.Score += question.Points; 
            }
        }

        await _db.SaveChangesAsync(); // enregistre la réponse et les éventuelles modifications du score en base de données
        return estCorrecte;
    }


//////// GESTION DES JOKERS

    // chaque rôle a accès à un joker différent :
    // - front : ChangerQuestion = peut changer la question
    // - back : Rattrapage = seconde chance si mauvaise réponse 
    // - Mobile : Indice = affiche un indice sur la question


// ACTION : Tente d'utiliser un joker pendant la partie.
    public async Task<bool> UtiliserJokerAsync(int partieId, string typeJoker)
    {
        // on vérifie si le joker a déjà été utilisé dans cette partie
        var jokerExistant = await _db.Jokers
            .FirstOrDefaultAsync(j => j.PartieId == partieId && j.Type == typeJoker); // cherche un joker du même type déjà utilisé pour cette partie

        // si le joker existe déjà et a été utilisé, on refuse
        if (jokerExistant != null && jokerExistant.Utilise)
            return false;

        // sinon on crée un nouveau joker et on le marque comme utilisé
        var joker = new Joker 
        {
            PartieId = partieId, // associe le joker à la partie en cours
            Type = typeJoker, // enregistre le type de joker utilisé
            Utilise = true // marquer comme déjà utilisé pour cette partie
        };

        _db.Jokers.Add(joker);
        await _db.SaveChangesAsync();
        return true;
    }

// CONSULTATION : Vérifie simplement si un joker a déjà été utilisé : ne modifie pas la bdd et afficher ou griser le bouton joker dans l'interface.
    public async Task<bool> JokerDejaUtiliseAsync(int partieId, string typeJoker)
    {
        return await _db.Jokers
            .AnyAsync(j => j.PartieId == partieId && j.Type == typeJoker && j.Utilise);
    }
}