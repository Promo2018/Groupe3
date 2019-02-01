﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.View
{
    class MenuPrincipal : Menu
    {
        private MenuVoyage menuVoyage = null;
        private MenuCommercial menuCommercial = null;

        public MenuPrincipal()
        {
            menuVoyage = new MenuVoyage(this);
            menuCommercial = new MenuCommercial(this);
            nombreOptions = 2;
        }

        public override void affiche()
        {
            System.Console.WriteLine("\n\n*********************************************************************");
            System.Console.WriteLine("******   Menu Principal   ******************************************");
            System.Console.WriteLine("BoVoyages : Sélectionnez une option dans la liste ci-dessous :");
            System.Console.WriteLine("BoVoyages :\t 1 - Menu Voyage");
            System.Console.WriteLine("BoVoyages :\t 2 - Menu Commercial");
            System.Console.WriteLine("BoVoyages :\t 0 - Quitter");
        }

        public override Menu execute(int sel)
        {
            Menu menu = null;

            if (sel == 1)
            {
                menu = menuVoyage;
            }
            else if (sel == 2)
            {
                menu = menuCommercial;
            }

            return menu;
        }
    }
}
