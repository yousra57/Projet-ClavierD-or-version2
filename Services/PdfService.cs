using QuestPDF.Fluent; // pour construire le document PDF de manière fluide
using QuestPDF.Helpers; // pour les couleurs et tailles de page
using QuestPDF.Infrastructure; // pour les interfaces de construction de document
using Projet_ClavierDor.Models;

namespace Projet_ClavierDor.Services;

public class PdfService
{
    public PdfService()
    {
        // Licence QuestPDF gratuite pour usage communautaire
        QuestPDF.Settings.License = LicenseType.Community;
    }

    // Génère un PDF complet des résultats d'une partie
    // Retourne un tableau de bytes prêt à être téléchargé
    public byte[] GenererPdf(Partie partie)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Element(ComposeHeader);

                page.Content().Element(content =>
                    ComposeContent(content, partie));

                // Pied de page avec date et numéro de page
                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Clavier d'Or — Généré le ")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                    text.Span(DateTime.Now.ToString("dd/MM/yyyy à HH:mm"))
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                    text.Span(" — Page ")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                    text.CurrentPageNumber()
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        }).GeneratePdf();
    }

    // EN-TÊTE DU PDF
    private void ComposeHeader(IContainer container)
{
    container.Column(col =>
    {
        col.Item().Row(row =>
        {
            row.RelativeItem().Column(c =>
            {
                c.Item().Text("Clavier d'Or")
                    .FontSize(24)
                    .FontColor(Color.FromHex("#9771de"))
                    .Bold();

                c.Item().Text("Résultats de partie")
                    .FontSize(13)
                    .FontColor(Colors.Grey.Medium);
            });

            // Barre décorative orange à droite
            row.ConstantItem(3).Background(Color.FromHex("#fdb735"));
        });

        // Ligne horizontale sous l'en-tête
        col.Item().PaddingTop(8).BorderBottom(2)
            .BorderColor(Color.FromHex("#6C3FBF"))
            .Text("");
    });
}

    // CONTENU PRINCIPAL DU PDF
    private void ComposeContent(IContainer container, Partie partie)
    {
        // Calcul du pourcentage fait une seule fois pour tout le contenu
        var total = partie.Reponses.Count;
        var bonnes = partie.Reponses.Count(r => r.EstCorrecte);
        var pct = total > 0 ? Math.Round((double)bonnes / total * 100, 1) : 0;

        container.Column(col =>
        {
            col.Spacing(16);

            // ---- Bloc infos joueur ----
            col.Item().Background(Color.FromHex("#f5f0ff"))
                .Padding(12)
                .Column(info =>
                {
                    info.Item().Text("Informations du joueur")
                        .FontSize(13)
                        .FontColor(Color.FromHex("#6C3FBF"))
                        .Bold();

                    info.Item().PaddingTop(8).Row(row =>
                    {
                        // Colonne gauche : pseudo et rôle
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text($"Joueur : {partie.Joueur?.Pseudo}").FontSize(11);
                            c.Item().Text($"Rôle : {partie.Role}").FontSize(11);
                        });

                        // Colonne droite : date de début de partie
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text($"Date : {partie.DateDebut:dd/MM/yyyy}").FontSize(11);
                        });
                    });
                });

            // ---- Bloc score ----
            col.Item().Background(Color.FromHex("#fff8e1"))
                .Padding(12)
                .Column(score =>
                {
                    score.Item().Text("Résultat final")
                        .FontSize(13)
                        .FontColor(Color.FromHex("#FFA500"))
                        .Bold();

                    score.Item().PaddingTop(8).Column(c =>
                    {
                        c.Item().Text($"Score total : {partie.Score} points")
                            .FontSize(14).Bold();

                        c.Item().Text($"Bonnes réponses : {bonnes}/{total} ({pct}%)")
                            .FontSize(11).FontColor(Colors.Grey.Darken2);

                        // Mention selon le score obtenu
                        c.Item().PaddingTop(4).Text(GetMention(pct))
                            .FontSize(12).Bold().FontColor(GetCouleurMention(pct));
                    });
                });

            // ---- Tableau des réponses ----
col.Item().Column(tableau =>
{
    tableau.Item().Text("Détail des réponses")
        .FontSize(13)
        .FontColor(Color.FromHex("#d877fc"))
        .Bold();

    tableau.Item().PaddingTop(8).Table(table =>
    {
        //  2 colonnes : question et résultat
        table.ColumnsDefinition(columns =>
        {
            columns.RelativeColumn(4); // Question
            columns.ConstantColumn(80); // Résultat
        });

        // En-tête simplifié
        table.Header(header =>
        {
            header.Cell().Background(Color.FromHex("#ffc27c"))
                .Padding(6).Text("Question")
                .FontColor(Colors.White).Bold().FontSize(10);

            header.Cell().Background(Color.FromHex("#ffc27c"))
                .Padding(6).Text("Résultat")
                .FontColor(Colors.White).Bold().FontSize(10);
        });

        // lignes simplifiées : question + correct/incorrect
        bool ligneAlternee = false;
        foreach (var reponse in partie.Reponses)
        {
            var couleurLigne = ligneAlternee
                ? Color.FromHex("#fafafa")
                : Colors.White;
            ligneAlternee = !ligneAlternee;

            table.Cell().Background(couleurLigne)
                .Padding(5).Text(reponse.Question?.Texte ?? "").FontSize(9);

            table.Cell()
                .Background(reponse.EstCorrecte
                    ? Color.FromHex("#e8f5e9")
                    : Color.FromHex("#ffebee"))
                .Padding(5).AlignCenter()
                .Text(reponse.EstCorrecte ? "OK" : "X")
                .FontSize(10)
                .FontColor(reponse.EstCorrecte
                    ? Color.FromHex("#2e7d32")
                    : Color.FromHex("#b32121"))
                .Bold();
        }
    });
});
        });
    }

    
    // MÉTHODES UTILITAIRES

    // Retourne la mention selon le pourcentage de réussite
    private string GetMention(double pct) => pct switch
    {
        >= 80 => "Mention : Clavier d'Or",
        >= 60 => "Mention : Bien",
        >= 40 => "Mention : Assez bien",
        _ => "Mention : Encouragements"
    };

    // Retourne la couleur de la mention selon le score
    private Color GetCouleurMention(double pct) => pct switch
    {
        >= 80 => Color.FromHex("#FFA500"),
        >= 60 => Color.FromHex("#4CAF50"),
        >= 40 => Color.FromHex("#6C3FBF"),
        _ => Color.FromHex("#ef4444")
    };
}