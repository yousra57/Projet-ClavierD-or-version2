namespace Projet_ClavierDor.Models; //namespace qui relie tous mes modèles 

public class Joueur // Classe joueur avec propriétés (id, pseudio, email, date de création) 
{
    public int Id { get; set; }
    public string Pseudo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateCreation { get; set; } = DateTime.Now;
    
    //Collection de parties associées au joueur
    public ICollection<Partie> Parties { get; set; } = new List<Partie>(); 
}