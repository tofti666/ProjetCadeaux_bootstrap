using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeauxBLL;
using ProjetCadeaux_Entites;
using System.Data;

namespace ProjetCadeaux.pages.Events
{
    public partial class Event : System.Web.UI.Page
    {
        public List<Participant> listeParticipantAyantListeCadeau = new List<Participant>();

        public ParticipantBLL participantBLL = new ParticipantBLL();
        public Evenement evt = new Evenement();
        public Participant connecte = new Participant();
        public Personne persConnecte = new Personne();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null && Request.Params["evenementId"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["evenementId"] = Request.Params["evenementId"].ToString();

                    //RechargerGridViewParticipants();
                }

                if (ViewState["listePersonnesChargee"] == null || ViewState["listePersonnesChargee"].ToString() != "true")
                {
                    


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

                    /*listeParticipantAyantListe.DataValueField = "ID";
                    listeParticipantAyantListe.DataTextField = "nom";

                    listeParticipantAyantListe.DataSource = retour;
                    listeParticipantAyantListe.DataBind();
                    ViewState["listePersonnesChargee"] = "true";*/

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

        protected void btnModifierParticipation_Click(object sender, EventArgs e){}

        protected void btnAjouterParticipation_Click(object sender, EventArgs e) { }

        protected void btnAnnulerAjoutParticipation_Click(object sender, EventArgs e){}

        protected void RowCommand_click(object sender, EventArgs e){}

        protected void RowCommandSuggestions_click(object sender, EventArgs e){}

        protected void btnAjouterIdee_Click(object sender, EventArgs e) { }
    }
}