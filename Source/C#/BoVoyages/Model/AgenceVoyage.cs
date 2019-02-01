using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    public class AgenceVoyage : Table
    {
        private AgenceVoyage_db agenceVoyage_db = new AgenceVoyage_db();
        private int agenceId;
        private string nom;

        public AgenceVoyage() { }

        public AgenceVoyage(int agenceId, string nom)
        {
            this.AgenceId = agenceId;
            this.Nom = nom;
        }

        public int AgenceId { get => agenceId; set => agenceId = value; }
        public string Nom { get => nom; set => nom = value; }

        public override void startTransaction()
        {
            agenceVoyage_db.startTransaction();
        }

        public override int endTransaction(bool commit)
        {
            return agenceVoyage_db.endTransaction(commit);
        }

        public AgenceVoyage getAgenceVoyage(int agenceId)
        {
            return agenceVoyage_db.getAgenceVoyage(agenceId);
        }

        public List<AgenceVoyage> getAgenceVoyages(string key, string value)
        {
            return agenceVoyage_db.getAgenceVoyages(key, value);
        }

        public List<AgenceVoyage> getAgenceVoyages()
        {
            return agenceVoyage_db.getAgenceVoyages();
        }

        public int updateAgenceVoyage(string change, string condition)
        {
            return agenceVoyage_db.updateAgenceVoyage(change, condition);
        }

        public int deleteAgenceVoyage(int agenceId)
        {
            return agenceVoyage_db.deleteAgenceVoyage(agenceId);
        }

        public int insertAgenceVoyage(AgenceVoyage agenceVoyage)
        {
            return agenceVoyage_db.insertAgenceVoyage(agenceVoyage);
        }

    }
}
