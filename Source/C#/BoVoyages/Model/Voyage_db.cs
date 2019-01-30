using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class Voyage_db
    {
        public Voyage_db voyage_db = new Voyage_db();

        public Voyage_db() { }

        public Voyage getVoyage(int voyageId)
        {
            Voyage voyage = new Voyage();
            DataSet ds = DBAccess.getInstance().execSelect("select * from Voyage where voyageId = " + voyageId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                voyage = getVoyage(row);
            }

            return voyage;
        }

        public List<Voyage> getVoyages(string key, string value)
        {
            List<Voyage> voyages = new List<Voyage>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from Voyage where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                voyages.Add(getVoyage(row));
            }

            return voyages;
        }

        public List<Voyage> getVoyages()
        {
            List<Voyage> voyages = new List<Voyage>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from Voyage;");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                voyages.Add(getVoyage(row));
            }
            return voyages;
        }

        Voyage getVoyage(DataRow row)
        {
            return new Voyage(int.Parse(row["voyageId"].ToString()),
                              (DateTime)(row["dateAller"]),
                              (DateTime)(row["dateRetour"]),
                              int.Parse(row["placesDisponible"].ToString()),
                              float.Parse(row["tarifToutCompris"].ToString()),
                              int.Parse(row["destinationId"].ToString()),
                              int.Parse(row["agenceId"].ToString())
                              );
        }

        public int updateVoyage(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update Voyage set " + change + " where " + condition + ";");
        }

        public int deleteVoyage(int voyageId)
        {
            return DBAccess.getInstance().execNonQuery("delete from Voyage where voyageId = " + voyageId + ";"); ;
        }

        public int insertVoyage(Voyage voyage)
        {
            return DBAccess.getInstance().execNonQuery("insert into Voyage (voyageId, dateAller, dateRetour, placesDisponible, tarifToutCompris, destinationId, agenceId) values ('" +
                                                                            voyage.VoyageId + "', '" +
                                                                            voyage.DateAller.ToShortDateString() + "', '" +
                                                                            voyage.DateRetour.ToShortDateString() + "', '" +
                                                                            voyage.PlacesDisponible + "', '" +
                                                                            voyage.TarifToutCompris + "', '" +
                                                                            voyage.DestinationId + "', '" +
                                                                            voyage.AgenceId + ");");
        }

    }
}
