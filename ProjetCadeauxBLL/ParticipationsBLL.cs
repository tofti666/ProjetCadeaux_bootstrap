using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using ProjetCadeaux_Connection;

namespace ProjetCadeauxBLL
{
    public class ParticipationsBLL
    {
        ParticipationsDAL participationDAL = new ParticipationsDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="participation"></param>
        /// <returns></returns>
        public Boolean ajouterParticipation(Participation participation)
        {
            return participationDAL.ajouterParticipation(participation.id_liste, participation.id_personne, participation.participation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="participation"></param>
        /// <returns></returns>
        public Boolean modifierParticipation(Participation participation)
        {
            return participationDAL.modifierParticipation(participation.id_liste, participation.id_personne, participation.participation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listeIdeesCadeaux"></param>
        /// <returns></returns>
        public Participation getParticipation(ListeIdeesCadeaux listeIdeesCadeaux, Personne participationDe)
        {
            return participationDAL.getParticipation(listeIdeesCadeaux.id_listeIdeesCadeaux, participationDe.id_personne);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listeIdeesCadeaux"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        public long getTotalParticipation(ListeIdeesCadeaux listeIdeesCadeaux)
        {
            return participationDAL.getTotalParticipation(listeIdeesCadeaux.id_listeIdeesCadeaux);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listeIdeesCadeaux"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        public List<Participation> getListeParticipation(ListeIdeesCadeaux listeIdeesCadeaux)
        {
            return participationDAL.getListeParticipation(listeIdeesCadeaux.id_listeIdeesCadeaux);
        }

        public List<Participation> getAllParticipations(Participation parti, Evenement evt)
        {
            return participationDAL.getAllParticipations(parti.id_personne, evt.id_evenement);
        }
    }
}
