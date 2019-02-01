using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.View;
using BoVoyages.Controller;

namespace BoVoyages
{
    class Program
    {
        static int selection = 0;

        static void Main(string[] args)
        {
            System.Console.WriteLine("BoVoyages : Bienvenue.");
            showMenu(new MenuPrincipal());
            System.Console.WriteLine("BoVoyages : Au revoir.");
            System.Console.ReadKey();
        }

        static void showMenu(Menu menu)
        {
            menu.affiche();
            selection = menu.lire();
            menu = menu.execute(selection);
            if(menu != null)
            {
                showMenu(menu);
            }
        }

    }
}
