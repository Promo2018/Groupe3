using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.Controller;

namespace BoVoyages.View
{
    /**
     * MenuPrincipal class.
     * Provides menu options and what to call when an option is selected.
     */

    class MenuVoyage : Menu
    {
        Menu previousMenu;
        private Voyage voyage = new Voyage();

        public MenuVoyage(Menu previousMenu)
        {
            nombreOptions = 3;
            this.previousMenu = previousMenu;
        }

        // Display Menu options.
        public override void affiche()
        {
            System.Console.Clear();
            System.Console.WriteLine("\n\n*********************************************************************");
            System.Console.WriteLine("******   Menu Voyage   **********************************************");
            System.Console.WriteLine("BoVoyages : Sélectionnez une option dans la liste ci-dessous :");
            System.Console.WriteLine("BoVoyages :\t 1 - Liste de voyages disponible");
            System.Console.WriteLine("BoVoyages :\t 2 - Reserver un Voyage");
            System.Console.WriteLine("BoVoyages :\t 3 - Supprimer un Voyage Reservation");
            System.Console.WriteLine("BoVoyages :\t 0 - Quitter");
        }

        // Execute requested option.
        public override Menu execute(int sel)
        {
            Menu menu = this;

            if (sel == 1)
            {
                string heading = "Voyage Id" + "\t" + "Date-Aller" + "\t" + "Date-Retour" + "\t" + "Places Disponible" + "\t" + "Tarif Tout Compris" + "\t" + "Pays";
                List<string> items = new List<string>();
                foreach (Model.Voyage voyage in voyage.getVoyages())
                {
                    items.Add(voyage.VoyageId + "\t\t" + voyage.DateAller.ToShortDateString() + "\t" + voyage.DateRetour.ToShortDateString() + "\t" + voyage.PlacesDisponible + "\t\t\t" + voyage.TarifToutCompris.ToString("C") + "\t\t" + voyage.Destination.Pays);
                }
                displayListToConsole(heading, items);
            }
            else if (sel == 2)
            {

                voyage.createReservation(this);
                System.Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                System.Console.ReadKey();
            }
            else if (sel == 3)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Supprimer un Voyage Reservation");
                voyage.deleteReservation(this);
                System.Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                System.Console.ReadKey();
            }
            else if (sel == 0)
            {
                menu = previousMenu;
            }

            return menu;
        }

    }
}
