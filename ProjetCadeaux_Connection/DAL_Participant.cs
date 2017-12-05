using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using System.Data;

namespace ProjetCadeaux_Connection
{
    public class DAL_Participant
    {

        ConnectionBase conn = new ConnectionBase();

        public Participant getAllInfosByParticipantById(int pId_participant)
        {
            String sql = "SELECT id_participant, id_evt_participant, id_personne_participant, date_ajout, has_liste"
                + " FROM \"participants\" WHERE id_participant = " +pId_participant +";";

            try
            {
                Participant partRetour = new Participant();

                DataTable dt = conn.getConnection(sql);

                partRetour.id_participant = pId_participant;
                partRetour.id_evenement = int.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString());
                partRetour.id_personne = int.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString());
                partRetour.dateAjout = DateTime.Parse(dt.Rows[0].ItemArray.GetValue(3).ToString());
                partRetour.hasListe = "1".Equals(dt.Rows[0].ItemArray.GetValue(4).ToString());

                return partRetour;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Crée un participant en base de données à partir de l'objet
        /// </summary>
        /// <param name="evt">Objet évènement</param>
        public void ajoutParticipant(Participant pParticipant)
        {
            String sql = "INSERT INTO \"participants\" (id_evt_participant, id_personne_participant, has_liste) VALUES"
                + "(" + pParticipant.id_evenement + "," + pParticipant.id_personne + ","+ (pParticipant.hasListe ? 1 : 0)+"); commit;";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Le participant n'a pas été correctement ajouté.", e);
            }
        }

