using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    /**
     * This class provides uses the DBAccess instance to select, update, delete and insert to the aClient view and the Clients database table.
     * The aClient view contains information of the Clients and the Personnes database tables.
     */

    class Client_db : Personne_db
    {
        private string table = "aClient";

        public Client_db()
        {
        }

        // Get the aClient information for a particular clientId
        public Client getClient(int clientId)
        {
            Client client = null;
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where ClientId = " + clientId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                client = getClient(row);
            }

            return client;
        }

        // Get a list of the clients with where key=value
        public List<Client> getClients(string key, string value)
        {
            List<Client> clients = new List<Client>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                clients.Add(getClient(row));
            }

            return clients;
        }

        // Get a list of all the clients.
        public List<Client> getClients()
        {
            List<Client> clients = new List<Client>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                clients.Add(getClient(row));
            }
            return clients;
        }

        // Parse a Client from a data row.
        Client getClient(DataRow row)
        {
            return new Client(int.Parse(row["ClientId"].ToString()),
                                       row["civilite"].ToString(),
                                       row["nom"].ToString(),
                                       row["prenom"].ToString(),
                                       row["adresse"].ToString(),
                                       row["telephone"].ToString(),
                                       ((DateTime)row["dateNaissance"]),
                                       row["email"].ToString());
        }

        // Update a Clients object using the parameters change and condition.
        public int updateClient(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        // Delete a Clients object with a particular id. Also delete any Dossiers that this Client has reserved.
        public int deleteClient(int clientId)
        {
            new DossierReservation_db().deleteDossiersForClient(clientId);
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where clientId = " + clientId+ ";");
        }

        // Insert a new Client object to the database table.
        public int insertClient(Client client)
        {
            int id = insertPersonne(client);
            DBAccess.getInstance().execNonQuery("insert into Clients (email, personneId) values ('" +
                                                                             client.Email + "', " +
                                                                             id + ");");
            id = getLastIdentityId();
            return id;
        }

        // Returns true if a client with the provided id exists in the database.
        public bool exists(int clientId)
        {
            return (getClient(clientId) != null);
        }

    }
}
