using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using ProjetCadeaux_Connection;

namespace ProjetCadeauxBLL
{
    public class ListeIdeesCadeauxBLL
    {
        /// <summary>
        /// Crée une liste d'idées de cadeaux
        /// </summary>
        /// <param name="pParticipant"></param>
        /// <param name="pEvt"></param>
        /// <param name="pHasListeCb"></param>
        /// <returns></returns>
        public ListeIdeesCadeaux creerListeIdeesCadeaux(Participant pParticipant, Evenement pEvt, Boolean pHasListeCb)
        {
            ListeIdeesCadeauxDAL listeIdeesService = new ListeIdeesCadeauxDAL();

            return listeIdeesService.creerListeIdeesCadeaux(pParticipant.id_personne, pEvt.id_evenement, pHasListeCb);
        }

        /// <summary>
        /// Crée une liste d'idées de cadeaux à partir d'une liste de participants
        /// </summary>
        /// <param name="pParticipant"></param>
        /// <param name="pEvt"></param>
        /// <param name="pHasListeCb"></param>
        /// <returns></returns>
        public Boolean creerListeIdeesCadeaux(List<Participant> pListeParticipant, Evenement pEvt)
        {
            Boolean retour = true;
            
            foreach (Participant part in pListeParticipant)
            {
                retour = retour && (creerListeIdeesCadeaux(part, pEvt, part.hasListe) != null);
            }

            return retour;
        }

        /// <summary>
        /// Récupère un objet ListeIdeesCadeaux à partir d'un évènement et d'une personne
        /// </summary>
        /// <param name="pId_personne"></param>
        /// <param name="pId_evt"></param>
        /// <returns></returns>
        public ListeIdeesCadeaux getListeIdeesCadeaux(Personne Ppersonne, Evenement pEvt)
        {
            ListeIdeesCadeauxDAL listeService = new ListeIdeesCadeauxDAL();

            return listeService.getListeIdeesCadeaux(Ppersonne.id_personne, pEvt.id_evenement);

        }

        public ListeIdeesCadeaux getListeIdeesCadeauxPasSuggere(Personne Ppersonne, Evenement pEvt)
        {
            ListeIdeesCadeauxDAL listeService = new ListeIdeesCadeauxDAL();

            return listeService.getListeIdeesCadeauxPasSuggere(Ppersonne.id_personne, pEvt.id_evenement);

        }

        /// <summary>
        /// Met à jour le booléen de la liste des cadeaux
        /// </summary>
        /// <param name="pParticipant"></param>
        /// <param name="pEvt"></param>
        /// <param name="pIsListeActive"></param>
        /// <returns></returns>
        public Boolean updateActiveListe(Participant pParticipant, Evenement pEvt, Boolean pIsListeActive)
        {
            ListeIdeesCadeauxDAL listeIdeesService = new ListeIdeesCadeauxDAL();

            return listeIdeesService.updateActiveListe(pParticipant.id_personne, pEvt.id_evenement, pIsListeActive);
        }

        /// <summary>
        /// Met à jour le booléen de la liste des cadeaux pour chaque participant de la liste
        /// </summary>
        /// <param name="pParticipant"></param>
        /// <param name="pEvt"></param>
        /// <param name="pIsListeActive"></param>
        /// <returns></returns>
        public Boolean updateActiveListe(List<Participant> pListeParticipant, Evenement pEvt)
        {
            Boolean retour = true;

            foreach (Participant part in pListeParticipant)
            {
                retour = retour && updateActiveListe(part, pEvt, part.hasListe);
            }

            return retour;
        }

        /// <summary>
        /// Désactive la liste de cadeaux de tous les participants de la liste
        /// </summary>
        /// <param name="pListeParticipant"></param>
        /// <param name="pEvt"></param>
        /// <returns></returns>
        public Boolean desactiverListe(List<Participant> pListeParticipant, Evenement pEvt)
        {
            Boolean retour = true;

            foreach (Participant part in pListeParticipant)
            {
                retour = retour && desactiverListe(part, pEvt);
            }

            return retour;
        }

        /// <summary>
        /// Désactive la liste de cadeaux du participant en question, pour cet évènement
        /// </summary>
        /// <param name="pParticipant"></param>
        /// <param name="pEvt"></param>
        /// <returns></returns>
        public Boolean desactiverListe(Participant pParticipant, Evenement pEvt)
        {
            Boolean retour = true;

            ListeIdeesCadeauxDAL listeService = new ListeIdeesCadeauxDAL();
            retour = listeService.updateActiveListe(pParticipant.id_personne, pEvt.id_evenement, false);

            return retour;
        }

        /// <summary>
        /// Récupère la liste associée à la personne et à l'évènement, et appelle la fonction qui ajoute un cadeau à une liste
        /// </summary>
        /// <param name="pPers"></param>
        /// <param name="pEvt"></param>
        /// <param name="pIdee"></param>
        /// <returns></returns>
        public bool ajouterCadeauToListe(Personne pPers, Evenement pEvt, IdeeCadeau pIdee)
        {
            Boolean retour = true;

            ListeIdeesCadeaux listeRetour = getListeIdeesCadeaux(pPers, pEvt);

            ListeIdeesCadeauxDAL listeService = new ListeIdeesCadeauxDAL();

            retour = listeService.ajouterCadeauToListe(listeRetour.id_listeIdeesCadeaux, pIdee.id_ideeCadeau);

            return retour;
            
        }

        /// <summary>
        /// Supprime le lien entre une liste d'idées cadeaux, et un cadeau donné en paramètre.
        /// On part ici du principe qu'il n'y a qu'une liste pour chaque cadeau, mais cela peut être amené à changer par la suite
        /// </summary>
        /// <param name="ideeCadeau"></param>
        /// <returns></returns>
        public bool supprimerCadeauFromListe(IdeeCadeau ideeCadeau)
        {
            ListeIdeesCadeauxDAL listeService = new ListeIdeesCadeauxDAL();

            return listeService.supprimerCadeauFromListe(ideeCadeau.id_ideeCadeau);
        }

        /// <summary>
        /// Récupère l'ensemble des listes d'idées cadeaux pour un évènement
        /// </summary>
        /// <param name="evt"></param>
        public List<ListeIdeesCadeaux> getAllListeIdeesCadeaux(Evenement pEvt)
        {
            ListeIdeesCadeauxDAL listeService = new ListeIdeesCadeauxDAL();

            return listeService.getAllListeIdeesCadeaux(pEvt.id_evenement);
        }

        public List<IdeeCadeauPourListe> getIdeesCadeauxByPersonneAndEvenement(Personne pPersonne, Evenement pEvenement){


            ListeIdeesCadeauxDAL listeCadeauDAL = new ListeIdeesCadeauxDAL();

            return listeCadeauDAL.getListeIdeesCadeauxByPersonneEtEvenement(pPersonne.id_personne, pEvenement.id_evenement);

        }
    }
}
