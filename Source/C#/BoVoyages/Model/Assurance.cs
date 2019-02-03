using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    /**
     * Datastructure class.
     * Provides data attributes for this classs and via a facade pattern calls the appropirate database methods.
     */

    public class Assurance : Table
    {
        // enum for the type of Assurance and the possible values.
        public const string ANNULATION = "ANNULATION";
        public const string BAGGAGE = "BAGGAGE";
        public const string RAPATRIEMENT = "RAPATRIEMENT";
        public enum ASSURANCE_TYPE { ANNULATION, BAGGAGE, RAPATRIEMENT };

        private Assurance_db assurance_db = new Assurance_db();
        private int assuranceId;
        private ASSURANCE_TYPE type;
        private string nom;
        private string description;

        public Assurance() {}

        public Assurance(int assuranceId, ASSURANCE_TYPE type, string nom, string description)
        {
            this.assuranceId = assuranceId;
            this.type = type;
            this.nom = nom;
            this.description = description;
        }

        public int AssuranceId { get => assuranceId; set => assuranceId = value; }
        public ASSURANCE_TYPE Type { get => type; set => type = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Description { get => description; set => description = value; }

        public override void startTransaction()
        {
            assurance_db.startTransaction();
        }

        public override int endTransaction(bool commit)
        {
            return assurance_db.endTransaction(commit);
        }

        public Assurance getAssurance(int assuranceId)
        {
            return assurance_db.getAssurance(assuranceId);
        }

        public List<Assurance> getAssurances(string key, string value)
        {
            return assurance_db.getAssurances(key, value);
        }

        public List<Assurance> getAssurances()
        {
            return assurance_db.getAssurances();
        }

        public List<DossierReservation> getDossiersForAssurance(int assuranceId)
        {
            return assurance_db.getDossiersForAssurance(assuranceId);
        }

        public int updateAssurance(string change, string condition)
        {
            return assurance_db.updateAssurance(change, condition);
        }

        public int deleteAssurance(int assuranceId)
        {
            return assurance_db.deleteAssurance(assuranceId);
        }

        public int insertAssurance(Assurance assurance)
        {
            return assurance_db.insertAssurance(assurance);
        }

        public double getPrixAssurancePourcentage()
        {
            return assurance_db.getPrixAssurancePourcentage();
        }

        // ToString method for debug purposes.
        public override string ToString()
        {
            string ass = "";

            ass += "AssuranceId = " + AssuranceId + "\n";
            ass += "Type = " + Type.ToString() + "\n";
            ass += "Nom = " + Nom + "\n";
            ass += "Description = " + Description + "\n";

            return ass;
        }

    }
}
