using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace ProjetCadeaux
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session != null && Session["connecte"].ToString().Equals("true"))
                {
                    HeadLoginStatus.Visible = false;
                    panelDeconnection.Visible = true;
                    NomConnecte.Text = Session["personneLogin"] != null ? Session["personneLogin"].ToString() : "";
                    constructionMenu(true);
                }
                else
                {
                    constructionMenu(false);
                    HeadLoginStatus.Visible = true;
                    panelDeconnection.Visible = false;
                }
            }
            catch (Exception)
            {
                constructionMenu(false);
                HeadLoginStatus.Visible = true;
                panelDeconnection.Visible = false;
            }
        }

        protected void deconnection_click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Redirect("~/");
        }

        protected void constructionMenu(Boolean connected)
        {
            menu_compte.Visible = connected;
            ddl_cadeaux.Visible = connected;
            DeconnectionLi.Visible = connected;

            //if (connected)
            //{
                //MenuItem menuDefault = new MenuItem("Accueil", "", "", "~/Default.aspx");
                //NavigationMenu.Items.Add(menuDefault);
                //MenuItem menuCompte = new MenuItem("Mon compte", "", "", "~/pages/Account/MonCompte.aspx");
                //NavigationMenu.Items.Add(menuCompte);
                //MenuItem menuEvenements = new MenuItem("Mes évènements", "", "", "~/pages/Events/evenement.aspx");
                //NavigationMenu.Items.Add(menuEvenements);
                //MenuItem menuMaListe = new MenuItem("Remplir mes listes de cadeaux", "", "", "~/pages/MaListe.aspx");
                //NavigationMenu.Items.Add(menuMaListe);
                //MenuItem menuParticipation = new MenuItem("Voir le résumé des participations", "", "", "~/pages/Participations.aspx");
                //NavigationMenu.Items.Add(menuParticipation);
                //MenuItem menuResponsable = new MenuItem("Mes responsabilités", "", "", "~/pages/Responsable.aspx");
                //NavigationMenu.Items.Add(menuResponsable);
                //MenuItem menuAbout = new MenuItem("Exemples", "", "", "~/About.aspx");
                //NavigationMenu.Items.Add(menuAbout);
            //}
            //else
            //{
                //MenuItem menuDefault = new MenuItem("Accueil", "", "", "~/Default.aspx");
                //NavigationMenu.Items.Add(menuDefault);
                //MenuItem menuAbout = new MenuItem("Exemples", "", "", "~/About.aspx");
                //NavigationMenu.Items.Add(menuAbout);
            //}
        }
    }
}
