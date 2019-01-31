using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class AgenceVoyage_db
    {
        private string table = "AgencesVoyages";

        public AgenceVoyage_db() { }

        public AgenceVoyage getAgenceVoyage(int agenceId)
        {
            AgenceVoyage agenceVoyage = new AgenceVoyage();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where agenceId = " + agenceId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                agenceVoyage = getAgenceVoyage(row);
            }

            return agenceVoyage;
        }

        public List<AgenceVoyage> getAgenceVoyages(string key, string value)
        {
            List<AgenceVoyage> agenceVoyages = new List<AgenceVoyage>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                agenceVoyages.Add(getAgenceVoyage(row));
            }

            return agenceVoyages;
        }

        public List<AgenceVoyage> getAgenceVoyages()
        {
            List<AgenceVoyage> agenceVoyages = new List<AgenceVoyage>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                agenceVoyages.Add(getAgenceVoyage(row));
            }
            return agenceVoyages;
        }

        AgenceVoyage getAgenceVoyage(DataRow row)
        {
            return new AgenceVoyage(int.Parse(row["agenceId"].ToString()),
                                    row["nom"].ToString()
                                    );
        }

        public int updateAgenceVoyage(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        public int deleteAgenceVoyage(int agenceId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where agenceId = " + agenceId + ";"); ;
        }

        public int insertAgenceVoyage(AgenceVoyage agenceVoyage)
        {
            return DBAccess.getInstance().execNonQuery("insert into " + table + " (agenceId, nom) values ('" +
                                                                                  agenceVoyage.AgenceId + "', '" +
                                                                                  agenceVoyage.Nom + ");");
        }

    }
}
