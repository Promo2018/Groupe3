using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class Destination_db
    {
        public Destination_db() { }

        public Destination getDestination(int destinationId)
        {
            Destination destination = new Destination();
            DataSet ds = DBAccess.getInstance().execSelect("select * from Destination where destinationId = " + destinationId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                destination = getDestination(row);
            }

            return destination;
        }

        public List<Destination> getDestinations(string key, string value)
        {
            List<Destination> destinations = new List<Destination>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from Destination where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                destinations.Add(getDestination(row));
            }

            return destinations;
        }

        public List<Destination> getDestinations()
        {
            List<Destination> destinations = new List<Destination>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from Destination;");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                destinations.Add(getDestination(row));
            }
            return destinations;
        }

        Destination getDestination(DataRow row)
        {
            return new Destination(int.Parse(row["destinationId"].ToString()),
                                   row["continent"].ToString(),
                                   row["pays"].ToString(),
                                   row["region"].ToString(),
                                   row["description"].ToString()
                                   );
        }

        public int updateDestination(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update Destination set " + change + " where " + condition + ";");
        }

        public int deleteDestination(int destinationId)
        {
            return DBAccess.getInstance().execNonQuery("delete from Destination where destinationId = " + destinationId + ";"); ;
        }

        public int insertDestination(Destination destination)
        {
            return DBAccess.getInstance().execNonQuery("insert into Destination (destinationId, continent, pays, region, description) values ('" +
                                                                                 destination.DestinationId + "', '" +
                                                                                 destination.Continent + "', '" +
                                                                                 destination.Pays + "', '" +
                                                                                 destination.Region + "', '" +
                                                                                 destination.Description + ");");
        }

    }
}
