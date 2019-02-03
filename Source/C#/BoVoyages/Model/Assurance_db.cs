using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    /**
     * This class provides uses the DBAccess instance to select, update, delete and insert to the Assuarnces database table.
     */

    class Assurance_db : Table_db
    {
        private string table = "Assurances";

        public Assurance_db() {}

        // Get the Assurances information for a particular assuranceId
        public Assurance getAssurance(int assuranceId)
        {
            Assurance assurance = new Assurance();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where assuranceId = " + assuranceId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                assurance = getAssurance(row);
            }

            return assurance;
        }

        // Get a list of the Assurances with where key=value
        public List<Assurance> getAssurances(string key, string value)
        {
            List<Assurance> assurances = new List<Assurance>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                assurances.Add(getAssurance(row));
            }

            return assurances;
        }

        // Get a list of all the Assurances.
        public List<Assurance> getAssurances()
        {
            List<Assurance> assurances = new List<Assurance>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                assurances.Add(getAssurance(row));
            }
            return assurances;
        }

        // Get a list of DossierReservation for a particular assuranceId
        public List<DossierReservation> getDossiersForAssurance(int assuranceId)
        {
            List<DossierReservation> dossiers = new List<DossierReservation>();
            string selectString = "select * from DossiersReservationPourAssurance where assuranceId = " + assuranceId + ";";
            DataSet ds = DBAccess.getInstance().execSelect(selectString);
            DossierReservation_db dossier = new DossierReservation_db();
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                dossiers.Add(dossier.getDossier(row));
            }
            return dossiers;
        }

        // Parse an Assurances from a data row.
        public Assurance getAssurance(DataRow row)
        {
            Assurance assurance = new Assurance();
            Assurance.ASSURANCE_TYPE type = Assurance.ASSURANCE_TYPE.ANNULATION;
            switch (row["type"].ToString())
            {
                case Assurance.ANNULATION:
                    type = Assurance.ASSURANCE_TYPE.ANNULATION;
                    assurance = new AssuranceAnnulation(int.Parse(row["assuranceId"].ToString()),
                                                        type,
                                                        row["nom"].ToString(),
                                                        row["description"].ToString()
                                                        );
                    break;
                case Assurance.BAGGAGE:
                    type = Assurance.ASSURANCE_TYPE.BAGGAGE;
                    assurance = new Assurance(int.Parse(row["assuranceId"].ToString()),
                                              type,
                                              row["nom"].ToString(),
                                              row["description"].ToString()
                                              );
                    break;
                case Assurance.RAPATRIEMENT:
                    type = Assurance.ASSURANCE_TYPE.RAPATRIEMENT;
                    assurance = new Assurance(int.Parse(row["assuranceId"].ToString()),
                                              type,
                                              row["nom"].ToString(),
                                              row["description"].ToString()
                                              );
                    break;
            }
            return assurance;
        }

        // Update a Assurances object using the parameters change and condition.
        public int updateAssurance(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        // Delete a Assurances object with a particular id.
        public int deleteAssurance(int assuranceId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where assuranceId = " + assuranceId + ";"); ;
        }

        // Insert a new Assurances object to the database table.
        public int insertAssurance(Assurance assurance)
        {
            return DBAccess.getInstance().execNonQuery("insert into " + table + " (assuranceId, type, nom, description) values ('" +
                                                                               assurance.AssuranceId + "', '" +
                                                                               assurance.Type.ToString() + "', '" +
                                                                               assurance.Nom + "', '" +
                                                                               assurance.Description + ");");
        }

        // Get the assurance tarif percentage which is used to calculate the total price of a voyage.
        public double getPrixAssurancePourcentage()
        {
            double pourcentage = 0;
            string assuranceTarif = Properties.getInstance().getProperty(Properties.ASSURANCETARIF);
            pourcentage = double.Parse(assuranceTarif.Substring(0, assuranceTarif.Length - 1)) / 100; 
            return pourcentage;
        }

    }
}
