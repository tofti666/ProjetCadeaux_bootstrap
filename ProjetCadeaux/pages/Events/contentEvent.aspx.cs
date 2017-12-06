using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Entites;
using ProjetCadeauxBLL;
using ProjetCadeaux_Connection;
using System.Data;

namespace ProjetCadeaux.pages.Events
{
    public partial class contentEvent : System.Web.UI.Page
    {

        public Participant participantListe = new Participant();
        public Personne personneListe = new Personne();

        public Participant participantConnecte = new Participant();
        public Personne personneConnectee = new Personne();

        public Evenement evenement = new Evenement();

        public Participation participation = new Participation();

        public ParticipantBLL participantBLL = new ParticipantBLL();
        public ParticipationsBLL participationBLL = new ParticipationsBLL();
        public CommentairesBLL commentaireBLL = new CommentairesBLL();
        public ListeIdeesCadeauxBLL ideecadeauBLL = new ListeIdeesCadeauxBLL();

        public List<IdeeCadeauPourListe> listeCadeaux = new List<IdeeCadeauPourListe>();

        public List<Commentaire> listeCommentaires = new List<Commentaire>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null)
            {
                if (Request.Params["evenementId"] != null)
                {
                    Session["evenementId"] = Request.Params["evenementId"].ToString();
                }
                if (Request.Params["idpersonne"] != null)
                {
                    Session["idPersonneListe"] = Request.Params["idpersonne"].ToString();
                }

                personneListe.id_personne = int.Parse(Session["idPersonneListe"].ToString());

                evenement.id_evenement = int.Parse(Session["evenementId"].ToString());

                personneConnectee.id_personne = int.Parse(Session["personneID"].ToString());

                if (Request.Params["sauvercommentaire"] != null)
                {
                    enregistrerCommentaire();
                }
                if (Request.Params["sauveridee"] != null)
                {
                    enregistrerIdee();
                }
                if (Request.Params["sauverparticipation"] != null)
                {
                    enregistrerParticipation();
                }
                if (Request.Params["sauverresponsable"] != null)
                {
                    enregistrerResponsable();
                }

                participantListe = participantBLL.getParticipantAyantListeByEvenementAndPersonne(evenement, personneListe);

                //Récupération de la participation de la personne connectée
                participantConnecte = participantBLL.getParticipantAyantListeByEvenementAndPersonne(evenement, personneConnectee);

                RechargerParticipation();

                RecupererListeCommentaires();

                RecupererListeCadeaux();

                //Récupérer responsable de la liste
                RecupererResponsableListe();

                
            }
            else
            {
                Response.Redirect("/Reconnecte.aspx");
            }
            
        }

        private void enregistrerResponsable()
        {
            throw new NotImplementedException();
        }

        private void RecupererResponsableListe()
        {
            throw new NotImplementedException();
        }

        private void enregistrerParticipation()
        {
            throw new NotImplementedException();
        }

        private void enregistrerIdee()
        {
            if (Request.Form["titre"] != null )
            {
                IdeeCadeauPourListe idee = new IdeeCadeauPourListe();
                idee.cadeau = new Cadeau();

                if( Request.Form["description"] != null){
                    idee.cadeau.description = Request.Form["description"].ToString();
                }
                if( Request.Form["prix"] != null){
                    idee.cadeau.prix = Request.Form["prix"].ToString();
                }
                idee.cadeau.intitule_cadeau = Request.Form["titre"].ToString();

                idee.dateAjoutIdeeCadeau = DateTime.Now;

                idee.ideeCadeauPour = personneListe;
                idee.proposePar = personneConnectee;

                idee.


                //Enregistrer un nouveau commentaire
                ideecadeauBLL.ajouterCadeauToListe(idee);
            }
        }    

        private void enregistrerCommentaire()
        {
            if (Request.Form["idCommentaireModifie"] != null)
            {
                //Code de modification du commentaire
            }
            else
            {
                if (Request.Form["commentaire"] != null)
                {
                    String commentaire = Request.Form["commentaire"].ToString();
                    if (commentaire != "")
                    {
                        Commentaire comm = new Commentaire();

                        comm.commentaire = commentaire;

                        //Enregistrer un nouveau commentaire
                        commentaireBLL.enregistrerCommentaire(evenement, personneListe, personneConnectee, comm);
                    }
                }
            }
                
        }

        private void RechargerParticipation()
        {
            ListeIdeesCadeauxBLL listeBLL = new ListeIdeesCadeauxBLL();

            ListeIdeesCadeaux liste = new ListeIdeesCadeaux();
            Personne cadeauPour = new Personne();

            liste = listeBLL.getListeIdeesCadeaux(personneListe, evenement);

            participation = participationBLL.getParticipation(liste, personneConnectee);

        }

        private void RecupererListeCommentaires()
        {
            CommentairesBLL commBLL = new CommentairesBLL();

            listeCommentaires = commBLL.chargerCommentaires(evenement, personneListe);
        }

        private void RecupererListeCadeaux()
        {

            if (Session["connecte"] != null)
            {         
                ListeIdeesCadeauxBLL listeBLL = new ListeIdeesCadeauxBLL();

                listeCadeaux = listeBLL.getIdeesCadeauxByPersonneAndEvenement(personneListe, evenement);
            }
            else
            {
                Response.Redirect("~/");
            }

        }
    }
}