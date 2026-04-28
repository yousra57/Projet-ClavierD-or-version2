namespace Projet_ClavierDor.Models;

public class Joker //CLASSE JOKER pour gérer les jokers utilisés par les joueurs pendant les parties
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty; // ChangerQuestion, Rattrapage, Indice
    public bool Utilise { get; set; } = false;

    public int PartieId { get; set; }
    public Partie Partie { get; set; } = null!;
}