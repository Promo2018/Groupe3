using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.Controller;

namespace BoVoyages.View
{
    public class MenuVoyage : Menu
    {
        private GestionVoyage gestionVoyage = new GestionVoyage();
        Menu previousMenu;

        public MenuVoyage(Menu previousMenu)
        {
            nombreOptions = 4;
            this.previousMenu = previousMenu;
        }

        public override void affiche()
        {
            System.Console.WriteLine("\n\n*********************************************************************");
            System.Console.WriteLine("******   Menu Voyage   **********************************************");
            System.Console.WriteLine("BoVoyages : Sélectionnez une option dans la liste ci-dessous :");
            System.Console.WriteLine("BoVoyages :\t 1 - Liste de voyages disponible");
            System.Console.WriteLine("BoVoyages :\t 2 - Supprimer les voyages expirés");
            System.Console.WriteLine("BoVoyages :\t 3 - Ajouter les voyages");
            System.Console.WriteLine("BoVoyages :\t 4 - Enregistrer");
            System.Console.WriteLine("BoVoyages :\t 0 - Quitter");
        }

        public override Menu execute(int sel)
        {
            Menu menu = this;

            if (sel == 1)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Liste de voyages disponible");
                gestionVoyage.listVoyages();
            }
            else if (sel == 2)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Supprimer les voyages expirés");
                gestionVoyage.supprimerVoyagesExpires();
            }
            else if (sel == 3)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Ajouter les voyages");
                gestionVoyage.ajouterVoyages();
            }
            else if (sel == 4)
            {
                System.Console.WriteLine("BoVoyages >>>>>>>>> - Enregistrer");
                gestionVoyage.enregistrer();
            }
            else if (sel == 0)
            {
                menu = previousMenu;
            }

            return menu;
        }

    }
}
