using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux_Connection
{
    public class DAL_Evenement
    {

        ConnectionBase conn = new ConnectionBase();

        /// <summary>
        /// Crée un évènement en base de données à partir de l'objet. A ne pas utiliser.
        /// </summary>
        /// <param name="evt">Objet évènement</param>
        public Boolean creerEvenement(Evenement evt)
        {
            String sql = "INSERT INTO evenements(libelle_evt, date_butoir, date_evt, id_admin) VALUES"
                + "('" + evt.libelle.Replace("'", "''") + "','" + evt.dateButoir.ToShortDateString() 
                + "','" + evt.dateEvenement.ToShortDateString() + "','" + evt.id_admin + "'); commit;";

            try
            {
                conn.getVoidConnection(sql);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Crée un évènement en base de données à partir de l'objet, et de celui qui créé l'évènement
        /// </summary>
        /// <param name="evt">Objet évènement</param>
        public Evenement creerEvenement(Evenement evt, Participant participant)
        {
            String sql = "SELECT creerEvenement('"
                +evt.libelle+"','"
                +evt.dateButoir+"','"
                +evt.dateEvenement+"',"
                +evt.id_admin+","
                +participant.id_personne+","
                + (participant.hasListe ? 1 : 0) + "); commit;";
            
            try
            {
                DataTable dt = conn.getConnection(sql);

                evt.id_evenement = int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());

                return evt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retourne le premier évènement sélectionné à partir de l'id
        /// </summary>
        /// <param name="pid_evenement"></param>
        /// <returns>Un évènement construit</returns>
        public Evenement getEvenementById(int pid_evenement)
        {
            String sql = "SELECT id_evenement, libelle_evt, date_butoir, date_evt, id_admin FROM \"evenements\" WHERE id_evenement = " + pid_evenement + ";";

            try
            {
                DataTable dt = conn.getConnection(sql);

                Evenement evt = new Evenement();
                evt.id_evenement = int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());
                evt.libelle = dt.Rows[0].ItemArray.GetValue(1).ToString();
                evt.dateButoir = DateTime.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString());
                evt.dateEvenement = DateTime.Parse(dt.Rows[0].ItemArray.GetValue(3).ToString());
                evt.id_admin = int.Parse(dt.Rows[0].ItemArray.GetValue(4).ToString());

                return evt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Evenement> getAllEvenementByIdPersonne(string id_personne)
        {
            String sql = "SELECT e.id_evenement, e.libelle_evt, e.date_butoir, e.date_evt, e.id_admin,"
                +" p.id_participant, p.id_evt_participant, p.date_ajout, p.id_personne_participant"
                +" FROM evenements e, participants p "
                + "WHERE p.id_personne_participant = " + id_personne + " AND p.id_evt_participant = e.id_evenement ORDER BY e.date_evt DESC;";

            try
            {
                DataTable dt = conn.getConnection(sql);

                List<Evenement> listeEvenementRetour = new List<Evenement>();

                foreach (DataRow row in dt.Rows)
                {
                    Evenement evt = new Evenement();
                    evt.id_evenement = int.Parse(row.ItemArray.GetValue(0).ToString());
                    evt.libelle = row.ItemArray.GetValue(1).ToString();
                    evt.dateButoir = DateTime.Parse(row.ItemArray.GetValue(2).ToString());
                    evt.dateEvenement = DateTime.Parse(row.ItemArray.GetValue(3).ToString());
                    evt.id_admin = int.Parse(row.ItemArray.GetValue(4).ToString());

                    listeEvenementRetour.Add(evt);
                }
                return listeEvenementRetour;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<Evenement> getAllEvenementAvecListeByIdPersonne(int id_personne)
        {
            String sql = "SELECT e.id_evenement, e.libelle_evt, e.date_butoir, e.date_evt, e.id_admin,"
                +" p.id_participant, p.id_evt_participant, p.date_ajout, p.id_personne_participant"
                +" FROM evenements e, participants p "
                + "WHERE p.id_personne_participant = " + id_personne + " AND p.id_evt_participant = e.id_evenement AND p.has_liste = 1 ORDER BY e.date_evt DESC;";

            try
            {
                DataTable dt = conn.getConnection(sql);

                List<Evenement> listeEvenementRetour = new List<Evenement>();

                foreach (DataRow row in dt.Rows)
                {
                    Evenement evt = new Evenement();
                    evt.id_evenement = int.Parse(row.ItemArray.GetValue(0).ToString());
                    evt.libelle = row.ItemArray.GetValue(1).ToString();
                    evt.dateButoir = DateTime.Parse(row.ItemArray.GetValue(2).ToString());
                    evt.dateEvenement = DateTime.Parse(row.ItemArray.GetValue(3).ToString());
                    evt.id_admin = int.Parse(row.ItemArray.GetValue(4).ToString());

                    listeEvenementRetour.Add(evt);
                }
                return listeEvenementRetour;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
