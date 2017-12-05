using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using EASendMail;

namespace ProjetCadeaux_Connection
{
    public class GestionMail
    {

        public GestionMail(string destinataires, string login, string nom, string nouveauMdp)
        {
            MailMessage message = new MailMessage();

            message.From = new System.Net.Mail.MailAddress("Christophe@sitedescadeaux.fr", "webmestre Site des Cadeaux");
            message.To.Add(destinataires);
            message.Subject = "Réinitialisation du mot de passe";

            message.IsBodyHtml = true;

            message.Body = "Bonjour <strong>" + nom + "</strong> !<br /><br /><p>Vous avez demandé la réinitialisation de votre mot de passe pour le compte : <strong>" + login + "</strong>.<br/><br/>" +
                "La réinitialisation du mot de passe est effective. Votre nouveau mot de passe est : <strong>" + nouveauMdp + "</strong>.<br />Notez le sur un papier, ou profitez-en pour le modifier."
                + "<br /><br />A très bientôt sur le site.<br />Christophe.";

            envoiMail(message);

        }

        public void envoiMail(MailMessage mail)
        {

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.bbox.fr", 25);

            //Kx$STuv*43N-6AbD
            smtp.Credentials = new System.Net.NetworkCredential("destips.christophe@bbox.fr", "Christophe$91");
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Timeout = 4000;

            smtp.Send(mail);
         
        }

    }
}
