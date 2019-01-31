using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.Controller;

namespace BoVoyages.View
{
    class MenuVoyage : Menu
    {
        Menu previousMenu;
        private Voyage voyage = new Voyage();

        public MenuVoyage(Menu previousMenu)
        {
            nombreOptions = 3;
            this.previousMenu = previousMenu;
        }

        public override void affiche()
        {
            System.Console.WriteLine("\n\n*********************************************************************");
            System.Console.WriteLine("******   Menu Voyage   **********************************************");
            System.Console.WriteLine("BoVoyages : Sélectionnez une option dans la liste ci-dessous :");
            System.Console.WriteLine("BoVoyages :\t 1 - Liste de voyages disponible");
            System.Console.WriteLine("BoVoyages :\t 2 - Reserver un Voyage");
            System.Console.WriteLine("BoVoyages :\t 3 - Supprimer un Voyage Reservation");
            System.Console.WriteLine("BoVoyages :\t 0 - Quitter");
        }

        public override Menu execute(int sel)
        {
            Menu menu = this;

            if (sel == 1)
            {
                System.Console.WriteLine("Voyage Id" + "\t" + "Date-Aller" + "\t" + "Date-Retour" + "\t" + "Places Disponible" + "\t" + "Tarif Tout Compris" + "\t" + "Pays");
                foreach (Model.Voyage voyage in voyage.getVoyages())
                {
                    System.Console.WriteLine(voyage.VoyageId + "\t" + voyage.DateAller.ToShortDateString() + "\t" + voyage.DateRetour.ToShortDateString() + "\t" + voyage.PlacesDisponible + "\t\t\t" + voyage.TarifToutCompris + "\t\t\t" + voyage.Destination.Pays);
                }
            }
            else if (sel == 2)
            {

                voyage.createReservation(this);
            }
            else if (sel == 3)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Supprimer un Voyage Reservation");
            }
            else if (sel == 0)
            {
                menu = previousMenu;
            }

            return menu;
        }

    }
}
