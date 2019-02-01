using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    public class Personne
    {
        private string civilite;
        private string nom;
        private string prenom;
        private string adresse;
        private string telephone;
        private DateTime dateNaissance;

        public Personne() { }

        public Personne(string civilite, string nom, string prenom, string adresse, string telephone, DateTime dateNaissance)
        {
            this.Civilite = civilite;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Adresse = adresse;
            this.Telephone = telephone;
            this.DateNaissance = dateNaissance;
        }

        public string Civilite { get => civilite; set => civilite = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }

        public int Age()
        {
            return  DateTime.Today.Year - dateNaissance.Year;
        }
    }
}
