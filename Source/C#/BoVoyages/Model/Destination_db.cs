using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    /**
     * This class provides uses the DBAccess instance to select, update, delete and insert to the Destinations database table.
     */

    class Destination_db : Table_db
    {
        private string table = "Destinations";

        public Destination_db() { }

        // Get the Destinations information for a particular destinationId
        public Destination getDestination(int destinationId)
        {
            Destination destination = new Destination();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where destinationId = " + destinationId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                destination = getDestination(row);
            }

            return destination;
        }

        // Get a list of the Destinations with where key=value
        public List<Destination> getDestinations(string key, string value)
        {
            List<Destination> destinations = new List<Destination>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                destinations.Add(getDestination(row));
            }

            return destinations;
        }

        // Get a list of all the Destinations.
        public List<Destination> getDestinations()
        {
            List<Destination> destinations = new List<Destination>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                destinations.Add(getDestination(row));
            }
            return destinations;
        }

        // Parse an Destinations from a data row.
        Destination getDestination(DataRow row)
        {
            return new Destination(int.Parse(row["destinationId"].ToString()),
                                   row["continent"].ToString(),
                                   row["pays"].ToString(),
                                   row["region"].ToString(),
                                   row["description"].ToString()
                                   );
        }

        // Update a Destinations object using the parameters change and condition.
        public int updateDestination(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        // Delete a Destinations object with a particular id.
        public int deleteDestination(int destinationId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where destinationId = " + destinationId + ";"); ;
        }

        // Insert a new Destinations object to the database table.
        public int insertDestination(Destination destination)
        {
            return DBAccess.getInstance().execNonQuery("insert into " + table + " (destinationId, continent, pays, region, description) values ('" +
                                                                                 destination.DestinationId + "', '" +
                                                                                 destination.Continent + "', '" +
                                                                                 destination.Pays + "', '" +
                                                                                 destination.Region + "', '" +
                                                                                 destination.Description + ");");
        }

    }
}
