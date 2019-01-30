using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    public class RaisonAnnulationDossier
    {
        public const string NONE = "NONE";
        public const string CLIENT = "CLIENT";
        public const string PLACESINSUFFISANTES = "PLACESINSUFFISANTES";
        public enum RAISON_ANNULATION { NONE, CLIENT, PLACESINSUFFISANTES };
        RAISON_ANNULATION reason = RAISON_ANNULATION.NONE;

        public RaisonAnnulationDossier()
        {
        }

        public RaisonAnnulationDossier(RAISON_ANNULATION reason)
        {
            this.reason = reason;
        }

        public RaisonAnnulationDossier(string reason)
        {
            switch (reason.ToUpper())
            {
                case CLIENT:
                    this.reason = RAISON_ANNULATION.CLIENT;
                    break;
                case PLACESINSUFFISANTES:
                    this.reason = RAISON_ANNULATION.PLACESINSUFFISANTES;
                    break;
                default:
                    this.reason = RAISON_ANNULATION.NONE;
                    break;
            }
        }

        public string getRaisonAnnulationDossier()
        {
            return reason.ToString();
        }

        public void setRaisonAnnulationDossier(RAISON_ANNULATION reason)
        {
            this.reason = reason;
        }

        public override string ToString()
        {
            return reason.ToString();
        }

    }
}
