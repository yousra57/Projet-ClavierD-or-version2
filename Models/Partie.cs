namespace Projet_ClavierDor.Models; 

public class Partie // Classe Partie (id, date de début, date de fin, score, état, rôle) avec une relation vers Joueur
{
    public int Id { get; set; }
    public DateTime DateDebut { get; set; } = DateTime.Now;
    public DateTime? DateFin { get; set; }
    public int Score { get; set; } = 0;
    public string Etat { get; set; } = "En cours";
    public string Role { get; set; } = string.Empty;

    public int JoueurId { get; set; }
    public Joueur Joueur { get; set; } = null!;

    public ICollection<Reponse> Reponses { get; set; } = new List<Reponse>(); //Collection de réponses associées à la partie
}