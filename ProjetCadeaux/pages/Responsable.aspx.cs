using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProjetCadeaux_Connection;
using ProjetCadeauxBLL;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux.pages
{
    public partial class Responsable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null)
            {
                if (!IsPostBack)
                {
                    chargerListeEvenement();
                }
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        protected void ddl_listeEvenements_Change(object sender, EventArgs e)
        {
            ViewState["evenementId"] = ddl_listeEvenements.SelectedValue;
            ViewState["listePersonnesChargee"] = "false";

            updatePanel_infosReponsabilite.Visible = true;

            chargerPersonnesDontOnEstResponsable();

            ChargerPersonnesSansRespo();

            chargerCadeauxPersonne(null);
            updatePanel_detailCadeau.Visible = false;
            updatePanel_totalParticipation.Visible = false;
        }

        protected void gridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "responsable")
            {
                ResponsablesBLL respoBLL = new ResponsablesBLL();

                Evenement evt = new Evenement();
                evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

                Personne responsable = new Personne();
                responsable.id_personne = int.Parse(Session["personneID"].ToString());

                //On récupère l'id de la personne sélectionnée
                int indexClique = Convert.ToInt32(e.CommandArgument);
                String idDeLaLigneCliquee = gridview_personnesSansResponsables.DataKeys[indexClique].Value.ToString();

                Personne responsableDe = new Personne();
                responsableDe.id_personne = int.Parse(idDeLaLigneCliquee);

                ResponsablesDAL respo = new ResponsablesDAL();

                bool success = respoBLL.devenirResponsable(responsable, responsableDe, evt);

                if (success)
                {
                    ViewState["listePersonnesChargee"] = "false";

                    ChargerPersonnesSansRespo();
                }
                else
                {
                    FailureText.Text = "Echec de la prise de responsabilité";
                }

            }
        }

        private void ChargerPersonnesSansRespo()
        {
            ResponsablesBLL respoBLL = new ResponsablesBLL();

            Personne personneConnectee = new Personne();
            personneConnectee.id_personne = int.Parse(Session["personneID"].ToString());

            Evenement evt = new Evenement();
            evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

            gridview_personnesSansResponsables.DataSource = respoBLL.getListePersonnesSansResponsables(personneConnectee,evt);

            gridview_personnesSansResponsables.DataBind();

            chargerPersonnesDontOnEstResponsable();
        }

        private void chargerCadeauxPersonne(Personne personneSelection)
        {
            if (Session["connecte"] != null)
            {
                if (personneSelection != null)
                {
                    IdeesCadeaux ideeCadeauDAL = new IdeesCadeaux();
                    Evenement evt = new Evenement();
                    evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

                    DataTable idees = new DataTable();
                    DataTable ideesSuggerees = new DataTable();

                    idees = ideeCadeauDAL.getIdeesCadeauxByPersonneEvenementAndPersonneConnectee(personneSelection.id_personne, evt.id_evenement, int.Parse(Session["personneID"].ToString()));

                    IdeesCadeauxProposes ideeCadeauxProposesDAL = new IdeesCadeauxProposes();

                    ideesSuggerees = ideeCadeauxProposesDAL.getIdeesCadeauxProposesByPersonneEvenementAndPersonneConnectee(personneSelection.id_personne.ToString(), evt.id_evenement, Session["personneID"].ToString());

                    if (idees != null && ideesSuggerees != null)
                        idees.Merge(ideesSuggerees);

                    gridView_cadeauResponsable.DataSource = idees;
                    gridView_cadeauResponsable.DataBind();

                }
                else
                {
                    gridView_cadeauResponsable.DataSource = null;
                    gridView_cadeauResponsable.DataBind();

                }
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        protected void listePersonnes_onIndexChanged(object sender, EventArgs e)
        {
            Personne personneSelection = new Personne();
            personneSelection.id_personne = int.Parse(listePersonnes.SelectedItem.Value);

            chargerCadeauxPersonne(personneSelection);

            if (listePersonnes.SelectedItem.Value != "0")
            {
                Evenement evt = new Evenement();
                evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

                ListeIdeesCadeauxBLL listeBLL = new ListeIdeesCadeauxBLL();
                ListeIdeesCadeaux liste = listeBLL.getListeIdeesCadeaux(personneSelection, evt);

                ParticipationsBLL partBLL = new ParticipationsBLL();
                long totalParticipation = partBLL.getTotalParticipation(liste);

                lbl_participationTotale.Text = totalParticipation + " €";

                chargerDetailsParticipation(liste);

                updatePanel_totalParticipation.Visible = true;
            }
            else
                updatePanel_totalParticipation.Visible = false;

            updatePanel_detailCadeau.Visible = false;
        }

        protected void gridView_cadeauResponsable_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            updatePanel_detailCadeau.Visible = true;

            int index = Convert.ToInt32(e.CommandArgument);

            IdeesCadeaux idee = new IdeesCadeaux();
            Liens lien = new Liens();
            Votes vote = new Votes();

            String cle = gridView_cadeauResponsable.DataKeys[index].Value.ToString();

            DataTable dtIdeeCadeau = new DataTable();
            dtIdeeCadeau = idee.getIdeeCadeauById(cle);

            DataTable dtLiens = new DataTable();
            dtLiens = lien.getInfosLiensByIdeeCadeau(cle);

            string nbVotesOui = vote.getNombreOuiByIdCadeau(cle);
            string nbVotesPourquoiPas = vote.getNombrePourquoiPasByIdCadeau(cle);
            string nbVotesNon = vote.getNombreNonByIdCadeau(cle);

            titreIdeeCadeau.Text = dtIdeeCadeau.Rows[0].ItemArray.GetValue(1).ToString();
            descriptionIdeeCadeau.Text = dtIdeeCadeau.Rows[0].ItemArray.GetValue(2).ToString();
            ordreDePrixIdeeCadeau.Text = dtIdeeCadeau.Rows[0].ItemArray.GetValue(3).ToString();
            prioriteCadeau.Text = dtIdeeCadeau.Rows[0].ItemArray.GetValue(0).ToString();

            lbl_votesOui.Text = nbVotesOui;

            lbl_votesPourquoiPas.Text = nbVotesPourquoiPas;

            lbl_votesNon.Text = nbVotesNon;

            #region vidage/remplissage des liens
            viderLiens();
            if (dtLiens.Rows.Count > 0)
            {
                Lien1.HRef = dtLiens.Rows[0].ItemArray.GetValue(0).ToString();
                lien1_nom.Text = dtLiens.Rows[0].ItemArray.GetValue(0).ToString();
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

        }

        private void chargerDetailsParticipation(ListeIdeesCadeaux liste)
        {
            ParticipationsBLL partService = new ParticipationsBLL();

            gridView_detailsParticipation.DataSource = partService.getListeParticipation(liste);
            gridView_detailsParticipation.DataBind();
        }

        private void chargerPersonnesDontOnEstResponsable()
        {
            if ((ViewState["listePersonnesChargee"] == null || ViewState["listePersonnesChargee"].ToString() != "true") && ViewState["evenementId"] != null)
            {
                ResponsablesBLL respoBLL = new ResponsablesBLL();

                Personne personneConnectee = new Personne();
                personneConnectee.id_personne = int.Parse(Session["personneID"].ToString());

                Evenement evt = new Evenement();
                evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

                List<Personne> listePersonnesRetour = respoBLL.getListeInfosResponsabilite(personneConnectee,evt);

                DataTable retour = new DataTable();

                retour.Columns.Add("ID");
                retour.Columns.Add("nom");

                DataRow dr2 = retour.NewRow();
                dr2["ID"] = "0";
                dr2["nom"] = "--";
                retour.Rows.Add(dr2);

                //List<String> truc = new List<String>();
                foreach (Personne pers in listePersonnesRetour)
                {
                    DataRow dr = retour.NewRow();
                    dr["ID"] = pers.id_personne;
                    dr["nom"] = pers.prenom + " " + pers.nom.ToUpper();

                    retour.Rows.Add(dr);
                }

                listePersonnes.DataValueField = "ID";
                listePersonnes.DataTextField = "nom";

                listePersonnes.DataSource = retour;
                listePersonnes.DataBind();
                ViewState["listePersonnesChargee"] = "true";
            }  
        }

        //protected void btn_voirDetails_click(object sender, EventArgs e)
        //{
        //    if (!updatePanel_detailsParticipation.Visible)
        //        updatePanel_detailsParticipation.Visible = true;
        //    else
        //        updatePanel_detailsParticipation.Visible = false;
        //}

        private void viderLiens()
        {
            lien1_nom.Text = "";
            lien2_nom.Text = "";
            lien3_nom.Text = "";
            lien4_nom.Text = "";
            lien5_nom.Text = "";
            Lien1.HRef = "";
            Lien2.HRef = "";
            Lien3.HRef = "";
            Lien4.HRef = "";
            Lien5.HRef = "";
        }

        private void chargerListeEvenement()
        {
            if (Session["personneId"] != null)
            {
                EvenementBLL evtBLL = new EvenementBLL();

                List<Evenement> listeEvt = evtBLL.getAllEvenementByIdPersonne(Session["personneId"].ToString());

                DataTable retour = new DataTable();

                retour.Columns.Add("ID");
                retour.Columns.Add("nom");

                DataRow dr2 = retour.NewRow();
                dr2["ID"] = "0";
                dr2["nom"] = "--";
                retour.Rows.Add(dr2);

                foreach (Evenement evt in listeEvt)
                {
                    String id = evt.id_evenement.ToString();
                    String libelle = evt.libelle;

                    DataRow dr = retour.NewRow();
                    dr["ID"] = id;
                    dr["nom"] = libelle;

                    retour.Rows.Add(dr);
                }

                ddl_listeEvenements.DataValueField = "ID";
                ddl_listeEvenements.DataTextField = "nom";

                ddl_listeEvenements.DataSource = retour;

                ddl_listeEvenements.DataBind();

            }
        }
    }
}