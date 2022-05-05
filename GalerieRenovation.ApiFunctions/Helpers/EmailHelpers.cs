using System.Text;

namespace GalerieRenovation.ApiFunctions.Helpers
{
    internal class EmailHelpers
    {
        public static string BuildEmail(string content)
        {
            StringBuilder builder = new();
            builder.Append("<div style='background:#0f0704;color:white;padding:5px; margin:5px;'><h2>Galerie Rénovation</h2></div><div style='padding:5px; margin:5px;'>");
            builder.Append(content);
            builder.Append("</div><div style='font-size:xx-small;background:#0f0704;color:white;padding:5px; margin:5px;'>");
            builder.Append(@"Imprimer ce courriel que si c'est nécessaire.
Courriel envoyé par la société Galerie Rénovation – 18 rue du Général Leclerc – 67110 Reichshoffen - France
Galerie Rénovation est une SASU - SIRET 851 328 773 – RCS DE STRASBOURG
Ce courriel et toutes les pièces jointes sont établis à l'intention exclusive de ses destinataires et sont confidentiels. Si vous recevez ce courriel par erreur, merci de le supprimer et d'en avertir l'expéditeur. 
Toute utilisation de ce courriel non conforme à sa destination, toute diffusion ou toute publication, totale ou partielle, est interdite, sauf autorisation expresse. 
Internet ne permettant pas d'assurer l'intégrité de ce message, la société IdeaStud.io décline toute responsabilité au titre de ce message, dans l'hypothèse où il aurait été modifié.");
            builder.Append("</div>");
            return builder.ToString();
        }
    }
}
