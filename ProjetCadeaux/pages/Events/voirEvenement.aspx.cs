using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeauxBLL;
using ProjetCadeaux_Entites;
using System.Data;
using ProjetCadeaux_Connection;

namespace ProjetCadeaux.pages.Events
{
    public partial class voirEvenement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null && Request.Params["evenementId"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["evenementId"] = Request.Params["evenementId"].ToString();

                    RechargerGridViewParticipants();
                }

                if (ViewState["listePersonnesChargee"] == null || ViewState["listePersonnesChargee"].ToString() != "true")
                {
                    ParticipantBLL participantBLL = new ParticipantBLL();

                    List<Participant> listeParticipantAyantListeCadeau = new List<Participant>();
                    ProjetCadeaux_Entites.Evenement evt = new ProjetCadeaux_Entites.Evenement();
                    Participant connecte = new Participant();
                    Personne persConnecte = new Personne();

                    evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());
                    connecte.id_personne = int.Parse(Session["personneID"].ToString());
                    persConnecte.id_personne = connecte.id_personne;

                    //Afficher le lien administration si admin
                    EvenementBLL evtService = new EvenementBLL();
                    evt = evtService.getEvenementById(evt.id_evenement);

                    if (estAdmin(persConnecte, evt))
                    {
                        linkAdministrerEvenement.Visible = true;
                        linkAdministrerEvenement.NavigateUrl += evt.id_evenement;
                    }
                    else
                    {
                        linkAdministrerEvenement.Visible = false;
                    }

                    listeParticipantAyantListeCadeau = participantBLL.getAllParticipantSaufConnecteAyantListeByEvenement(evt, connecte);

                    DataTable retour = new DataTable();

                    retour.Columns.Add("ID");
                    retour.Columns.Add("nom");

                    DataRow dr2 = retour.NewRow();
                    dr2["ID"] = "";
                    dr2["nom"] = "--";
                    retour.Rows.Add(dr2);

                    foreach (Participant part in listeParticipantAyantListeCadeau)
                    {

                        String id = part.id_personne.ToString();
                        String nom = part.nom_participant;

                        DataRow dr = retour.NewRow();
                        dr["ID"] = id;
                        dr["nom"] = nom;

                        retour.Rows.Add(dr);
                    }

                    listeParticipantAyantListe.DataValueField = "ID";
                    listeParticipantAyantListe.DataTextField = "nom";

