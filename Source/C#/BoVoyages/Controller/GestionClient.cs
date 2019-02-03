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
     * Class to manage all aspects of interaction with clients
     */
    
    class GestionClient
    {
        public GestionClient() {}

        // List all clients.
        // Not yet implemented.
        public void getClients(Menu menu)
        {
            menu.display(">>>>>>>>>>   PAS ENCORE IMPLEMENTE !");
        }

        // Perform a monthly Synthes of client questionaires.
        // Not yet implemented.
        public void syntheseMensuelle(Menu menu)
        {
            menu.display(">>>>>>>>>>   PAS ENCORE IMPLEMENTE !");
        }

        // Send questionaires to clients.
        // Not yet implemented.
        public void sendQuestionnaire(Menu menu)
        {
            menu.display("Questionnaire envoyé aux clients.");
        }

        // Send advertising to clients.
        // Not yet implemented.
        public void sendPub(Menu menu)
        {
            menu.display("Promotion des voyages encore disponibles envoyée aux clients.");
        }
    }
}
