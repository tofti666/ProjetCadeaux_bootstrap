using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class Vote
    {

        #region paramètres
        public int id_vote { get; set; }
        public int vote { get; set; }
        public long participation { get; set; }
        public Personne voteDe { get; set; }
        #endregion paramètres

    }
}