        /// <summary>
        /// Modifie le hasListe d'un participant en base de données à partir de l'objet
        /// </summary>
        /// <param name="evt">Objet évènement</param>
        public void modifierHasListesParticipant(Participant pParticipant)
        {
            String sql = "UPDATE \"participants\" SET has_liste = " + (pParticipant.hasListe ? 1 : 0)
                + "WHERE id_participant = "+pParticipant.id_participant +"; commit;";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Le participant n'a pas été correctement modifié.", e);
            }
        }

        public void supprimerParticipant(Participant pParticipant)
        {
            String sql = "DELETE FROM \"participants\" WHERE id_evt_participant = " 
                + pParticipant.id_evenement + " AND id_participant = " + pParticipant.id_participant + "; commit;";
                

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Le participant n'a pas été correctement supprimé.", e);
            }
        }

        /// <summary>
        /// Retourne le premier participant sélectionné à partir de l'id
        /// </summary>
        /// <param name="pid_evenement"></param>
        /// <returns>Un participant construit</returns>
        public Participant getParticipantById(int pid_participant)
        {
            String sql = "SELECT p.id_participant, p.id_evt_participant, p.id_personne_participant,"
                +" p.date_ajout, p.has_liste, pe.nom, pe.prenom FROM \"participants\" p, \"personnes\" pe"
                + " WHERE id_participant = " + pid_participant +" AND p.id_personne_participant = pe.id_personne;";

            try
            {
                DataTable dt = conn.getConnection(sql);

                Participant participant = new Participant();
                participant.id_participant= int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());
                participant.id_evenement = (int.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString()));
                participant.id_personne = (int.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString()));
                participant.dateAjout = (DateTime.Parse(dt.Rows[0].ItemArray.GetValue(3).ToString()));
                participant.hasListe = "1".Equals(dt.Rows[0].ItemArray.GetValue(4).ToString());
                participant.nom_participant = dt.Rows[0].ItemArray.GetValue(5).ToString() + " " + dt.Rows[0].ItemArray.GetValue(6).ToString();

                
                return participant;
            }
            catch (Exception e)
            {
                throw new Exception("Le participant n'a pu être récupéré", e);
            }
        }

        /// <summary>
        /// Retourne le participant sélectionné à partir de l'id_personne et de l'évènement
        /// </summary>
        /// <param name="pid_evenement"></param>
        /// <returns>Un participant construit</returns>
        public Participant getParticipantByIdPersonneAndEvenementId(int pid_personne, int pid_evenement)
        {
            String sql = "SELECT p.id_participant, p.id_evt_participant, p.id_personne_participant,"
                + " p.date_ajout, p.has_liste, pe.nom, pe.prenom FROM \"participants\" p, \"personnes\" pe"
                + " WHERE p.id_personne_participant = " + pid_personne + " AND p.id_evt_participant = " + pid_evenement + " AND p.id_personne_participant = pe.id_personne;";

            try
            {
                DataTable dt = conn.getConnection(sql);

                Participant participant = new Participant();
                participant.id_participant = int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());
                participant.id_evenement = (int.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString()));
                participant.id_personne = (int.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString()));
                participant.dateAjout = (DateTime.Parse(dt.Rows[0].ItemArray.GetValue(3).ToString()));
                participant.hasListe = "1".Equals(dt.Rows[0].ItemArray.GetValue(4).ToString());
                participant.nom_participant = dt.Rows[0].ItemArray.GetValue(5).ToString() + " " + dt.Rows[0].ItemArray.GetValue(6).ToString();


                return participant;
            }
            catch (Exception e)
            {
                throw new Exception("Le participant n'a pu être récupéré", e);
            }
        }

        /// <summary>
        /// Retourne la liste des participants à partir de l'id d'un évènement
        /// </summary>
        /// <param name="pid_evenement"></param>
        /// <returns>La liste des participants construite</returns>
        public List<Participant> getListeParticipantByIdEvenement(int pid_evenement)
        {
            String sql = "SELECT p.id_participant, p.id_evt_participant, p.id_personne_participant,"
                +" p.date_ajout, p.has_liste, upper(pe.nom), pe.prenom FROM \"participants\" p, \"personnes\" pe "
                + "WHERE p.id_evt_participant = " + pid_evenement + " AND p.id_personne_participant = pe.id_personne ORDER BY upper(pe.nom), upper(pe.prenom) ASC;";

            try
            {
                DataTable dt = conn.getConnection(sql);
                List<Participant> listeParticipant = new List<Participant>();

                //Construit la liste des participants
                foreach(DataRow row in dt.Rows){
                    Participant participant = new Participant();
                    participant.id_participant= (int.Parse(row.ItemArray.GetValue(0).ToString()));
                    participant.id_evenement = (int.Parse(row.ItemArray.GetValue(1).ToString()));
                    participant.id_personne = (int.Parse(row.ItemArray.GetValue(2).ToString()));
                    participant.dateAjout = (DateTime.Parse(row.ItemArray.GetValue(3).ToString()));
                    participant.hasListe = "1".Equals(row.ItemArray.GetValue(4).ToString());
                    participant.nom_participant = row.ItemArray.GetValue(5).ToString().ToUpper() +" " + row.ItemArray.GetValue(6).ToString();

                    listeParticipant.Add(participant);
                }

                return listeParticipant;
            }
            catch (Exception e)
            {
                throw new Exception("La liste des participants n'a pu être récupérée",e);
            }
        }



        /// <summary>
        /// Récupère la liste des personnes ayant une liste pour l'évènement passé en paramètre
        /// </summary>
        /// <param name="pid_evenement"></param>
        /// <returns></returns>
        public List<Participant> getAllParticipantAyantListeByEvenement(int pid_evenement)
        {
            String sql = "SELECT p.id_participant, p.id_evt_participant, p.id_personne_participant,"
                + " p.date_ajout, p.has_liste,  upper(pe.nom), pe.prenom FROM \"participants\" p, \"personnes\" pe "
                +"WHERE p.id_evt_participant = " + pid_evenement + "AND p.has_liste = 1 AND p.id_personne_participant = pe.id_personne "
                +"ORDER BY upper(pe.nom), upper(pe.prenom) ASC;";

            try
            {
                DataTable dt = conn.getConnection(sql);
                List<Participant> listeParticipant = new List<Participant>();

                //Construit la liste des participants
                foreach(DataRow row in dt.Rows){
                    Participant participant = new Participant();
                    participant.id_participant= (int.Parse(row.ItemArray.GetValue(0).ToString()));
                    participant.id_evenement = (int.Parse(row.ItemArray.GetValue(1).ToString()));
                    participant.id_personne = (int.Parse(row.ItemArray.GetValue(2).ToString()));
                    participant.dateAjout = (DateTime.Parse(row.ItemArray.GetValue(3).ToString()));
                    participant.hasListe = "1".Equals(row.ItemArray.GetValue(4).ToString());
                    participant.nom_participant = row.ItemArray.GetValue(5).ToString().ToUpper() + " " + row.ItemArray.GetValue(6).ToString();

                    listeParticipant.Add(participant);
                }

                return listeParticipant;
            }
            catch (Exception e)
            {
                throw new Exception("La liste des participants n'a pu être récupérée",e);
            }
        }

    }
}
