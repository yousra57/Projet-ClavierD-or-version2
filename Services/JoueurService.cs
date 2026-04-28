using Microsoft.EntityFrameworkCore; // pour permettre requêtes à la bdd 
using Projet_ClavierDor.Data; // pour accéder au contexte de la bdd
using Projet_ClavierDor.Models; // pour accéder à la classe Joueur et ses propriétés

namespace Projet_ClavierDor.Services;

public class JoueurService
{
    // injection du contexte de bdd pour accéder aux données des joueurs et de leurs parties
    private readonly AppDbContext _db;

    public JoueurService(AppDbContext db)
    {
        _db = db;
    }

    // Créer un nouveau joueur
    public async Task<Joueur> CreerJoueurAsync(string pseudo, string email)
    {
        var joueur = new Joueur
        {
            Pseudo = pseudo, // enregistre le pseudo, mail, et date de création choisi par le joueur
            Email = email,
            DateCreation = DateTime.Now
        };
        _db.Joueurs.Add(joueur); 
        await _db.SaveChangesAsync();
        return joueur;
    }

    // Récupérer un joueur par son pseudo
    public async Task<Joueur?> TrouverJoueurAsync(string pseudo)
    {
        return await _db.Joueurs
            .FirstOrDefaultAsync(j => j.Pseudo == pseudo);
    }

    // Récupérer l'historique des parties d'un joueur
    public async Task<List<Partie>> GetHistoriqueAsync(int joueurId)
    {
        return await _db.Parties
            .Where(p => p.JoueurId == joueurId) // Filtrer les parties du joueur, équivalent aux requêtes SQL : SELECT * FROM Parties WHERE JoueurId = @joueurId
            .OrderByDescending(p => p.DateDebut)
            .ToListAsync();
    }
    // Récupère un joueur par son ID
    public async Task<Joueur?> GetJoueurParIdAsync(int id)
    {
        return await _db.Joueurs.FindAsync(id);
    }
}