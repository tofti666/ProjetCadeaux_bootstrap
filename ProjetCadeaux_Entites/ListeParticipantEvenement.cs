using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class ListeParticipantEvenement
    {

        #region paramètres
        private int id_evntParticipant;
        private int id_participant;
        private int id_liste;
        private int id_evenement;
        #endregion paramètres

        #region Constructeur
        public ListeParticipantEvenement(int pid_evntParticipant, int pid_participant, int pid_liste, int pid_evenement)
        {
            this.id_evntParticipant = pid_evntParticipant;
            this.id_participant = pid_participant;
            this.id_liste = pid_liste;
            this.id_evenement = pid_evenement;
        }
        #endregion

        #region setters
        //Setters
        public void setIdEvntParticipant(int id_evntParticipant)
        {
            this.id_evntParticipant = id_evntParticipant;
        }

        public void setIdParticipant(int id_participant)
        {
            this.id_participant = id_participant;
        }

        public void setIdListe(int id_liste)
        {
            this.id_liste = id_liste;
        }

        public void setIdEvenement(int id_evenement)
        {
            this.id_evenement = id_evenement;
        }
        #endregion setters

        #region getters
        //Getters
        public int getIdEvntParticipant()
        {
            return this.id_evntParticipant;
        }

        public int getIdParticipant()
        {
            return this.id_participant;
        }

        public int getIdListe()
        {
            return this.id_liste;
        }

        public int getIdEvenement()
        {
            return this.id_evenement;
        }
        #endregion getters

        #region fonctions
        public String toString()
        {
            return "ListeParticipantEvenement [ id = " + id_evntParticipant
                + "; participant = " + id_participant
            + "; id_liste = " + id_liste
            + "; id_evenement = " + id_evenement
            + "]";
        }
        #endregion fonctions

    }
}
