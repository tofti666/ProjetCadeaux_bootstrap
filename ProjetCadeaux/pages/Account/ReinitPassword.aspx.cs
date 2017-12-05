using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Connection;
using System.Data;

namespace ProjetCadeaux.Account
{
    public partial class ReinitPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void ReinitPassword_Click(object sender, EventArgs e)
        {
            Compte compte = new Compte();

            String email = EmailRecover.Text;

            String mdp = compte.reinitialiserMotDePasse(email);
            DataTable dt = compte.getInfosPersonne(email);

            if (mdp != null && dt != null && dt.Rows.Count > 0)
            {
                String nom = dt.Rows[0].ItemArray.GetValue(0).ToString();
                String prenom = dt.Rows[0].ItemArray.GetValue(1).ToString();
                String login = dt.Rows[0].ItemArray.GetValue(2).ToString();

                GestionMail gMail = new GestionMail(EmailRecover.Text, login, prenom + " "+ nom , mdp);

                Response.Redirect("~/pages/Account/ReinitPasswordSuccess.aspx");
            }
            else
            {
                FailureText.Text = "L'identification a échoué";
            }

        }
    }
}
