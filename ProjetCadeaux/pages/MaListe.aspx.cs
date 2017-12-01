using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Connection;
using System.Data;
using ProjetCadeauxBLL;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux.pages
{
    public partial class MaListe : System.Web.UI.Page
    {   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChargerListeEvenement();
            }
            if (Page.IsPostBack)
            {
                Refresh();
            }
        }

        protected void btnAjoutIdee_Click(object sender, EventArgs e)
        {
            updatePanelAjoutCadeaux.Visible = true;
            updatePanelIdeesCadeaux.Visible = false;

            btn_ModifierCadeau.Visible = false;
            btn_AjouterCadeau.Visible = true;
        }

        protected void btnAjouterIdee_Click(object sender, EventArgs e)
        {

            IdeesCadeaux ideeCadeau = new IdeesCadeaux();
            ListeIdeesCadeauxBLL listeService = new ListeIdeesCadeauxBLL();

            Evenement evt = new Evenement();
            Personne pers = new Personne();
            IdeeCadeau idee = new IdeeCadeau();

            Boolean retour = true;

            String titre = TitreCadeau.Text;
            String description = Description.Text;
            String prix = Prix.Text;
            String personne = Session["personneID"].ToString();
            String priorite = Priorite.Text;

            String evenementId = ViewState["evenementIdSelection"].ToString();

            String ideeCadeau_id = ideeCadeau.ajouterIdeeCadeau(titre, description, prix, priorite, personne,
                tb_lien1.Text, tb_lien2.Text, tb_lien3.Text, tb_lien4.Text, tb_lien5.Text);

            evt.id_evenement = int.Parse(evenementId);
            pers.id_personne = int.Parse(personne);
            idee.id_ideeCadeau = int.Parse(ideeCadeau_id);


            if (ideeCadeau_id != null)
                retour = listeService.ajouterCadeauToListe(pers,evt,idee);

            if (retour)
            {
                viderForm();

                FailureText.Text = "";
                SuccesText.Text = "L'idée a bien été ajoutée.";
            }
            else
            {
                SuccesText.Text = "";
                FailureText.Text = "L'idée n'a pas été correctement ajoutée.";
            }

        }

        protected void btnModifierIdee_Click(object sender, EventArgs e)
        {

            IdeesCadeaux ideeCadeau = new IdeesCadeaux();

            String titre = TitreCadeau.Text;
            String description = Description.Text;
            String prix = Prix.Text;
            String priorite = Priorite.Text;
            String cle = "";

            String evenementId = ViewState["evenementIdSelection"].ToString();

            if (ViewState["cleIdeeCadeauModification"] != null)
            {
                cle = ViewState["cleIdeeCadeauModification"].ToString();
            }

            ideeCadeau.modifierIdeeCadeau(cle, titre, description, prix, priorite,
                tb_lien1.Text, tb_lien2.Text, tb_lien3.Text, tb_lien4.Text, tb_lien5.Text);

            viderForm();

            SuccesText.Text = "L'idée a bien été modifiée.";

        }    

        protected void btnVoirMaListe_Click(object sender, EventArgs e)
        {
            updatePanelAjoutCadeaux.Visible = false;
            updatePanelIdeesCadeaux.Visible = true;

            viderForm();
        }

        private void viderForm()
        {
            TitreCadeau.Text = "";
            Description.Text = "";
            Prix.Text = "";
            Priorite.Text = "";
            tb_lien1.Text = "";
            tb_lien2.Text = "";
            tb_lien3.Text = "";
            tb_lien4.Text = "";
            tb_lien5.Text = "";
        }

        protected void RowCommand_click(object sender, GridViewCommandEventArgs e)
        {
            #region action suppression
            if (e.CommandName == "supprimer")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                ListeIdeesCadeauxBLL idee = new ListeIdeesCadeauxBLL();

                String cle = gridViewMesCadeaux.DataKeys[index].Value.ToString();
                IdeeCadeauPourListe ideeCadeau = new IdeeCadeauPourListe();

                ideeCadeau.id_ideeCadeau = int.Parse(cle);

                idee.supprimerCadeauFromListe(ideeCadeau);

                Refresh();

            }
            #endregion
            #region action modification
            else if (e.CommandName == "modifier")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                IdeesCadeaux idee = new IdeesCadeaux();
                Liens lien = new Liens();

                String cle = gridViewMesCadeaux.DataKeys[index].Value.ToString();

                ViewState["cleIdeeCadeauModification"] = cle;

                viderForm();
                updatePanelAjoutCadeaux.Visible = true;
                updatePanelIdeesCadeaux.Visible = false;

                btn_AjouterCadeau.Visible = false;
                btn_ModifierCadeau.Visible = true;

                DataTable dt = idee.getIdeeCadeauById(cle);

                TitreCadeau.Text = dt.Rows[0].ItemArray.GetValue(1).ToString();
                Description.Text = dt.Rows[0].ItemArray.GetValue(2).ToString();
                Prix.Text = dt.Rows[0].ItemArray.GetValue(3).ToString();
                Priorite.Text = dt.Rows[0].ItemArray.GetValue(0).ToString();

                DataTable dt2 = lien.getInfosLiensByIdeeCadeau(cle);

                for(int i=0; i<dt2.Rows.Count; i++){
                    switch (i)
                    {
                        case 0 :
                            tb_lien1.Text = dt2.Rows[i].ItemArray.GetValue(0).ToString();
                            break;
                        case 1:
                            tb_lien2.Text = dt2.Rows[i].ItemArray.GetValue(0).ToString();
                            break;
                        case 2:
                            tb_lien3.Text = dt2.Rows[i].ItemArray.GetValue(0).ToString();
                            break;
                        case 3:
                            tb_lien4.Text = dt2.Rows[i].ItemArray.GetValue(0).ToString();
                            break;
                        case 4:
                            tb_lien5.Text = dt2.Rows[i].ItemArray.GetValue(0).ToString();
                            break;
                    }
                }
                
            }
            #endregion
        }

        private void ChargerListeEvenement()
        {
            if (Session["connecte"] != null)
            {

                int personneId = int.Parse(Session["personneID"].ToString());
                Personne pers = new Personne();
                pers.id_personne = personneId;

                EvenementBLL evtService = new EvenementBLL();

                ddl_choixEvenement.DataSource = evtService.getAllEvenementAvecListeByIdPersonne(pers);
                ddl_choixEvenement.DataTextField = "libelle";
                ddl_choixEvenement.DataValueField = "id_evenement";

                ddl_choixEvenement.DataBind();

                if(ddl_choixEvenement.SelectedValue != null)
                    ViewState["evenementIdSelection"] = ddl_choixEvenement.SelectedValue;
                
                FailureText.Text = "";
                
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        private void Refresh()
        {
            if (Session["connecte"] != null)
            {
                if (ddl_choixEvenement.DataValueField != null)
                {
                    Personne pers = new Personne();
                    Evenement evt = new Evenement();

                    evt.id_evenement = int.Parse(ddl_choixEvenement.SelectedValue);
                    pers.id_personne = int.Parse(Session["personneID"].ToString());

                    ListeIdeesCadeauxBLL listeService = new ListeIdeesCadeauxBLL();

                    ListeIdeesCadeaux listeRetour = listeService.getListeIdeesCadeauxPasSuggere(pers, evt);

                    if (listeRetour != null)
                    {
                        gridViewMesCadeaux.DataSource = listeRetour.listeDeCadeaux;
                        gridViewMesCadeaux.DataBind();
                    }
                    else
                    {
                        gridViewMesCadeaux.DataSource = null;
                        gridViewMesCadeaux.DataBind();
                    }
                }
                else
                {
                    gridViewMesCadeaux.DataSource = null;
                    gridViewMesCadeaux.DataBind();
                }
            }
            else
            {
                Response.Redirect("~/");
            }

        }

        protected void choixEvenement_SelectIndexChanged(object sender, EventArgs e)
        {
            ViewState["evenementIdSelection"] = ddl_choixEvenement.SelectedValue;

            Refresh();
        }
    }
}