using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProjetCadeaux_Connection
{
    public class Liens
    {

        ConnectionBase conn = new ConnectionBase();

        public void ajouterLien(string id_cadeau, string lien)
        {
            String sql = "INSERT INTO \"liens\"(id_cadeau, lien) VALUES ('" + id_cadeau + "','" + lien.Replace("'", "''") + ");";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {

            }
        }

        public DataTable getInfosLiensByIdeeCadeau(string id_ideecadeau)
        {
            String sql = "SELECT lien, id_lien FROM \"liens\" WHERE id_cadeau = (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau ='" + id_ideecadeau + "');";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void supprimerLien(string id_lien)
        {
            String sql = "DELETE FROM \"liens\" id_lien = '"+id_lien+"';";

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
