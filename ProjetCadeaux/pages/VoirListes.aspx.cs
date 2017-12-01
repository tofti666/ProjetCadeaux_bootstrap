using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Connection;
using System.Data;
using ProjetCadeaux_Entites;
using ProjetCadeauxBLL;

namespace ProjetCadeaux
{
    public partial class VoirListes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null)
            {
                if (ViewState["listePersonnesChargee"] == null || ViewState["listePersonnesChargee"].ToString() != "true")
                {
                    Compte compte = new Compte();
                    DataTable personnes = compte.getPersonnesInscrites(Session["personneID"].ToString());

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
                #region chargement liste souhaits
                updatePanelListePropositions.Visible = true;
                updatePanel_participationCadeau.Visible = false;
                updatePanelCommentaires.Visible = false;

                RechargerListeIdeesCadeaux();

                lbl_nomSelectionne.Text = " de " + listePersonnes.SelectedItem.Text + " :";
                #endregion

                #region chargement liste suggestions

                RechargerListeIdeesCadeauxSuggeres();

                lbl_nomSelectionneSuggestion.Text = " pour " + listePersonnes.SelectedItem.Text + " :";
                #endregion

                #region chargement infos responsable

                chargerResponsable();

                #endregion

                #region chargement commentaires

                chargerCommentaires();

                #endregion

            }
        }

        protected void RowCommand_click(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            String cle = gridView_cadeaux.DataKeys[index].Value.ToString();

            IdeesCadeaux idees = new IdeesCadeaux();
            DataTable dt = new DataTable();
            Votes votes = new Votes();
            DataTable dtVote = new DataTable();

            dtVote = votes.getTotalParticipationByIdCadeau(cle);
            dt = idees.getIdeeCadeauById(cle);

            DataTable dtLiens = new DataTable();
            Liens liens = new Liens();

            dtLiens = liens.getInfosLiensByIdeeCadeau(cle);

            #region vidage/remplissage des liens
            viderLiens();
            if (dtLiens.Rows.Count > 0)
            {
                lien1_nom.Text = dtLiens.Rows[0].ItemArray.GetValue(0).ToString();
                Lien1.HRef = dtLiens.Rows[0].ItemArray.GetValue(0).ToString();
                if (dtLiens.Rows.Count > 1)
                {
                    Lien2.HRef = dtLiens.Rows[1].ItemArray.GetValue(0).ToString();
                    lien2_nom.Text = dtLiens.Rows[1].ItemArray.GetValue(0).ToString();
                    if (dtLiens.Rows.Count > 2)
                    {
                        Lien3.HRef = dtLiens.Rows[2].ItemArray.GetValue(0).ToString();
                        lien3_nom.Text = dtLiens.Rows[2].ItemArray.GetValue(0).ToString();
                        if (dtLiens.Rows.Count > 3)
                        {
                            Lien4.HRef = dtLiens.Rows[3].ItemArray.GetValue(0).ToString();
                            lien4_nom.Text = dtLiens.Rows[3].ItemArray.GetValue(0).ToString();
                            if (dtLiens.Rows.Count > 4)
                            {
                                Lien5.HRef = dtLiens.Rows[4].ItemArray.GetValue(0).ToString();
                                lien5_nom.Text = dtLiens.Rows[4].ItemArray.GetValue(0).ToString();
                            }
                        }
                    }
                }
            }
            #endregion

            ViewState["cleIdeeCadeauVote"] = cle;

            updatePanel_participationCadeau.Visible = true;
            lbl_validation.Text = "";

            titreIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(1).ToString();
            descriptionIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(2).ToString();
            ordreDePrixIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(3).ToString();

            if (dtVote.Rows[0].ItemArray.GetValue(0).ToString() != null || dtVote.Rows[0].ItemArray.GetValue(0).ToString() != "")
                participationActuelle.Text = dtVote.Rows[0].ItemArray.GetValue(0).ToString();
            else
                participationActuelle.Text = "0";

            DataTable dtVoteModif = new DataTable();

            if ((dtVoteModif = votes.getInfosParticipationCadeauByPersonne(cle, Session["personneID"].ToString())).Rows.Count >= 1)
            {

                Tb_participation.Text = dtVoteModif.Rows[0].ItemArray.GetValue(0).ToString();
                radioButtonList_vote.SelectedValue = dtVoteModif.Rows[0].ItemArray.GetValue(1).ToString();

                button_sauvegardeParticipation.Visible = false;
                button_modificationParticipation.Visible = true;
                button_sauvegardeParticipation.ValidationGroup = "";
                button_modificationParticipation.ValidationGroup = "participation";

                ViewState["ModificationVote"] = "true";
            }
            else
            {
                button_sauvegardeParticipation.Visible = true;
                button_modificationParticipation.Visible = false;
                button_sauvegardeParticipation.ValidationGroup = "participation";
                button_modificationParticipation.ValidationGroup = "";

                ViewState["ModificationVote"] = "";
            }

        }

        protected void RowCommandSuggestions_click(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            String cle = gridView_Suggestions.DataKeys[index].Value.ToString();

            IdeesCadeauxProposes idees = new IdeesCadeauxProposes();
            DataTable dt = new DataTable();
            Votes votes = new Votes();
            DataTable dtVote = new DataTable();

            dtVote = votes.getTotalParticipationByIdCadeau(cle);
            dt = idees.getIdeeCadeauProposeById(cle);

            ViewState["cleIdeeCadeauVote"] = cle;

            DataTable dtLiens = new DataTable();
            Liens liens = new Liens();

            dtLiens = liens.getInfosLiensByIdeeCadeau(cle);

            #region vidage/remplissage des liens
            viderLiens();
            if (dtLiens.Rows.Count > 0)
            {
                lien1_nom.Text = dtLiens.Rows[0].ItemArray.GetValue(0).ToString();
                Lien1.HRef = dtLiens.Rows[0].ItemArray.GetValue(0).ToString();
                if (dtLiens.Rows.Count > 1)
                {
                    Lien2.HRef = dtLiens.Rows[1].ItemArray.GetValue(0).ToString();
                    lien2_nom.Text = dtLiens.Rows[1].ItemArray.GetValue(0).ToString();
                    if (dtLiens.Rows.Count > 2)
                    {
                        Lien3.HRef = dtLiens.Rows[2].ItemArray.GetValue(0).ToString();
                        lien3_nom.Text = dtLiens.Rows[2].ItemArray.GetValue(0).ToString();
                        if (dtLiens.Rows.Count > 3)
                        {
                            Lien4.HRef = dtLiens.Rows[3].ItemArray.GetValue(0).ToString();
                            lien4_nom.Text = dtLiens.Rows[3].ItemArray.GetValue(0).ToString();
                            if (dtLiens.Rows.Count > 4)
                            {
                                Lien5.HRef = dtLiens.Rows[4].ItemArray.GetValue(0).ToString();
                                lien5_nom.Text = dtLiens.Rows[4].ItemArray.GetValue(0).ToString();
                            }
                        }
                    }
                }
            }
            #endregion

            updatePanel_participationCadeau.Visible = true;
            lbl_validation.Text = "";

            titreIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(1).ToString();
            descriptionIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(2).ToString();
            ordreDePrixIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(3).ToString();

            if (dtVote.Rows[0].ItemArray.GetValue(0).ToString() != null || dtVote.Rows[0].ItemArray.GetValue(0).ToString() != "")
                participationActuelle.Text = dtVote.Rows[0].ItemArray.GetValue(0).ToString();
            else
                participationActuelle.Text = "0";

            DataTable dtVoteModif = new DataTable();

            if ((dtVoteModif = votes.getInfosParticipationCadeauByPersonne(cle, Session["personneID"].ToString())).Rows.Count >= 1)
            {

                Tb_participation.Text = dtVoteModif.Rows[0].ItemArray.GetValue(0).ToString();
                radioButtonList_vote.SelectedValue = dtVoteModif.Rows[0].ItemArray.GetValue(1).ToString();

                button_sauvegardeParticipation.Visible = false;
                button_modificationParticipation.Visible = true;
                button_sauvegardeParticipation.ValidationGroup = "";
                button_modificationParticipation.ValidationGroup = "participation";

                ViewState["ModificationVote"] = "true";
            }
            else
            {
                button_sauvegardeParticipation.Visible = true;
                button_modificationParticipation.Visible = false;
                button_sauvegardeParticipation.ValidationGroup = "participation";
                button_modificationParticipation.ValidationGroup = "";

                ViewState["ModificationVote"] = "";
            }

        }

        protected void btn_sauverParticipation_Click(object sender, EventArgs e)
        {

            if (ViewState["ModificationVote"] == null || ViewState["ModificationVote"].ToString() != "true")
            {
                Votes votes = new Votes();

                String vote = radioButtonList_vote.SelectedValue;

                String cleIdeeCadeauVote = ViewState["cleIdeeCadeauVote"].ToString();
                String id_personne = Session["personneID"].ToString();

                votes.ajouterVote(cleIdeeCadeauVote, id_personne,
                    vote);

                lbl_validation.Text = "La participation a bien été enregistrée.";

                updatePanel_participationCadeau.Visible = false;

                RechargerListeIdeesCadeaux();
                RechargerListeIdeesCadeauxSuggeres();
            }

        }

        protected void btn_modificationParticipation_Click(object sender, EventArgs e)
        {

            if (ViewState["ModificationVote"] != null && ViewState["ModificationVote"].ToString() == "true")
            {
                Votes votes = new Votes();

                String participation = Tb_participation.Text;
                String vote = radioButtonList_vote.SelectedValue;

                String cleIdeeCadeauVote = ViewState["cleIdeeCadeauVote"].ToString();
                String id_personne = Session["personneID"].ToString();

                votes.modifierVote(cleIdeeCadeauVote, id_personne, vote);

                lbl_validation.Text = "Vous avez bien modifié la participation.";

                updatePanel_participationCadeau.Visible = false;

                RechargerListeIdeesCadeaux();
                RechargerListeIdeesCadeauxSuggeres();
            }

        }

        protected void RechargerListeIdeesCadeaux()
        {
            IdeesCadeaux idee = new IdeesCadeaux();

            DataTable idees = new DataTable();

            idees = idee.getIdeesCadeauxByPersonneAndPersonneConnectee(listePersonnes.SelectedItem.Value, Session["personneID"].ToString());

            gridView_cadeaux.DataSource = idees;
            gridView_cadeaux.DataBind();
        }

        protected void RechargerListeIdeesCadeauxSuggeres()
        {
            IdeesCadeauxProposes idee = new IdeesCadeauxProposes();

            DataTable idees = new DataTable();

            idees = idee.getIdeesCadeauxProposesByPersonneAndPersonneConnectee(listePersonnes.SelectedItem.Value, Session["personneID"].ToString());

            gridView_Suggestions.DataSource = idees;
            gridView_Suggestions.DataBind();
        }

        protected void chargerCommentaires()
        {
            Commentaires comm = new Commentaires();

            DataTable nbComm = new DataTable();

            nbComm = comm.getNbCommentairesByListe(int.Parse(listePersonnes.SelectedItem.Value));

            lbl_nbCommentaires.Text = nbComm.Rows[0].ItemArray.GetValue(0).ToString() + " commentaires : ";

            DataTable comments = new DataTable();

            comments = comm.getCommentairesByPersonneListe(int.Parse(listePersonnes.SelectedItem.Value));

            gridView_Commentaires.DataSource = comments;
            gridView_Commentaires.DataBind();
        }

        protected void chargerResponsable()
        {
            ResponsablesBLL respBLL = new ResponsablesBLL();

            Evenement evt = new Evenement();
            evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

            Personne personneSelectionnee = new Personne();
            personneSelectionnee.id_personne = int.Parse(listePersonnes.SelectedItem.Value);

            Personne responsable = respBLL.getInfosResponsableDe(personneSelectionnee, evt);

            if (responsable != null)
                lbl_responsable.Text = "Le responsable du cadeau est : " + responsable.prenom + " " + responsable.nom;
            else
                lbl_responsable.Text = listePersonnes.SelectedItem.Text + " n'a pas encore de responsable.";
        }

        protected void btn_ajoutComment_Click(object sender, EventArgs e)
        {
            if (tb_commment.Text != "")
            {
                Commentaires comm = new Commentaires();
                if (Session["personneID"] != null)
                    //comm.ajouterCommentaire(Session["personneID"].ToString(), listePersonnes.SelectedItem.Value, tb_commment.Text);

                tb_commment.Text = "";

                lbl_validation.Text = "Le commentaire a bien été ajouté.";
                
                chargerCommentaires();
            }
        }

        protected void blabla_image_click(object sender, EventArgs e)
        {
            lbl_validation.Text = "";

            updatePanelListePropositions.Visible = false;
            updatePanel_participationCadeau.Visible = false;

            updatePanelCommentaires.Visible = true;
        }

        protected void btn_retourListe_Click(object sender, EventArgs e)
        {
            lbl_validation.Text = "";

            updatePanelListePropositions.Visible = true;
            updatePanelCommentaires.Visible = false;
        }

        private void viderLiens()
        {
            Lien1.HRef = "";
            lien1_nom.Text = "";
            Lien2.HRef = "";
            lien2_nom.Text = "";
            Lien3.HRef = "";
            lien3_nom.Text = "";
            Lien4.HRef = "";
            lien4_nom.Text = "";
            Lien5.HRef = "";
            lien5_nom.Text = "";
        }
       
    }
}