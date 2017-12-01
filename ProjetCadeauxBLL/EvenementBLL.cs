using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using ProjetCadeaux_Connection;

namespace ProjetCadeauxBLL
{
    public class EvenementBLL
    {

        /// <summary>
        /// Récupère la liste des évènements auxquels participe une personne
        /// </summary>
        /// <param name="id_personne"></param>
        /// <returns>Une liste d'évènements</returns>
        public List<Evenement> getAllEvenementByIdPersonne(string id_personne)
        {

            DAL_Evenement evenementService = new DAL_Evenement();

            return evenementService.getAllEvenementByIdPersonne(id_personne);

        }

        public List<Evenement> getAllEvenementAvecListeByIdPersonne(Personne pPersonne)
        {
            DAL_Evenement evenementService = new DAL_Evenement();

            return evenementService.getAllEvenementAvecListeByIdPersonne(pPersonne.id_personne);
        }

        /// <summary>
        /// Retourne un évènement à partir de son Id.
        /// </summary>
        /// <param name="id_evenement"></param>
        /// <returns>Un évènement</returns>
        public Evenement getEvenementById(int id_evenement)
        {
            DAL_Evenement evenementService = new DAL_Evenement();

            return evenementService.getEvenementById(id_evenement);
        }

        /// <summary>
        /// Créé un évènement, mais sans participant.. A ne pas utiliser car ce n'est pas logique de ne pas avoir de participant.
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        public Boolean creerEvenement(Evenement evt)
        {
            DAL_Evenement evenementService = new DAL_Evenement();

            return evenementService.creerEvenement(evt);

        }

        /// <summary>
        /// Créé un évènement avec comme participant celui qui est connecté.
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="participant"></param>
        /// <returns>un booléen qui dit si l'action s'est bien passé</returns>
        public Evenement creerEvenement(Evenement evt, Participant participant)
        {
            DAL_Evenement evenementService = new DAL_Evenement();

            return evenementService.creerEvenement(evt,participant);

        }

    }
}
