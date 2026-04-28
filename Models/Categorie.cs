namespace Projet_ClavierDor.Models;

public class Categorie //Classe Catégorie
{
    public int Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Question> Questions { get; set; } = new List<Question>(); //Collection questions associées à la catégorie
}