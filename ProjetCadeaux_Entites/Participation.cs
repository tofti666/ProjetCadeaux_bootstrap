using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class Participation
    {

        public int id_participation { get; set; }
        public int id_liste { get; set; }
        public int id_personne { get; set; }
        public long participation { get; set; }

        public Personne personne { get; set; }

    }
}
