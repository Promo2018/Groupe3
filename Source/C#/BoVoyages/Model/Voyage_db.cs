using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class Voyage_db : Table_db
    {
        private const string table = "Voyages";
        private const string view = "allVoyages";

        public Voyage_db() { }

        public Voyage getVoyage(int voyageId)
        {
            Voyage voyage = new Voyage();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + view + " where voyageId = " + voyageId + " order by dateAller;");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                voyage = getVoyage(row);
            }

            return voyage;
        }

        public List<Voyage> getVoyages(string key, string value)
        {
            List<Voyage> voyages = new List<Voyage>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + view + " where " + key + " = '" + value + "' order by dateAller;");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                voyages.Add(getVoyage(row));
            }

            return voyages;
        }

        public List<Voyage> getVoyages()
        {
            List<Voyage> voyages = new List<Voyage>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + view + " order by dateAller;");
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
                              new Destination(int.Parse(row["destinationId"].ToString()),
                                              row["continent"].ToString(),
                                              row["pays"].ToString(),
                                              row["region"].ToString(),
                                              row["description"].ToString()),
                              new AgenceVoyage(int.Parse(row["agenceId"].ToString()),
                                              row["nom"].ToString())
                              );
        }

        public int updateVoyage(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        public int deleteVoyage(int voyageId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where voyageId = " + voyageId + ";"); ;
        }

        public int insertVoyage(Voyage voyage)
        {
            DBAccess.getInstance().execNonQuery("insert into " + table + " (dateAller, dateRetour, placesDisponible, tarifToutCompris, destinationId, agenceId) values ('" +
                                                                            voyage.DateAller.ToShortDateString() + "', '" +
                                                                            voyage.DateRetour.ToShortDateString() + "', '" +
                                                                            voyage.PlacesDisponible + "', '" +
                                                                            voyage.TarifToutCompris + "', '" +
                                                                            voyage.Destination.DestinationId + "', '" +
                                                                            voyage.Agence.AgenceId + ");");
            return getLastIdentityId();
        }

        public void deleteVoyagesPerimes()
        {
            DBAccess.getInstance().execProcedure("deleteVoyagesPerimes");
        }

    }
}
