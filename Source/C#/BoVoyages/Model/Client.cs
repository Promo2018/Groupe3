using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BoVoyages.Model
{
    /**
     * Datastructure class.
     * Provides data attributes for this classs and via a facade pattern calls the appropirate database methods.
     */

    public class Client : Personne
    {
        private Client_db client_db = new Client_db();
        private int clientId;
        private string email;

        public Client() {}

        public Client(int clientId,
                      string civilite,
                      string nom,
                      string prenom,
                      string adresse,
                      string telephone,
                      DateTime dateNaissance,
                      string email) : base(civilite, nom, prenom, adresse, telephone, dateNaissance)
        {
            this.clientId = clientId;
            this.Email = email;
        }

        public int ClientId { get => clientId; set => clientId = value; }
        public string Email { get => email; set => email = value; }

        public override void startTransaction()
        {
            client_db.startTransaction();
        }

        public override int endTransaction(bool commit)
        {
            return client_db.endTransaction(commit);
        }

        public Client getClient(int clientId)
        {
            return client_db.getClient(clientId);
        }

        public List<Client> getClients(string key, string value)
        {
            return client_db.getClients(key, value);
        }

        public List<Client> getClients()
        {
            return client_db.getClients();
        }

        public int updateClient(string change, string condition)
        {
            return client_db.updateClient(change, condition);
        }

        public int deleteClient(int clientId)
        {
            return client_db.deleteClient(clientId);
        }

        public int insertClient(Client client)
        {
            return client_db.insertClient(client);
        }

        public bool exists(int clientId)
        {
            return client_db.exists(clientId);
        }

        // ToString method for debug purposes.
        public override string ToString()
        {
            string cl = "";

            cl += "ClientId = " + clientId + "\n";
            cl += "Civilite = " + Civilite  + "\n";
            cl += "Nom = " + Nom + "\n";
            cl += "Prenom = " + Prenom + "\n";
            cl += "Adresse = " + Adresse + "\n";
            cl += "Telephone = " + Telephone + "\n";
            cl += "Date of Birth = " + DateNaissance.ToString() + "\n";
            cl += "Age = " + Age() + "\n";
            cl += "eMail = " + Email + "\n";

            return cl;
        }

    }
}
