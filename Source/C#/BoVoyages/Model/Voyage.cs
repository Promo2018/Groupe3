using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BoVoyages.Model
{
    public class Voyage : Table
    {
        private Voyage_db voyage_db = new Voyage_db();
        private int voyageId;
        private DateTime dateAller;
        private DateTime dateRetour;
        private int placesDisponible;
        private float tarifToutCompris;
        private Destination destination;
        private AgenceVoyage agence;

        public Voyage() { }

        public Voyage(int voyageId, DateTime dateAller, DateTime dateRetour, int placesDisponible, float tarifToutCompris, Destination destination, AgenceVoyage agence)
        {
            this.VoyageId = voyageId;
            this.DateAller = dateAller;
            this.DateRetour = dateRetour;
            this.PlacesDisponible = placesDisponible;
            this.TarifToutCompris = tarifToutCompris;
            this.Destination = destination;
            this.Agence = agence;
        }

        public int VoyageId { get => voyageId; set => voyageId = value; }
        public DateTime DateAller { get => dateAller; set => dateAller = value; }
        public DateTime DateRetour { get => dateRetour; set => dateRetour = value; }
        public int PlacesDisponible { get => placesDisponible; set => placesDisponible = value; }
        public float TarifToutCompris { get => tarifToutCompris; set => tarifToutCompris = value; }
        public Destination Destination { get => destination; set => destination = value; }
        public AgenceVoyage Agence { get => agence; set => agence = value; }

        public override void startTransaction()
        {
            voyage_db.startTransaction();
        }

        public override int endTransaction(bool commit)
        {
            return voyage_db.endTransaction(commit);
        }

        public Voyage getVoyage(int voyageId)
        {
            return voyage_db.getVoyage(voyageId);
        }

        public List<Voyage> getVoyages(string key, string value)
        {
            return voyage_db.getVoyages(key, value);
        }

        public List<Voyage> getVoyages()
        {
            return voyage_db.getVoyages();
        }

        public int updateVoyage(string change, string condition)
        {
            return voyage_db.updateVoyage(change, condition);
        }

        public int deleteVoyage(int voyageId)
        {
            return voyage_db.deleteVoyage(voyageId);
        }

        public int insertVoyage(Voyage voyage)
        {
            return voyage_db.insertVoyage(voyage);
        }

        public bool arePlacesAvailable(int voyageId, int places)
        {
            bool available = true;
            return available;
        }

        public override string ToString()
        {
            string outString = "";

            outString += "Destination = " + Destination.Pays + "\n";
            outString += "DateAller = " + DateAller.ToShortDateString() + "\n";
            outString += "DateRetour = " + DateRetour.ToShortDateString() + "\n";
            outString += "PlacesDisponible = " + PlacesDisponible + "\n";
            outString += "TarifToutCompris = " + TarifToutCompris + "\n";

            return outString;
        }

        public void deleteVoyagesPerimes()
        {
            voyage_db.deleteVoyagesPerimes();
        }

    }
}
