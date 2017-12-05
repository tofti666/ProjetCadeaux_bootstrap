using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using System.Data;

namespace ProjetCadeaux_Connection
{
    public class CadeauDAL
    {

        ConnectionBase conn = new ConnectionBase();

        public Cadeau getInfosCadeau(int pId_cadeau)
        {
            String sql = "SELECT intitule_cadeau, description, prix, id_cadeau"
                +" FROM \"cadeaux\""
                +" WHERE id_cadeau = "+pId_cadeau +";";
            try
            {
                Cadeau cadeauRetour = new Cadeau();

                DataTable dt = conn.getConnection(sql);

                cadeauRetour.id_cadeau = pId_cadeau;
                cadeauRetour.intitule_cadeau = dt.Rows[0].ItemArray.GetValue(0).ToString();
                cadeauRetour.description = dt.Rows[0].ItemArray.GetValue(1).ToString();
                cadeauRetour.prix = dt.Rows[0].ItemArray.GetValue(2).ToString();

                return cadeauRetour;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
