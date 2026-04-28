using Microsoft.EntityFrameworkCore;
using Projet_ClavierDor.Data; // accès bdd
using Projet_ClavierDor.Models; // accès aux classes Joueur, Partie, Question, Reponse, Joker

namespace Projet_ClavierDor.Services;

public class ScoreService
{
    // iinjection du contexte de base de données pour accéder aux données des parties, joueurs et réponses
    private readonly AppDbContext _db;

    public ScoreService(AppDbContext db)
    {
        _db = db;
    }


///////// CONSULTATION DES SCORES


    // récupère le score actuel d'une partie en cours (afficher le score en temps réel pendant le quiz)
    public async Task<int> GetScoreAsync(int partieId)
    {
        var partie = await _db.Parties.FindAsync(partieId);
        return partie?.Score ?? 0; // Si la partie n'existe pas, on retourne 0
    }

    // récupère le meilleur score d'un joueur parmi toutes ses parties (afficher le record personnel sur la page d'accueil)
    public async Task<int> GetMeilleurScoreAsync(int joueurId)
    {
        var parties = await _db.Parties
            .Where(p => p.JoueurId == joueurId && p.Etat == "Terminée") // on ne considère que les parties terminées pour le calcul du meilleur score
            .ToListAsync();

        // si le joueur n'a aucune partie terminée, on retourne 0
        if (!parties.Any()) return 0;

        // sinon on retourne le score maximum parmi toutes ses parties
        return parties.Max(p => p.Score);
    }

    // récupère le classement général de tous les joueurs (afficher le tableau des meilleurs scores)
    public async Task<List<Joueur>> GetClassementAsync()
    {
        return await _db.Joueurs
            .Include(j => j.Parties) // inclut les parties liées à chaque joueur
            .OrderByDescending(j => j.Parties
                .Where(p => p.Etat == "Terminée") // classement uniquement sur les parties terminées
                .Select(p => p.Score) // pour chaque joueur, on calcule le meilleur score parmi ses parties terminées
                .DefaultIfEmpty(0) // calcule le meilleur score de chaque joueur parmi ses parties terminées, ou 0 s'il n'en a aucune
                .Max()) // trie par meilleur score décroissant
            .Take(10) // On garde uniquement le top 10
            .ToListAsync();
    }



////// RÉSUMÉ DE FIN DE PARTIE

    // Récupère un résumé complet d'une partie terminée (afficher la page de résultats après le quiz)
    public async Task<Partie?> GetResumPartieAsync(int partieId) 
    {
        return await _db.Parties
            .Include(p => p.Reponses) // inclut toutes les réponses de la partie
            .ThenInclude(r => r.Question) // inclut la question liée à chaque réponse
            .Include(p => p.Joueur) // inclut les infos du joueur
            .FirstOrDefaultAsync(p => p.Id == partieId); 
    }

    // Calcule le pourcentage de bonnes réponses d'une partie (bilan visuel sur la page de résultats)
    public async Task<double> GetPourcentageReussiteAsync(int partieId)
    {
        var reponses = await _db.Reponses // récupère toutes les réponses associées à la partie
            .Where(r => r.PartieId == partieId) 
            .ToListAsync();

        // si aucune réponse enregistrée, on retourne 0
        if (!reponses.Any()) return 0;

        // CALCUL DE POURCENTAGE : nombre de bonnes réponses divisé par le total, multiplié par 100
        int bonnesReponses = reponses.Count(r => r.EstCorrecte); // compte le nombre de réponses correctes
        return Math.Round((double)bonnesReponses / reponses.Count * 100, 1); // calcule le pourcentage et arrondit à 1 décimale
    }
}