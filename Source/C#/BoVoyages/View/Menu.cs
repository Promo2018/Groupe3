using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.View
{
    public abstract class Menu
    {
        protected int nombreOptions = 0;
        private int selection = 0;

        public abstract void affiche();

        public abstract Menu execute(int sel);

        private bool validerSelection()
        {
            return (selection <= nombreOptions);
        }


        public int lire()
        {
            try
            {
                selection = Int32.Parse(System.Console.ReadLine());
                if(!validerSelection())
                {
                    System.Console.WriteLine("BoVoyages: Veuillez entrer un nombre entre 0 et " + nombreOptions);
                    lire();
                }
            }
            catch(FormatException fe)
            {
                System.Console.WriteLine("BoVoyages: Veuillez entrer un nombre entre 0 et " + nombreOptions);
                selection = lire();
            }
            return selection;
        }

    }
}
