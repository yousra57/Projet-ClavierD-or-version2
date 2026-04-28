namespace Projet_ClavierDor.Models;

public class Reponse //CLASSE REPONSE
{
    public int Id { get; set; }
    public string ReponseDonnee { get; set; } = string.Empty;
    public bool EstCorrecte { get; set; }
    public DateTime DateReponse { get; set; } = DateTime.Now;

    public int PartieId { get; set; }
    public Partie Partie { get; set; } = null!;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}