using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class Participant
    {

        #region paramètres
        public int id_participant {get; set;}
        public int id_personne { get; set; }
        public DateTime dateAjout { get; set; }
        public int id_evenement { get; set; }
        public Boolean hasListe { get; set; }

        public string nom_participant { get; set; }
        #endregion paramètres

        #region Constructeur
        public Participant(int pid_participant, int pid_personne, DateTime pdateAjout, int pid_evenement, Boolean pHasListe)
        {
            this.id_participant = pid_participant;
            this.id_personne = pid_personne;
            this.dateAjout = pdateAjout;
            this.id_evenement = pid_evenement;
            this.hasListe = pHasListe;
        }

        public Participant()
        {
        }
        #endregion

        #region fonctions
        public String toString()
        {
            return "Participant [ id = " + id_participant
                + "; participant = " + id_personne
                + "; dateAjout = " + dateAjout.ToShortDateString()
                + "; id_evenement = " + id_evenement
                + "; hasListe = " + hasListe
                + "; nom_participant = " + nom_participant
                + "]";
        }
        #endregion fonctions

    }
}
