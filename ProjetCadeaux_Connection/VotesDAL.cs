using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux_Connection
{
    public class VotesDAL
    {

        ConnectionBase conn = new ConnectionBase();

        public void ajouterVote(string id_ideecadeau, string id_personne, string vote, string participation)
        {
            String sql = "INSERT INTO \"votes\"(id_personne, id_ideecadeau, vote, participation) VALUES ('"
                + id_personne + "','" + id_ideecadeau + "','" + vote + "'," + participation.Replace("'", "''") + ");";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {

            }
        }

        public List<Vote> getListeVoteByIdeeCadeau(int id_ideeCadeau)
        {

            String sql = "SELECT v.id_vote, "
                            + " v.id_personne,"
                            + " v.vote,"
                            + " v.participation"
                        + "FROM \"votes\" v "
                        + "WHERE v.id_ideecadeau = " + id_ideeCadeau + ";";

            try
            {
                List<Vote> listeVoteRetour = new List<Vote>();

                DataTable dt = conn.getConnection(sql);

                foreach (DataRow row in dt.Rows)
                {
                    Vote voteRetour = new Vote();

                    voteRetour.id_vote = int.Parse(row.ItemArray.GetValue(0).ToString());

                    //On récupère les informations sur le votant
                    PersonneDAL personneService = new PersonneDAL();
                    voteRetour.voteDe = personneService.getInfosPersonne(int.Parse(row.ItemArray.GetValue(1).ToString()));

                    voteRetour.vote = int.Parse(row.ItemArray.GetValue(2).ToString());
                    voteRetour.participation = long.Parse(row.ItemArray.GetValue(3).ToString());

                    listeVoteRetour.Add(voteRetour);
                }

                return listeVoteRetour;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getDetailsParticipationByIdeeCadeau(string id_ideeCadeau)
        {
            String sql = "SELECT p.prenom ||' '|| p.nom as personne, case v.vote='1' when true then 'oui' else case v.vote='2' when true then 'pourquoi pas' else 'non' end end AS vote, v.participation as participation FROM \"votes\" v, \"personnes\" p WHERE v.id_ideecadeau = '" + id_ideeCadeau + "' AND v.id_personne = p.id_personne;";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getTotalParticipationByIdCadeau(string id_ideeCadeau)
        {
            String sql = "SELECT SUM(participation) FROM \"votes\" WHERE id_ideecadeau = '" + id_ideeCadeau + "';";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string getNombreOuiByIdCadeau(string id_ideeCadeau)
        {
            String sql = "SELECT id_vote FROM \"votes\" WHERE id_ideecadeau = '" + id_ideeCadeau + "' AND vote='1';";

            try
            {
                DataTable dt = new DataTable();
                dt = conn.getConnection(sql);

                return dt.Rows.Count.ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string getNombrePourquoiPasByIdCadeau(string id_ideeCadeau)
        {
            String sql = "SELECT id_vote FROM \"votes\" WHERE id_ideecadeau = '" + id_ideeCadeau + "' AND vote='2';";

            try
            {
                DataTable dt = new DataTable();
                dt = conn.getConnection(sql);

                return dt.Rows.Count.ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string getNombreNonByIdCadeau(string id_ideeCadeau)
        {
            String sql = "SELECT id_vote FROM \"votes\" WHERE id_ideecadeau = '" + id_ideeCadeau + "' AND vote='3';";

            try
            {
                DataTable dt = new DataTable();
                dt = conn.getConnection(sql);

                return dt.Rows.Count.ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public DataTable getInfosParticipationCadeauByPersonne(string id_ideeCadeau, string id_personne)
        {
            String sql = "SELECT participation, vote, id_vote FROM \"votes\" WHERE id_ideecadeau = '" + id_ideeCadeau 
                + "' AND id_personne='"+id_personne+"';";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void modifierVote(string cleIdeeCadeauVote, string id_personne, string participation, string vote)
        {
            String sql = "UPDATE \"votes\" SET participation = '" + participation.Replace("'", "''") 
                + "', vote ='"+vote+"' WHERE id_ideecadeau = '" + cleIdeeCadeauVote + "' AND id_personne = '"+id_personne+"';";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {
            }
        }

    }
}
