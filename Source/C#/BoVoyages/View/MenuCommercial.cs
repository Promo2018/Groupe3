using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.View
{
    public class MenuCommercial : Menu
    {
        private MenuVoyage menuVoyage = null;
        private MenuClient menuClient = null;

        public MenuCommercial()
        {
            menuVoyage = new MenuVoyage(this);
            menuClient = new MenuClient(this);
            nombreOptions = 2;
        }

        public override void affiche()
        {
            System.Console.WriteLine("\n\n*********************************************************************");
            System.Console.WriteLine("******   Menu Commercial   ******************************************");
            System.Console.WriteLine("BoVoyages : Sélectionnez une option dans la liste ci-dessous :");
            System.Console.WriteLine("BoVoyages :\t 1 - Gérer les voyages");
            System.Console.WriteLine("BoVoyages :\t 2 - Gérer les clients");
            System.Console.WriteLine("BoVoyages :\t 0 - Quitter");
        }

        public override Menu execute(int sel)
        {
            Menu menu = null;

            if(sel == 1)
            {
                menu = menuVoyage;
            } else if(sel == 2)
            {
                menu = menuClient;
            }

            return menu;
        }
    }
}
