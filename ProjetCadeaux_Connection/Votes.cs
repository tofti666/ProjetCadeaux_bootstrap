using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProjetCadeaux_Connection
{
    public class Votes
    {

        ConnectionBase conn = new ConnectionBase();

        public void ajouterVote(string id_ideecadeau, string id_personne, string vote)
        {
            String sql = "INSERT INTO \"votes\"(id_personne, id_ideecadeau, vote) VALUES ('"
                + id_personne + "','" + id_ideecadeau + "','" + vote + "');";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {

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

        public void modifierVote(string cleIdeeCadeauVote, string id_personne, string vote)
        {
            String sql = "UPDATE \"votes\" SET" 
                + " vote ='"+vote+"' WHERE id_ideecadeau = '" + cleIdeeCadeauVote + "' AND id_personne = '"+id_personne+"';";

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
