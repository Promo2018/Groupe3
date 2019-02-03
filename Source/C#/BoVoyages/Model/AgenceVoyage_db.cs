using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    /**
     * This class provides uses the DBAccess instance to select, update, delete and insert to the AgenceVoyages database table.
     */

    class AgenceVoyage_db : Table_db
    {
        private string table = "AgencesVoyages";

        public AgenceVoyage_db() { }

        // Get the AgencesVoyages information for a particular agenceId
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

        // Get a list of the AgencesVoyages with where key=value
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

        // Get a list of all the AgencesVoyages.
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

        // Parse an AgencesVoyages from a data row.
        AgenceVoyage getAgenceVoyage(DataRow row)
        {
            return new AgenceVoyage(int.Parse(row["agenceId"].ToString()),
                                    row["nom"].ToString()
                                    );
        }

        // Update a AgencesVoyages object using the parameters change and condition.
        public int updateAgenceVoyage(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        // Delete a AgencesVoyages object with a particular id.
        public int deleteAgenceVoyage(int agenceId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where agenceId = " + agenceId + ";"); ;
        }

        // Insert a new AgencesVoyages object to the database table.
        public int insertAgenceVoyage(AgenceVoyage agenceVoyage)
        {
            return DBAccess.getInstance().execNonQuery("insert into " + table + " (agenceId, nom) values ('" +
                                                                                  agenceVoyage.AgenceId + "', '" +
                                                                                  agenceVoyage.Nom + ");");
        }

    }
}
