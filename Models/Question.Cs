namespace Projet_ClavierDor.Models;

public class Question // classe Question avec propriétés (id, texte, bonne réponse, fausses réponses, difficulté, points) 
{
    public int Id { get; set; }
    public string Texte { get; set; } = string.Empty;
    public string BonneReponse { get; set; } = string.Empty;
    public string FausseReponse1 { get; set; } = string.Empty;
    public string FausseReponse2 { get; set; } = string.Empty;
    public string Difficulte { get; set; } = "Facile";
    public int Points { get; set; } = 1;

    public int CategorieId { get; set; }
    public Categorie Categorie { get; set; } = null!;

    public ICollection<Reponse> Reponses { get; set; } = new List<Reponse>(); // Collection de réponses associées à la question
}
