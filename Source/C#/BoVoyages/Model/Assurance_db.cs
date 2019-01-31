using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class Assurance_db
    {
        private string table = "Assurances";

        public Assurance_db() {}

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

        public List<DossierReservation> getDossiersForAssurance(int assuranceId)
        {
            List<DossierReservation> dossiers = new List<DossierReservation>();
            string selectString = "select * from DossierReservationPourAssurance where assuranceId = " + assuranceId + ";";
            DataSet ds = DBAccess.getInstance().execSelect(selectString);
            DossierReservation_db dossier = new DossierReservation_db();
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                dossiers.Add(dossier.getDossier(row));
            }
            return dossiers;
        }

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

        public int updateAssurance(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        public int deleteAssurance(int assuranceId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where assuranceId = " + assuranceId + ";"); ;
        }

        public int insertAssurance(Assurance assurance)
        {
            return DBAccess.getInstance().execNonQuery("insert into " + table + " (assuranceId, type, nom, description) values ('" +
                                                                               assurance.AssuranceId + "', '" +
                                                                               assurance.Type.ToString() + "', '" +
                                                                               assurance.Nom + "', '" +
                                                                               assurance.Description + ");");
        }

    }
}
