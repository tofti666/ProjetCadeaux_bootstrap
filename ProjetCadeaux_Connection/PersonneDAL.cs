using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux_Connection
{
    public class PersonneDAL
    {

        ConnectionBase conn = new ConnectionBase();

        public Personne getInfosPersonne(int pIdPersonne)
        {
            String sql = "SELECT nom, prenom, login, email FROM \"personnes\" "
                + "WHERE id_personne = '" + pIdPersonne + "';";

            try
            {
                DataTable dt = conn.getConnection(sql);

                Personne personneRetour = new Personne();

                personneRetour.id_personne = pIdPersonne;
                personneRetour.nom = dt.Rows[0].ItemArray.GetValue(0).ToString();
                personneRetour.prenom = dt.Rows[0].ItemArray.GetValue(1).ToString();
                personneRetour.login = dt.Rows[0].ItemArray.GetValue(2).ToString();
                personneRetour.email = dt.Rows[0].ItemArray.GetValue(3).ToString();

                return personneRetour;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
