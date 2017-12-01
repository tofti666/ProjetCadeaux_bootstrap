using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Connection;
using System.Data;

namespace ProjetCadeaux.pages
{
    public partial class Suggestions : System.Web.UI.Page
    {   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null)
            {
                if (ViewState["listePersonnesChargee"] == null || ViewState["listePersonnesChargee"].ToString() != "true")
                {
                    Compte cpt = new Compte();
                    DataTable personnes = cpt.getPersonnesInscrites(Session["personneID"].ToString());

                    DataTable retour = new DataTable();

                    retour.Columns.Add("ID");
                    retour.Columns.Add("nom");

                    DataRow dr2 = retour.NewRow();
                    dr2["ID"] = "";
                    dr2["nom"] = "--";
                    retour.Rows.Add(dr2);

                    //List<String> truc = new List<String>();
                    for (int i = 0; i < personnes.Rows.Count; i++)
                    {

                        String id = personnes.Rows[i].ItemArray.GetValue(0).ToString();
                        String nom = personnes.Rows[i].ItemArray.GetValue(1).ToString();
                        String prenom = personnes.Rows[i].ItemArray.GetValue(2).ToString();

                        DataRow dr = retour.NewRow();
                        dr["ID"] = id;
                        dr["nom"] = prenom + " " + nom;

                        retour.Rows.Add(dr);
                    }

                    listePersonnes.DataValueField = "ID";
                    listePersonnes.DataTextField = "nom";

                    listePersonnes.DataSource = retour;
                    listePersonnes.DataBind();
                    ViewState["listePersonnesChargee"] = "true";
                }

                Refresh();
                
            }
            else
            {
                Response.Redirect("~/");
            }

        }

        protected void listePersonnes_onIndexChanged(object sender, EventArgs e)
        {

            if (listePersonnes.SelectedItem.Value != "")
            {

                IdeesCadeauxProposes idee = new IdeesCadeauxProposes();

                DataTable idees = new DataTable();

                idees = idee.getIdeesCadeauxProposesByPersonne(listePersonnes.SelectedItem.Value);

                gridViewSuggestion.DataSource = idees;
                gridViewSuggestion.DataBind();

                ViewState["clePersonneSuggestionSelected"] = listePersonnes.SelectedItem.Value;

                lbl_nomSelectionne.Text = " de " + listePersonnes.SelectedItem.Text + " :";
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

            IdeesCadeauxProposes ideeCadeau = new IdeesCadeauxProposes();

            String titre = TitreCadeau.Text;
            String description = Description.Text;
            String prix = Prix.Text;
            String personne = listePersonnes.SelectedItem.Value.ToString();
            String priorite = Priorite.Text;

            ideeCadeau.ajouterIdeeCadeau(titre, description, prix, priorite, personne, Session["personneID"].ToString(),
                tb_lien1.Text, tb_lien2.Text, tb_lien3.Text, tb_lien4.Text, tb_lien5.Text);            

            viderForm();

            FailureTextSuggestions.Text = "L'idée a bien été ajoutée.";

        }

        protected void btnModifierIdee_Click(object sender, EventArgs e)
        {

            IdeesCadeauxProposes ideeCadeau = new IdeesCadeauxProposes();

            String titre = TitreCadeau.Text;
            String description = Description.Text;
            String prix = Prix.Text;
            String priorite = Priorite.Text;
            String cle = "";

            if (ViewState["cleIdeeCadeauProposeModification"] != null)
            {
                cle = ViewState["cleIdeeCadeauProposeModification"].ToString();
            }

            ideeCadeau.modifierIdeeCadeauPropose(cle, titre, description, prix, priorite,
                tb_lien1.Text, tb_lien2.Text, tb_lien3.Text, tb_lien4.Text, tb_lien5.Text);

            viderForm();

            FailureTextSuggestions.Text = "L'idée a bien été modifiée.";

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

                IdeesCadeauxProposes idee = new IdeesCadeauxProposes();

                String cle = gridViewSuggestion.DataKeys[index].Value.ToString();

                idee.deleteIdeeCadeauPropose(cle);

                Refresh();

            }
            #endregion
            #region action modification
            else if (e.CommandName == "modifier")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                IdeesCadeauxProposes idee = new IdeesCadeauxProposes();
                Liens lien = new Liens();

                String cle = gridViewSuggestion.DataKeys[index].Value.ToString();

                ViewState["cleIdeeCadeauProposeModification"] = cle;

                viderForm();
                updatePanelAjoutCadeaux.Visible = true;
                updatePanelIdeesCadeaux.Visible = false;

                btn_AjouterCadeau.Visible = false;
                btn_ModifierCadeau.Visible = true;

                DataTable dt = idee.getIdeeCadeauProposeById(cle);

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

        private void Refresh()
        {
            if (ViewState != null && ViewState["clePersonneSuggestionSelected"] != null)
            {
                IdeesCadeauxProposes ideesCadeaux = new IdeesCadeauxProposes();

                gridViewSuggestion.DataSource = ideesCadeaux.getIdeesCadeauxProposesByPersonne(ViewState["clePersonneSuggestionSelected"].ToString());

                gridViewSuggestion.DataBind();
                FailureTextSuggestions.Text = "";
            }
        }
    }
}