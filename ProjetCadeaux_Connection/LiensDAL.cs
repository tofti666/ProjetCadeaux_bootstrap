using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux_Connection
{
    public class LiensDAL
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

        public List<Lien> getInfosLiensByIdeeCadeau(int id_ideecadeau)
        {
            String sql = "SELECT lien, id_lien FROM \"liens\" WHERE id_cadeau = (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau =" + id_ideecadeau + ");";

            try
            {

                List<Lien> listeLiensRetour = new List<Lien>();

                DataTable dt = conn.getConnection(sql);

                foreach (DataRow row in dt.Rows)
                {
                    Lien lienRetour = new Lien();

                    lienRetour.lien = row.ItemArray.GetValue(0).ToString();
                    lienRetour.id_lien = int.Parse(row.ItemArray.GetValue(1).ToString());

                    listeLiensRetour.Add(lienRetour);

                }

                return listeLiensRetour;
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
