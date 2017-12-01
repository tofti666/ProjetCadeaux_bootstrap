using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using ProjetCadeaux_Connection;

namespace ProjetCadeauxBLL
{
    public class ParticipantBLL
    {

        public Boolean ajouterListeParticipant(List<Participant> listeParticipant)
        {
            Boolean retour = true;
            
            foreach (Participant part in listeParticipant)
            {
                if(retour)
                    retour = this.ajouterParticipant(part);
            }

            return retour;
        }

        public Boolean ajouterParticipant(Participant part)
        {
            DAL_Participant partService = new DAL_Participant();
            try
            {
                partService.ajoutParticipant(part);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean modifierHasListesListeParticipants(List<Participant> listeParticipant)
        {
            Boolean retour = true;

            foreach (Participant part in listeParticipant)
            {
                if (retour)
                    retour = this.modifierHasListesParticipant(part);
            }

            return retour;
        }

        public Boolean modifierHasListesParticipant(Participant part)
        {
            DAL_Participant partService = new DAL_Participant();
            try
            {
                partService.modifierHasListesParticipant(part);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean supprimerListeParticipants(List<Participant> listeParticipant)
        {
            Boolean retour = true;
            
            foreach (Participant part in listeParticipant)
            {
                if(retour)
                    retour = this.supprimerParticipant(part);
            }

            return retour;
        }

        
        public Boolean supprimerParticipant(Participant part)
        {
            DAL_Participant partService = new DAL_Participant();
            try
            {
                partService.supprimerParticipant(part);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        

        public List<Participant> getAllParticipantByEvenement(Evenement evt)
        {
            DAL_Participant participantService = new DAL_Participant();

            return participantService.getListeParticipantByIdEvenement(evt.id_evenement);
        }

        public List<Participant> getAllParticipantAyantListeByEvenement(Evenement evt)
        {
            DAL_Participant participantService = new DAL_Participant();

            return participantService.getAllParticipantAyantListeByEvenement(evt.id_evenement);
        
        }

        public Participant getParticipantAyantListeByEvenementAndPersonne(Evenement evt, Personne personne)
        {
            DAL_Participant participantService = new DAL_Participant();

            return participantService.getParticipantByIdPersonneAndEvenementId(personne.id_personne,evt.id_evenement);

        }

        public List<Participant> getAllParticipantSaufConnecteAyantListeByEvenement(Evenement evt, Participant part)
        {
            DAL_Participant participantService = new DAL_Participant();

            List<Participant> listeTmp = participantService.getAllParticipantAyantListeByEvenement(evt.id_evenement);

            for (int i = 0; i < listeTmp.Count; i++)
            {
                Boolean onBreak = false;
                if (listeTmp[i].id_personne == part.id_personne)
                {
                    listeTmp.Remove(listeTmp[i]);
                    onBreak = true;
                }
                if (onBreak)
                {
                    break;
                }
            }

            return listeTmp;

        }

        public Participant getAllInfosByParticipant(Participant part)
        {
            DAL_Participant participantService = new DAL_Participant();

            return participantService.getAllInfosByParticipantById(part.id_participant);
        }

    }
}
