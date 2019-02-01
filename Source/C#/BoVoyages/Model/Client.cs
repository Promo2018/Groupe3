using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BoVoyages.Model
{
    public class Client : Personne
    {
        private Client_db client_Db = new Client_db();
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
            client_Db.startTransaction();
        }

        public override int endTransaction(bool commit)
        {
            return client_Db.endTransaction(commit);
        }

        public Client getClient(int clientId)
        {
            return client_Db.getClient(clientId);
        }

        public List<Client> getClients(string key, string value)
        {
            return client_Db.getClients(key, value);
        }

        public List<Client> getClients()
        {
            return client_Db.getClients();
        }

        public int updateClient(string change, string condition)
        {
            return client_Db.updateClient(change, condition);
        }

        public int deleteClient(int clientId)
        {
            return client_Db.deleteClient(clientId);
        }

        public int insertClient(Client client)
        {
            return client_Db.insertClient(client);
        }

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
