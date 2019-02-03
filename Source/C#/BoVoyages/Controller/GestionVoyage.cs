using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.Model;
using BoVoyages.View;

namespace BoVoyages.Controller
{
    /**
     * Class to manage all aspects of voyages.
     */

    public class GestionVoyage
    {
        public GestionVoyage() {}

        // Add Voyages.
        // Not yet implemented.
        public void addVoyages(Menu menu)
        {
            menu.display(">>>>>>>>>>   PAS ENCORE IMPLEMENTE !");
        }

        // Delete expired Voyages.
        public void deleteVoyagesPerimes(Menu menu)
        {
            new Model.Voyage().deleteVoyagesPerimes();
        }

    }
}
