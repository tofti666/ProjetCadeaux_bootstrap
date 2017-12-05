using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux_Connection
{
    public class ParticipationsDAL
    {

        ConnectionBase conn = new ConnectionBase();

        public Boolean ajouterParticipation(int id_liste, int id_personne, long participation)
        {
            String sql = "INSERT INTO \"participations\"(id_liste, id_personne, participation) VALUES ("
                + id_liste + "," + id_personne + "," + participation + ");";

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

        public Boolean modifierParticipation(int id_liste, int id_personne, long participation)
        {
            String sql = "UPDATE \"participations\" SET participation = "+ participation
                + " WHERE id_liste = "+id_liste+" AND id_personne = "+id_personne+" ;";

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

        public Participation getParticipation(int id_liste, int id_personne)
        {
            String sql = "SELECT id_participation, participation FROM \"participations\" "
                + " WHERE id_liste = " + id_liste + " AND id_personne = " + id_personne + " ;";

            try
            {
                DataTable dt = conn.getConnection(sql);

                Participation part = new Participation();

                part.id_participation = int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());
                part.participation = long.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString());
                part.id_liste = id_liste;
                part.id_personne = id_personne;

                return part;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_liste"></param>
        /// <param name="id_evenement"></param>
        /// <returns></returns>
        public long getTotalParticipation(int id_liste)
        {
            String sql = "SELECT SUM(participation) FROM \"participations\" "
                + " WHERE id_liste = " + id_liste + ";";

            try
            {
                DataTable dt = conn.getConnection(sql);

                return long.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());

            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Participation> getListeParticipation(int id_liste)
        {
            String sql = "SELECT * FROM \"participations\" "
                + "WHERE id_liste = " + id_liste + ";";

            try
            {
                DataTable dt = conn.getConnection(sql);

                List<Participation> listeRetour = new List<Participation>();

                foreach(DataRow row in dt.Rows){
                    Participation participation = new Participation();
                    participation.id_participation = (int.Parse(row.ItemArray.GetValue(0).ToString()));
                    participation.id_liste = (int.Parse(row.ItemArray.GetValue(1).ToString()));
                    participation.id_personne = (int.Parse(row.ItemArray.GetValue(2).ToString()));
                    participation.participation = (long.Parse(row.ItemArray.GetValue(3).ToString()));

                    PersonneDAL persService = new PersonneDAL();
                    participation.personne = persService.getInfosPersonne(participation.id_personne);
                    
                    listeRetour.Add(participation);
                }

                return listeRetour;

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retourne l'ensemble des participations d'une personne aux listes pour un évènement donné
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <returns></returns>
        public List<Participation> getAllParticipations(int pId_personne, int pId_evt)
        {
            String sql = "SELECT pa.id_participation, l.id_liste, l.id_personne, pa.participation "
                + "from \"personnes\" pe, \"listeCadeaux\" l "
                + "LEFT OUTER JOIN \"participations\" pa "
                + "ON pa.id_liste = l.id_liste AND pa.id_personne = "+pId_personne +" "
                + "WHERE l.id_evenement = " + pId_evt + " AND pe.id_personne = l.id_personne AND pe.id_personne != " + pId_personne + " AND pa.id_participation is not null;";

            try
            {
                DataTable dt = conn.getConnection(sql);

                List<Participation> listeRetour = new List<Participation>();

                foreach (DataRow row in dt.Rows)
                {
                    Participation participation = new Participation();
                    participation.id_participation = (int.Parse(row.ItemArray.GetValue(0).ToString()));
                    participation.id_liste = (int.Parse(row.ItemArray.GetValue(1).ToString()));
                    participation.id_personne = pId_personne;
                    participation.participation = (long.Parse(row.ItemArray.GetValue(3).ToString()));

                    PersonneDAL persService = new PersonneDAL();
                    participation.personne = persService.getInfosPersonne(participation.id_personne);

                    listeRetour.Add(participation);
                }

                return listeRetour;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