                    listeParticipantAyantListe.DataSource = retour;
                    listeParticipantAyantListe.DataBind();
                    ViewState["listePersonnesChargee"] = "true";

                }



            }
            else
            {
                Response.Redirect("~/");
            }
        }

        private Boolean estAdmin(Personne pers, ProjetCadeaux_Entites.Evenement evt)
        {
            return pers.id_personne == evt.id_admin;
        }

        private void RechargerGridViewParticipants()
        {
            int cle = int.Parse(ViewState["evenementId"].ToString());

            EvenementBLL evtBLL = new EvenementBLL();

            ProjetCadeaux_Entites.Evenement evt = evtBLL.getEvenementById(cle);

            if (evt != null)
            {
                TitreEvenementLabel.Text = evt.libelle;
                TitreEvenementLabelh3.Text = evt.libelle;
                dateEvenement.Text = evt.dateEvenement.ToShortDateString();
                dateButoir.Text = evt.dateButoir.ToShortDateString();

                recupererListeParticipants(evt);
            }
            else
            {
                TitreEvenementLabel.Text = "";
                TitreEvenementLabelh3.Text = "";
                dateEvenement.Text = "";
                dateButoir.Text = "";


                FailureText.Text = "Cet évènement n'existe pas.";
            }
        }

        private void recupererListeParticipants(Evenement evt)
        {

            ParticipantBLL ptcpBLL = new ParticipantBLL();

            List<Participant> listeParticipants = ptcpBLL.getAllParticipantByEvenement(evt);

            gridViewParticipants.DataSource = listeParticipants;

            gridViewParticipants.DataBind();
        }

        protected void listePersonnes_onIndexChanged(object sender, EventArgs e)
        {

            if (listeParticipantAyantListe.SelectedItem.Value != "")
            {
                #region chargement liste souhaits
                updatePanelListePropositions.Visible = true;
                updatePanel_participationCadeau.Visible = false;
                updatePanelCommentaires.Visible = false;

                RechargerListeIdeesCadeaux();

                lbl_nomSelectionne.Text = " de " + listeParticipantAyantListe.SelectedItem.Text + " :";
                #endregion

                #region chargement liste suggestions

                RechargerListeIdeesCadeauxSuggeres();

                lbl_nomSelectionneSuggestion.Text = " pour " + listeParticipantAyantListe.SelectedItem.Text + " :";
                #endregion

                #region chargement infos responsable
                chargerResponsable();
                #endregion

                #region chargement commentaires
                chargerCommentaires();
                #endregion

                #region chargement participation
                RechargerParticipation();
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
            SuccessText.Text = "";

            titreIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(1).ToString();
            descriptionIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(2).ToString();
            ordreDePrixIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(3).ToString();

            DataTable dtVoteModif = new DataTable();

            if ((dtVoteModif = votes.getInfosParticipationCadeauByPersonne(cle, Session["personneID"].ToString())).Rows.Count >= 1)
            {

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
            SuccessText.Text = "";

            titreIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(1).ToString();
            descriptionIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(2).ToString();
            ordreDePrixIdeeCadeau.Text = dt.Rows[0].ItemArray.GetValue(3).ToString();


            DataTable dtVoteModif = new DataTable();

            if ((dtVoteModif = votes.getInfosParticipationCadeauByPersonne(cle, Session["personneID"].ToString())).Rows.Count >= 1)
            {

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

                SuccessText.Text = "La participation a bien été enregistrée.";

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

                String vote = radioButtonList_vote.SelectedValue;

                String cleIdeeCadeauVote = ViewState["cleIdeeCadeauVote"].ToString();
                String id_personne = Session["personneID"].ToString();

                votes.modifierVote(cleIdeeCadeauVote, id_personne, vote);

                SuccessText.Text = "Vous avez bien modifié la participation.";

                updatePanel_participationCadeau.Visible = false;

                RechargerListeIdeesCadeaux();
                RechargerListeIdeesCadeauxSuggeres();
            }

        }

        protected void RechargerListeIdeesCadeaux()
        {
            if (Session["connecte"] != null)
            {
                IdeesCadeaux idee = new IdeesCadeaux();
                ProjetCadeaux_Entites.Evenement evt = new ProjetCadeaux_Entites.Evenement();

                evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

                DataTable idees = new DataTable();

                idees = idee.getIdeesCadeauxByPersonneEvenementAndPersonneConnectee(int.Parse(listeParticipantAyantListe.SelectedItem.Value), evt.id_evenement, int.Parse(Session["personneID"].ToString()));

                gridView_cadeaux.DataSource = idees;
                gridView_cadeaux.DataBind();
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        protected void RechargerListeIdeesCadeauxSuggeres()
        {
            IdeesCadeauxProposes idee = new IdeesCadeauxProposes();

            DataTable idees = new DataTable();
            ProjetCadeaux_Entites.Evenement evt = new ProjetCadeaux_Entites.Evenement();

            evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

            idees = idee.getIdeesCadeauxProposesByPersonneEvenementAndPersonneConnectee(listeParticipantAyantListe.SelectedItem.Value, evt.id_evenement, Session["personneID"].ToString());

            gridView_Suggestions.DataSource = idees;
            gridView_Suggestions.DataBind();
        }

        protected void chargerCommentaires()
        {
            Commentaires comm = new Commentaires();

            String id_evenement = ViewState["evenementId"].ToString();

            DataTable nbComm = new DataTable();

            nbComm = comm.getNbCommentairesByListe(int.Parse(listeParticipantAyantListe.SelectedItem.Value), int.Parse(id_evenement));

            lbl_nbCommentaires.Text = nbComm.Rows[0].ItemArray.GetValue(0).ToString() + " commentaires : ";

            DataTable comments = new DataTable();

            comments = comm.getCommentairesByPersonneListe(int.Parse(listeParticipantAyantListe.SelectedItem.Value), int.Parse(id_evenement));

            gridView_Commentaires.DataSource = comments;
            gridView_Commentaires.DataBind();
        }

        protected void chargerResponsable()
        {
            ResponsablesBLL respBLL = new ResponsablesBLL();

            Evenement evt = new Evenement();
            evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

            Personne personneSelectionnee = new Personne();
            personneSelectionnee.id_personne = int.Parse(listeParticipantAyantListe.SelectedItem.Value);

            Personne responsable = respBLL.getInfosResponsableDe(personneSelectionnee, evt);

            if (responsable != null)
                lbl_responsable.Text = "Le responsable du cadeau est : " + responsable.prenom + " " + responsable.nom.ToUpper();
            else
                lbl_responsable.Text = listeParticipantAyantListe.SelectedItem.Text + " n'a pas encore de responsable.";       
        }

        protected void btn_ajoutComment_Click(object sender, EventArgs e)
        {
            if (tb_commment.Text != "")
            {
                Commentaires comm = new Commentaires();
                if (Session["personneID"] != null && ViewState["evenementId"] != null)
                    comm.ajouterCommentaire(int.Parse(Session["personneID"].ToString()), int.Parse(ViewState["evenementId"].ToString()), int.Parse(listeParticipantAyantListe.SelectedItem.Value), tb_commment.Text);

                tb_commment.Text = "";

                SuccessText.Text = "Le commentaire a bien été ajouté.";

                chargerCommentaires();
            }
        }

        protected void blabla_image_click(object sender, EventArgs e)
        {
            SuccessText.Text = "";

            updatePanelListePropositions.Visible = false;
            updatePanel_participationCadeau.Visible = false;

            updatePanelCommentaires.Visible = true;
        }

        protected void btn_retourListe_Click(object sender, EventArgs e)
        {
            SuccessText.Text = "";

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

        protected void btnAjouterIdee_Click(object sender, EventArgs e)
        {
            btnAjoutSuggestionListe.Enabled = false;

            Boolean retour = true;
            IdeesCadeauxProposes ideeCadeau = new IdeesCadeauxProposes();

            ListeIdeesCadeauxBLL listeService = new ListeIdeesCadeauxBLL();
            Evenement evt = new Evenement();
            Personne pers = new Personne();
            IdeeCadeau idee = new IdeeCadeau();


            evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

            String titre = TitreCadeau.Text;
            String description = Description.Text;
            String prix = Prix.Text;
            String personne = listeParticipantAyantListe.SelectedItem.Value.ToString();
            String priorite = Priorite.Text;

            string ideeCadeau_id = ideeCadeau.ajouterIdeeCadeau(titre, description, prix, priorite, personne, Session["personneID"].ToString(),
                tb_lien1.Text, tb_lien2.Text, tb_lien3.Text, tb_lien4.Text, tb_lien5.Text);

            pers.id_personne = int.Parse(personne);
            idee.id_ideeCadeau = int.Parse(ideeCadeau_id);


            if (ideeCadeau_id != null)
                retour = listeService.ajouterCadeauToListe(pers, evt, idee);

            viderSuggestionForm();
            RechargerListeIdeesCadeauxSuggeres();

            btnAjoutSuggestionListe.Enabled = true;

            if (retour)
            {
                FailureText.Text = "";
                SuccessText.Text = "La suggestion a bien été ajoutée.";
            }
            else
            {
                SuccessText.Text = "";
                FailureText.Text = "Erreur lors de l'ajout de la suggestion.";
            }

        }

        private void viderSuggestionForm()
        {
            this.TitreCadeau.Text = "";
            this.Description.Text = "";
            this.Prix.Text = "";
            this.Priorite.Text = "";
            this.tb_lien1.Text = "";
            this.tb_lien2.Text = "";
            this.tb_lien3.Text = "";
            this.tb_lien4.Text = "";
            this.tb_lien5.Text = "";
        }

        protected void btnAjouterParticipation_Click(object sender, EventArgs e)
        {
            ParticipationsBLL partBLL = new ParticipationsBLL();
            ListeIdeesCadeauxBLL listeBLL = new ListeIdeesCadeauxBLL();

            ListeIdeesCadeaux liste = new ListeIdeesCadeaux();
            Personne cadeauPour = new Personne();
            Evenement evt = new Evenement();

            Personne participationDe = new Personne();
            participationDe.id_personne = int.Parse(Session["personneID"].ToString());

            cadeauPour.id_personne = int.Parse(listeParticipantAyantListe.SelectedItem.Value.ToString());
            evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

            liste = listeBLL.getListeIdeesCadeaux(cadeauPour, evt);

            Participation part = new Participation();
            part.id_liste = liste.id_listeIdeesCadeaux;
            part.id_personne = int.Parse(Session["personneID"].ToString());

            Boolean isParticipationCorrecte = true;
            try
            {
                part.participation = long.Parse(tbParticipation.Text);
            }
            catch (Exception)
            {
                isParticipationCorrecte = false;
                lbPbParticipation.Text = "Vous devez saisir un nombre dans participation. (exemple: 20)";
                SuccessText.Text = "";
            }

            //Si l'utilisateur a bien saisi une participation correcte, on peut modifier
            if (isParticipationCorrecte)
            {
                if (partBLL.getParticipation(liste,participationDe) == null)
                {
                    if (partBLL.ajouterParticipation(part))
                    {
                        tbParticipation.Text = "";

                        lbPbParticipation.Text = "";
                        SuccessText.Text = "Vous avez bien ajouté votre participation à cette liste !";

                        RechargerParticipation();
                        afficherGroupeModificationParticipation(false);
                    }
                    else
                    {
                        lbPbParticipation.Text = "Erreur dans l'enregistrement de la participation. Vous devez saisir un nombre. (exemple: 20)";
                        SuccessText.Text = "";
                    }
                }
                else
                {
                    if (partBLL.modifierParticipation(part))
                    {
                        lbPbParticipation.Text = "";
                        SuccessText.Text = "Votre participation a été modifiée.";

                        RechargerParticipation();
                        afficherGroupeModificationParticipation(false);
                    }
                    else
                    {
                        lbPbParticipation.Text = "Erreur dans l'enregistrement de la participation. Vous devez saisir un nombre. (exemple: 20)";
                        SuccessText.Text = "";
                    }
                }
            }


        }

        private void RechargerParticipation()
        {
            ParticipationsBLL partBLL = new ParticipationsBLL();
            ListeIdeesCadeauxBLL listeBLL = new ListeIdeesCadeauxBLL();

            ListeIdeesCadeaux liste = new ListeIdeesCadeaux();
            Personne cadeauPour = new Personne();
            Evenement evt = new Evenement();

            Personne participationDe = new Personne();
            participationDe.id_personne = int.Parse(Session["personneID"].ToString());

            cadeauPour.id_personne = int.Parse(listeParticipantAyantListe.SelectedItem.Value.ToString());
            evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

            liste = listeBLL.getListeIdeesCadeaux(cadeauPour, evt);

            Participation participation = partBLL.getParticipation(liste, participationDe);
            if (participation != null)
                lblValeurParticipationListe.Text = "Votre participation est de : " + participation.participation.ToString() + " €";
            else
                lblValeurParticipationListe.Text = "Votre participation est de : 0 €";
        }

        private void afficherGroupeModificationParticipation(bool afficher)
        {
            //Champs qui affichent
            lblValeurParticipationListe.Visible = !afficher;
            btnModifierParticipation.Visible = !afficher;

            //Champs qui modifient
            lblParticipationFinanciere.Visible = afficher;
            tbParticipation.Visible = afficher;
            btnAjouterParticipation.Visible = afficher;
            btnAnnulerAjoutParticipation.Visible = afficher;
        }

        protected void btnModifierParticipation_Click(object sender, EventArgs e)
        {
            afficherGroupeModificationParticipation(true);
        }

        protected void btnAnnulerAjoutParticipation_Click(object sender, EventArgs e)
        {
            tbParticipation.Text = "";
            lbPbParticipation.Text = "";
            afficherGroupeModificationParticipation(false);
        }
    }
}
