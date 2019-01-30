using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BoVoyages.Model
{
    public class Voyage
    {
        private Voyage_db voyage_db = new Voyage_db();
        private int voyageId;
        private DateTime dateAller;
        private DateTime dateRetour;
        private int placesDisponible;
        private float tarifToutCompris;
        private int destinationId;
        private int agenceId;

        public Voyage() { }

        public Voyage(int voyageId, DateTime dateAller, DateTime dateRetour, int placesDisponible, float tarifToutCompris, int destinationId, int agenceId)
        {
            this.VoyageId = voyageId;
            this.DateAller = dateAller;
            this.DateRetour = dateRetour;
            this.PlacesDisponible = placesDisponible;
            this.TarifToutCompris = tarifToutCompris;
            this.DestinationId = destinationId;
            this.AgenceId = agenceId;
        }

        public int VoyageId { get => voyageId; set => voyageId = value; }
        public DateTime DateAller { get => dateAller; set => dateAller = value; }
        public DateTime DateRetour { get => dateRetour; set => dateRetour = value; }
        public int PlacesDisponible { get => placesDisponible; set => placesDisponible = value; }
        public float TarifToutCompris { get => tarifToutCompris; set => tarifToutCompris = value; }
        public int DestinationId { get => destinationId; set => destinationId = value; }
        public int AgenceId { get => agenceId; set => agenceId = value; }

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

    }
}
