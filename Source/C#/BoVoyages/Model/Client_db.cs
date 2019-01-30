using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class Client_db
    {
        public Client_db()
        {
        }

        public Client getClient(int clientId)
        {
            Client client = new Client();
            DataSet ds = DBAccess.getInstance().execSelect("select * from aClient where ClientId = " + clientId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                client = getClient(row);
            }

            return client;
        }

        public List<Client> getClients(string key, string value)
        {
            List<Client> clients = new List<Client>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from aClient where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                clients.Add(getClient(row));
            }

            return clients;
        }

        public List<Client> getClients()
        {
            List<Client> clients = new List<Client>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from aClient;");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                clients.Add(getClient(row));
            }
            return clients;
        }

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

        public int updateClient(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update aClient set " + change + " where " + condition + ";");
        }

        public int deleteClient(int clientId)
        {
            new DossierReservation_db().deleteDossiersForClient(clientId);
            return DBAccess.getInstance().execNonQuery("delete from aClient where clientId = " + clientId+ ";");
        }

        public int insertClient(Client client)
        {
            return DBAccess.getInstance().execNonQuery("insert into aClient (civilite, nom, prenom, adresse, telephone, DOB, email) values ('" +
                                                                             client.Civilite + "', '" +
                                                                             client.Nom + "', '" +
                                                                             client.Prenom + "', '" +
                                                                             client.Adresse + "', '" +
                                                                             client.Telephone + "', '" +
                                                                             client.DateNaissance.ToShortDateString() + "', " +
                                                                             client.Email + ");");
        }
    }
}
