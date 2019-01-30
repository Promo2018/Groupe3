using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.Controller;

namespace BoVoyages.View
{
    public class MenuClient : Menu
    {
        private GestionClient gestionClient = new GestionClient();
        private Menu previousMenu;

        public MenuClient(Menu previousMenu)
        {
            this.previousMenu = previousMenu;
            nombreOptions = 4;
        }

        public override void affiche()
        {
            System.Console.WriteLine("\n\n*********************************************************************");
            System.Console.WriteLine("******   Menu Client   **********************************************");
            System.Console.WriteLine("BoVoyages : Sélectionnez une option dans la liste ci-dessous :");
            System.Console.WriteLine("BoVoyages :\t 1 - Lister les clients");
            System.Console.WriteLine("BoVoyages :\t 2 - Faire une synthèse mensuelle");
            System.Console.WriteLine("BoVoyages :\t 3 - Promouvoir les voyages disponibles par mail");
            System.Console.WriteLine("BoVoyages :\t 4 - Envoyer les questionnaire la satisfaction par mail");
            System.Console.WriteLine("BoVoyages :\t 0 - Quitter");
        }

        public override Menu execute(int sel)
        {
            Menu menu = this;

            if (sel == 1)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Lister les clients");
                gestionClient.listerClients();
            }
            else if (sel == 2)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Faire une synthèse mensuelle");
                gestionClient.syntheseMensuelle();
            }
            else if (sel == 3)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Promouvoir les voyages disponibles par mail");
                gestionClient.envoyerPub();
            }
            else if (sel == 4)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Envoyer les questionnaire la satisfaction par mail");
                gestionClient.envoyerQuestionnaire();
            }
            else if (sel == 0)
            {
                menu = previousMenu;
            }

            return menu;
        }

    }
}
