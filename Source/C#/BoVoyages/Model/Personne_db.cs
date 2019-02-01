using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class Personne_db : Table_db
    {
        public int insertPersonne(Personne personne)
        {
            int id = -1;
            DBAccess.getInstance().execNonQuery("insert into Personnes (civilite, nom, prenom, adresse, telephone, dateNaissance) values ('" +
                                                                             personne.Civilite + "', '" +
                                                                             personne.Nom + "', '" +
                                                                             personne.Prenom + "', '" +
                                                                             personne.Adresse + "', '" +
                                                                             personne.Telephone + "', '" +
                                                                             personne.DateNaissance.ToShortDateString() + "');");
            id = getLastIdentityId();
            return id;
        }
    }
}
