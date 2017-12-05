using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Connection;
using System.Data;
using ProjetCadeauxBLL;
using ProjetCadeaux_BLL;

namespace ProjetCadeaux.Account
{
    public partial class MonCompte : System.Web.UI.Page
    {

        /// <summary>
        /// Action exécutée au chargement de la page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si l'utilisateur est connecté, on précharge ses informations personnelles
            if (Session["connecte"] != null && Session["connecte"].ToString() == "true")
            {
                if (ViewState["monCompteCharge"] == null || ViewState["monCompteCharge"].ToString() != "true")
                {
                    Nom.Text = Session["personneNom"].ToString();
                    Prenom.Text = Session["personnePrenom"].ToString();
                    Email.Text = Session["personneEmail"].ToString();

                    oldMotDePasseTb.Text = null;
                    newMotDePasse1Tb.Text = null;
                    newMotDePasse1Tb.Text = null;

                    ViewState["monCompteCharge"] = "true";
                }

            }
            else
            {
                Response.Redirect("~/pages/Account/Login.aspx");
            }
        }

        /// <summary>
        /// Modifie le mot de passe de l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModifierMotDePasse_Click(object sender, EventArgs e)
        {

            if (Session["connecte"].ToString() == "true")
            {
                Boolean retour;

                MonCompteBLL compteBLL = new MonCompteBLL();

                String idPersonne = Session["personneID"].ToString();
                String oldMotDePasse = oldMotDePasseTb.Text;
                String newMotDePasse1 = newMotDePasse1Tb.Text;
                String newMotDePasse2 = newMotDePasse1Tb.Text;

                if (StringUtils.estNonNullNiVide(oldMotDePasse)
                    && StringUtils.estNonNullNiVide(newMotDePasse1)
                    && StringUtils.estNonNullNiVide(newMotDePasse2))
                {
                    try
                    {
                        retour = compteBLL.modifierMotDePasse(idPersonne,
                            oldMotDePasse,
                            newMotDePasse1,
                            newMotDePasse2);

                        if (retour)
                        {
                            FailureText.Text = null;
                            SuccessText.Text = "La modification du mot de passe a bien été prise en compte !";

                            ViewState["monCompteCharge"] = "false";
                            this.Page_Load(sender, e);
                        }
                        else
                        {
                            SuccessText.Text = null;
                            FailureText.Text = "La modification du mot de passe n'a pas été prise en compte !";

                            ViewState["monCompteCharge"] = "false";
                            this.Page_Load(sender, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        SuccessText.Text = null;
                        FailureText.Text = "La modification du mot de passe n'a pas été prise en compte : " + ex.Message;

                        ViewState["monCompteCharge"] = "false";
                        this.Page_Load(sender, e);
                    }

                }
                else
                {
                    SuccessText.Text = null;
                    FailureText.Text = "La modification du mot de passe n'a pas été prise en compte.";

                    ViewState["monCompteCharge"] = "false";
                    this.Page_Load(sender, e);
                }

            }
            //L'utilisateur a été déconnecté
            else
            {
                ViewState["monCompteCharge"] = "false";
                Response.Redirect("~/pages/Account/Login.aspx");
            }
        }

        /// <summary>
        /// Modifie les informations personnelles de l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModifierInfos_Click(object sender, EventArgs e)
        {
            if (Session["connecte"].ToString() == "true")
            {
                Boolean retour;

                MonCompteBLL compteBLL = new MonCompteBLL();

                String idPersonne = Session["personneID"].ToString();
                String nom = Nom.Text;
                String prenom = Prenom.Text;
                String email = Email.Text;
                String oldEmail = Session["personneEmail"].ToString();

                try
                {
                    if (StringUtils.estUnEmail(email))
                        retour = compteBLL.modifierInfos(idPersonne, nom, prenom, email, oldEmail);
                    else
                    {
                        EmailRequired.IsValid = false;
                        retour = false;
                    }

                    if (retour)
                    {
                        // Mettre à jour la session
                        Session["personnePrenom"] = prenom;
                        Session["personneNom"] = nom;
                        Session["personneEmail"] = email;

                        //Rediriger là où on veut avec un message de confirmation que tout s'est bien passé.
                        FailureText.Text = null;
                        SuccessText.Text = "Vos informations ont bien été modifiées !";

                        ViewState["monCompteCharge"] = "false";
                        this.Page_Load(sender, e);
                    }
                    else
                    {
                        //Retourner le message d'erreur qui va bien.
                        SuccessText.Text = null;
                        FailureText.Text = "Les informations n'ont pas pu être modifiées.";

                        ViewState["monCompteCharge"] = "false";
                        this.Page_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    SuccessText.Text = null;
                    FailureText.Text = "Les informations n'ont pas pu être modifiées : " + ex.Message;

                    ViewState["monCompteCharge"] = "false";
                    this.Page_Load(sender, e);
                }
            }
            //L'utilisateur a été déconnecté.
            else
            {
                ViewState["monCompteCharge"] = "false";
                Response.Redirect("~/pages/Account/Login.aspx");
            }

        }
    }
}
