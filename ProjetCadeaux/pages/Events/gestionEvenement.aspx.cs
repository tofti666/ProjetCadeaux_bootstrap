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
    public partial class gestionEvenement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["evenementId"] = Request.Params["evenementId"].ToString();

                    RechargerGridViewParticipants();
                }
            }
            else
            {
                Response.Redirect("~/");
            }

        }

        /// <summary>
        /// Récupération de la liste des participants d'après l'évènement
        /// </summary>
        /// <param name="evt"></param>
        private void recupererListeParticipants(ProjetCadeaux_Entites.Evenement evt)
        {

            ParticipantBLL ptcpBLL = new ParticipantBLL();

            List<Participant> listeParticipants = ptcpBLL.getAllParticipantByEvenement(evt);

            gridViewParticipants.DataSource = listeParticipants;

            gridViewParticipants.DataBind();
        }

        /// <summary>
        /// Action effectuée lorsque l'on clique sur le bouton enregistrer les modifications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModifierParticipant_Click(object sender, EventArgs e)
        {
            List<Participant> listeParticipantsAModifier = new List<Participant>();

            List<Participant> listeParticipants = new List<Participant>();
            ParticipantBLL partService = new ParticipantBLL();
            Participant partTemp = new Participant();

            foreach (GridViewRow row in gridViewParticipants.Rows)
            {
                String id = gridViewParticipants.DataKeys[row.RowIndex]["id_participant"].ToString();

                Participant part = new Participant();
                part.id_participant = int.Parse(id);
                part.id_evenement = int.Parse(ViewState["evenementId"].ToString());
                part.hasListe = ((CheckBox)row.FindControl("cbHasListe")).Checked;

                //On récupère l'id de la personne
                partTemp = partService.getAllInfosByParticipant(part);
                if (partTemp != null)
                    part.id_personne = partTemp.id_personne;

                listeParticipants.Add(part);

            }

            //Ne garder que les participants modifiés
            int cle = int.Parse(ViewState["evenementId"].ToString());

            EvenementBLL evtBLL = new EvenementBLL();
            ParticipantBLL ptcpBLL = new ParticipantBLL();
            
            Evenement evt = evtBLL.getEvenementById(cle);

            List<Participant> listeParticipantsBefore = ptcpBLL.getAllParticipantByEvenement(evt);

            if (listeParticipants.Count == listeParticipantsBefore.Count)
            {
                for (int i = 0; i < listeParticipants.Count; i++)
                {
                    if (listeParticipants[i].id_participant == listeParticipantsBefore[i].id_participant
                        && listeParticipants[i].hasListe != listeParticipantsBefore[i].hasListe)
                    {
                        listeParticipantsAModifier.Add(listeParticipants[i]);
                    }
                }
            }
            //Modifier les participants

            if (listeParticipantsAModifier.Count > 0)
            {
                ParticipantBLL partBLL = new ParticipantBLL();
                ListeIdeesCadeauxBLL listeService = new ListeIdeesCadeauxBLL();

                Boolean retour = partBLL.modifierHasListesListeParticipants(listeParticipantsAModifier) && listeService.updateActiveListe(listeParticipantsAModifier, evt);

                if (retour)
                    SuccessText.Text = "Les modifications ont bien été prises en compte";
                else
                    FailureText.Text = "Les modifications n'ont pas pu être prises en compte";

                RechargerGridViewParticipants();
            }

        }

        /// <summary>
        /// Action effectuée lorsque l'on clique sur le bouton ajout des participants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAjouterParticipant_Click(object sender, EventArgs e)
        {
            //L'utilisateur a confirmé l'ajout des participants sélectionnés
            if ("Y".Equals(hiddenFieldAjouterParticipant.Value))
            {

                List<Participant> listeParticipantsAAjouter = new List<Participant>();
                ProjetCadeaux_Entites.Evenement evt = new ProjetCadeaux_Entites.Evenement();
                evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());


                foreach (GridViewRow row in gridViewPersonnesRecherche.Rows)
                {
                    CheckBox cbChoixPersonne = ((CheckBox)row.FindControl("cbChoixPersonne"));

                    if (cbChoixPersonne.Checked)
                    {
                        CheckBox cbHasListe = ((CheckBox)row.FindControl("cbChoixListe"));

                        String id = gridViewPersonnesRecherche.DataKeys[row.RowIndex]["id_personne"].ToString();
                        String nomSelect = row.Cells[3].Text;
                        String prenomSelect = row.Cells[4].Text;

                        Participant part = new Participant();
                        part.hasListe = cbHasListe.Checked;
                        part.nom_participant = nomSelect + " " + prenomSelect;
                        part.id_personne = int.Parse(id);
                        part.id_evenement = int.Parse(ViewState["evenementId"].ToString());

                        listeParticipantsAAjouter.Add(part);
                        
                    }
                }

                gridViewPersonnesRecherche.DataSource = null;
                gridViewPersonnesRecherche.DataBind();

                ParticipantBLL partBLL = new ParticipantBLL();
                ListeIdeesCadeauxBLL listeService = new ListeIdeesCadeauxBLL();

                Boolean retour = partBLL.ajouterListeParticipant(listeParticipantsAAjouter) && listeService.creerListeIdeesCadeaux(listeParticipantsAAjouter,evt);

                if (!retour)
                    FailureText.Text = "Tous les participants n'ont pas pû être ajoutés";
                else
                    SuccessText.Text = "Les participants ont été ajoutés";

                btnAjouterParticipant.Visible = false;

                RechargerGridViewParticipants();

            }
        }

        /// <summary>
        /// Rechargement de la grille des participants selon l'évènement
        /// </summary>
        private void RechargerGridViewParticipants()
        {
            int cle = int.Parse(ViewState["evenementId"].ToString());

            EvenementBLL evtBLL = new EvenementBLL();

            ProjetCadeaux_Entites.Evenement evt = evtBLL.getEvenementById(cle);

            if (evt != null)
            {
                TitreEvenementLabel.Text = evt.libelle;
                TitreEvenementLabelh3.Text = evt.libelle;

                if (evt.id_admin == int.Parse(Session["personneID"].ToString()))
                {
                    recupererListeParticipants(evt);
                }
                else
                {
                    FailureText.Text = "Vous n'avez pas les droits pour consulter les informations de cet évènement.";
                }
            }
            else
            {
                TitreEvenementLabel.Text = "";
                TitreEvenementLabelh3.Text = "";

                FailureText.Text = "Cet évènement n'existe pas.";
            }
        }

        /// <summary>
        /// Action de suppression des participants sélectionnés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSupprimerParticipant_Click(object sender, EventArgs e)
        {
            //L'utilisateur a confirmé la suppression des participants
            if ("Y".Equals(supprimerParticipants.Value))
            {

                Boolean connecteEstParticipantASupprimer = false;
                List<Participant> listeParticipantsASupprimer = new List<Participant>();
                ParticipantBLL partService = new ParticipantBLL();
                Participant partTemp = new Participant();

                foreach (GridViewRow row in gridViewParticipants.Rows)
                {
                    CheckBox cbChoixParticipant = ((CheckBox)row.FindControl("cbChoix"));

                    if (cbChoixParticipant.Checked)
                    {

                        String id = gridViewParticipants.DataKeys[row.RowIndex]["id_participant"].ToString();
                        if(id.Equals(Session["personneID"].ToString())){
                            connecteEstParticipantASupprimer = true;
                        }

                        Participant part = new Participant();
                        part.id_participant = int.Parse(id);
                        part.id_evenement = int.Parse(ViewState["evenementId"].ToString());

                        partTemp = partService.getAllInfosByParticipant(part);
                        if (partTemp != null)
                            part.id_personne = partTemp.id_personne;

                        listeParticipantsASupprimer.Add(part);

                    }
                }
                if(!connecteEstParticipantASupprimer){
                    ListeIdeesCadeauxBLL listeService = new ListeIdeesCadeauxBLL();

                    ProjetCadeaux_Entites.Evenement evt = new ProjetCadeaux_Entites.Evenement();
                    evt.id_evenement = int.Parse(ViewState["evenementId"].ToString());

                    Boolean retour = partService.supprimerListeParticipants(listeParticipantsASupprimer) && listeService.desactiverListe(listeParticipantsASupprimer, evt);

                    if (retour)
                        SuccessText.Text = "Tous les participants sélectionnés ont pu être supprimés";
                    else
                        FailureText.Text = "Les participants n'ont pas pu être supprimés";

                    RechargerGridViewParticipants();
                }
                else
                {
                    FailureText.Text = "Vous ne pouvez supprimer l'administrateur de l'évènement";
                }
            }                
        }

        /// <summary>
        /// Action effectuée au moment du clic sur la recherche des personnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRecherchePersonne_Click(object sender, EventArgs e)
        {
            string nom = rechercheNomParticipantTb.Text;
            string prenom = recherchePrenomParticipantTb.Text;

            int cle = int.Parse(Request.Params["evenementId"].ToString());
            PersonnesBLL personnesService = new PersonnesBLL();
            EvenementBLL evtBLL = new EvenementBLL();
            ParticipantBLL ptcpBLL = new ParticipantBLL();

            //Récupération des personnes que l'on a recherché
            List<Personne> listePersonnesRecherches = personnesService.recherchePersonne(nom, prenom);

            //Recherche des personnes qui font déjà partie de l'évènement
            Evenement evt = evtBLL.getEvenementById(cle);

            List<Participant> listeParticipants = ptcpBLL.getAllParticipantByEvenement(evt);

            //On retire les personnes participant déjà
            if(listeParticipants.Count > 0)
            {
                for(int i=0; i<listePersonnesRecherches.Count; i++){
                    Personne pers = listePersonnesRecherches[i];

                    for(int j=0; j<listeParticipants.Count; j++)
                    {
                        Participant part = listeParticipants[j];

                        if(part.id_personne == pers.id_personne)
                        {
                            listePersonnesRecherches.Remove(pers);
                            i--;
                        }
                    }
                }
            }

            if(listePersonnesRecherches.Count > 0){
                btnAjouterParticipant.Visible = true;

                gridViewPersonnesRecherche.DataSource = listePersonnesRecherches;

                gridViewPersonnesRecherche.DataBind();
            }
            else{
                FailureText.Text = "Aucune personne ne correspond aux critères.";
            }


        }

    }
}