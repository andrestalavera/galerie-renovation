using BlazorBootstrap;

namespace GalerieRenovation.Web.Services
{
    public class Helpers
    {
        public enum ToastReason
        {
            SendEmailSuccess,
            SendEmailFailure,
            SuscribeNewsletterSuccess,
            SuscribeNewsletterFailure,
            UnsuscribeNewsletterSuccess,
            UnsuscribeNewsletterFailure,
        }
        public static ToastMessage GetToastMessage(ToastReason toastReason) => toastReason switch
        {
            ToastReason.SendEmailSuccess => new()
            {
                Title = "Courriel envoyé",
                Message = "Votre message nous a été transmis. Nous vous répondrons dans les meilleurs délais.",
                Type = ToastType.Success
            },
            ToastReason.SendEmailFailure => new()
            {
                Title = "Courriel non envoyé",
                Message = "Une erreur s'est produite et votre message ne nous a pas été transmis. Veuillez réessayer ultérieurement.",
                Type = ToastType.Danger
            },
            ToastReason.SuscribeNewsletterSuccess => new()
            {
                Title = "Newsletter",
                Message = "Inscription à la lettre d'informations confirmée.",
                Type = ToastType.Success
            },
            ToastReason.SuscribeNewsletterFailure => new()
            {
                Title = "Newsletter",
                Message = "Inscription à la lettre d'informations impossible pour le moment. Veuillez réessayer ultérieurement.",
                Type = ToastType.Danger
            },
            ToastReason.UnsuscribeNewsletterSuccess => throw new NotImplementedException(),
            ToastReason.UnsuscribeNewsletterFailure => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }
}
