using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Entites;
using ProjetCadeauxBLL;
using ProjetCadeaux_Connection;

namespace ProjetCadeaux.pages.Events
{
    public partial class creerEvenement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null)
            {

            }
            else
            {
                Response.Redirect("~/");
            }
        }

        protected void btnAjouterEvt_Click(object sender, EventArgs e)
        {
            int idPersonne = int.Parse(Session["personneID"].ToString());

            ProjetCadeaux_Entites.Evenement evt = new ProjetCadeaux_Entites.Evenement();

            evt.libelle = TitreEvenementTb.Text;
            evt.dateEvenement = DateTime.Parse(dateEvenementTb.Text);
            evt.dateButoir = DateTime.Parse(dateButoirTb.Text);
            evt.id_admin = idPersonne;

            Participant participant = new Participant();

            participant.dateAjout = DateTime.Now;
            participant.id_personne = idPersonne;
            participant.hasListe = hasListeCb.Checked;

            EvenementBLL evtBLL = new EvenementBLL();

            ProjetCadeaux_Entites.Evenement evtRetour = evtBLL.creerEvenement(evt, participant);
            
            ListeIdeesCadeauxBLL listeIdeesService = new ListeIdeesCadeauxBLL();

            if(evtRetour != null){

                ListeIdeesCadeaux listeRetour = listeIdeesService.creerListeIdeesCadeaux(participant, evtRetour, hasListeCb.Checked);

                if (listeRetour != null)
                {

                    SuccessText.Text = "L'évènement a bien été créé.";
                    FailureText.Text = null;

                    TitreEvenementTb.Text = "";
                    dateEvenementTb.Text = "";
                    dateButoirTb.Text = "";
                    hasListeCb.Checked = false;

                    Page_Load(sender, e);
                }
                else
                {
                    TitreEvenementTb.Text = "";
                    dateEvenementTb.Text = "";
                    dateButoirTb.Text = "";
                    hasListeCb.Checked = false;

                    SuccessText.Text = null;
                    FailureText.Text = "L'évènement a été créé, mais la liste n'a pas été créée.";

                    Page_Load(sender, e);
                }

            }
            else
            {
                TitreEvenementTb.Text = "";
                dateEvenementTb.Text = "";
                dateButoirTb.Text = "";
                hasListeCb.Checked = false;

                SuccessText.Text = null;
                FailureText.Text = "L'évènement n'a pas pu être créé.";

                Page_Load(sender, e);
            }
        }

        protected void btnModifierEvt_Click(object sender, EventArgs e)
        {

        }
    }
}