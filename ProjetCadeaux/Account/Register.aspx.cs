using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Connection;

namespace ProjetCadeaux.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {

            Compte compte = new Compte();

            //Récupérer informations saisies
            String nom = this.TextBoxNom.Text;
            String prenom =  this.TextBoxPrenom.Text;
            String email = this.Email.Text;
            String login = this.UserName.Text;
            String mdp = Password.Text;

            try
            {
                compte.creerCompte(nom, prenom, email, login, mdp);

                Response.Redirect("~/Account/Login.aspx");
            }
            catch (Exception)
            {
                ErrorMessage.Text = "L'utilisateur n'a pas pu être créé";
            }
        }

    }
}
