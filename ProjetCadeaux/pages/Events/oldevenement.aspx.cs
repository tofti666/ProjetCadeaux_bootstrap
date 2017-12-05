using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeauxBLL;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux.pages.Events
{
    public partial class oldevenement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void btnCreerEvt_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/Events/creerEvenement.aspx");
        }

        protected void RowCommand_click(object sender, GridViewCommandEventArgs e)
        {
            #region action consultation évènement
            if (e.CommandName == "voir")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                String cle = gridViewMesEvenements.DataKeys[index].Value.ToString();

                Response.Redirect("~/pages/Events/voirEvenement.aspx?evenementId="+cle);
            }
            #endregion
            //#region action administration
            //else if (e.CommandName == "admin")
            //{

            //    int index = Convert.ToInt32(e.CommandArgument);

            //    String cle = gridViewMesEvenements.DataKeys[index].Value.ToString();

            //    Response.Redirect("~/pages/Events/gestionEvenement.aspx?evenementId="+cle);
            //}
            //#endregion
        }


        private void Refresh()
        {
            if (Session["connecte"] != null)
            {

                EvenementBLL evtBLL = new EvenementBLL();

                string id_personne = Session["personneID"].ToString();

                List<Evenement> listeRetour = evtBLL.getAllEvenementByIdPersonne(id_personne);

                gridViewMesEvenements.DataSource = listeRetour;

                gridViewMesEvenements.DataBind();
            }
            else
            {
                Response.Redirect("~/");
            }
        }
    }
}