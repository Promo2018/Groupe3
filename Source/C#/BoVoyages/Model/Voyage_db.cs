using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    /**
     * This class provides uses the DBAccess instance to select, update, delete and insert to the allVoyages view and the Voyages database table.
     * The allVoyages view contains information from the Voyages table and the Destinations tables.
     */

    class Voyage_db : Table_db
    {
        private const string table = "Voyages";
        private const string view = "allVoyages";

        public Voyage_db() { }

        // Get the allVoyages information for a particular voyageId
        public Voyage getVoyage(int voyageId)
        {
            Voyage voyage = null;
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + view + " where voyageId = " + voyageId + " order by dateAller;");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                voyage = getVoyage(row);
            }

            return voyage;
        }

        // Get a list of the allVoyages with where key=value
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

        // Get a list of the allVoyages.
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

        // Parse a allVoyages from a data row.
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

        // Update a Voyages object using the parameters change and condition.
        public int updateVoyage(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        // Delete a Voyages object with a particular id.
        public int deleteVoyage(int voyageId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where voyageId = " + voyageId + ";"); ;
        }

        // Insert a new Voyages object to the database table.
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

        // Method to delete voyages where the start date is before today.
        // This method uses the stored procedure "deleteVoyagesPerimes".
        public void deleteVoyagesPerimes()
        {
            DBAccess.getInstance().execProcedure("deleteVoyagesPerimes");
        }

        // Checks if the number of places are available for the given voyage.
        public bool arePlacesAvailable(int voyageId, int places)
        {
            return (getVoyage(voyageId).PlacesDisponible >= places);
        }

        // Returns true if the Voyage with the given Id exists.
        public bool exists(int voyageId)
        {
            return (getVoyage(voyageId) != null);
        }

    }
}
