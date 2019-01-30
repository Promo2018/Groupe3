using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    public class EtatDossierReservation 
    {
        public const string ENATTENTE = "ENATTENTE";
        public const string ENCOURS = "ENCOURS";
        public const string REFUSEE = "REFUSEE";
        public const string ACCEPTEE = "ACCEPTEE";
        public enum ETAT_RESERVATION { ENATTENTE, ENCOURS, REFUSEE, ACCEPTEE };
        private ETAT_RESERVATION status = ETAT_RESERVATION.ENATTENTE;

        public EtatDossierReservation()
        {
        }

        public EtatDossierReservation(ETAT_RESERVATION status)
        {
            this.status = status;
        }

        public EtatDossierReservation(string etat)
        {
            switch(etat.ToUpper())
            {
                case ENATTENTE:
                    this.status = ETAT_RESERVATION.ENATTENTE;
                    break;
                case ENCOURS:
                    this.status = ETAT_RESERVATION.ENCOURS;
                    break;
                case REFUSEE:
                    this.status = ETAT_RESERVATION.REFUSEE;
                    break;
                case ACCEPTEE:
                    this.status = ETAT_RESERVATION.ACCEPTEE;
                    break;
            }
        }

        public string getEtatReservation()
        {
            return status.ToString();
        }

        public void setEtatReservation(ETAT_RESERVATION status)
        {
            this.status = status;
        }

        public override string ToString()
        {
            return status.ToString();
        }

    }
}
