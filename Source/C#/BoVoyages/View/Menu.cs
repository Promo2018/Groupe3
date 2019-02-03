using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.View
{
    /**
     * Abstract base class for all Console menu classes.
     * Provides data attributes for the number of options in a Menu and the selection entered.
     * Takes care of checking that the values entered are in the correct format and are valid for the particular menu.
     */

    public abstract class Menu
    {
        protected int nombreOptions = 0;
        private int selection = 0;

        public abstract Menu execute(int sel);

        public abstract void affiche();

        // Is the entered selection in the expected range.
        private bool validerSelection()
        {
            return (selection <= nombreOptions);
        }


        // Read the values entered and check that they are in the correct format and are valid for the particular menu.
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

        // Displays a basic message to the console.
        public void display(string displayString)
        {
            Console.WriteLine(displayString);
        }

        // Displays a list to the console. Taking care of Heading and paging constraints.
        public void displayListToConsole(string heading, List<string> items)
        {
            int next = 0;
            int displays = 0;
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(heading);
                for (int j = 0; j < 27; j++)
                {
                    next = (j + (displays * 27));
                    if (next < items.Count)
                        Console.WriteLine(items[next]);
                    if (j == 26)
                    {
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                        Console.ReadKey();
                        System.Console.Clear();
                        i = i + 27;
                    }
                }
                displays++;
            }
        }

        // Displays an important message. Usually at the end of a particular operation.
        public void displayFinal(string displayString)
        {
            Console.WriteLine("**********************************************************************************");
            Console.WriteLine("==>   " + displayString);
            Console.WriteLine("**********************************************************************************");
        }

        // Reads a string from the console
        public string readString()
        {
            return Console.ReadLine();
        }

        // Reads an integer from the console. If a non-numeric character is entered, call recursively.
        public int readInt()
        {
            int input = 0;
            try
            {
                input = Int32.Parse(System.Console.ReadLine());
            }
            catch (FormatException fe)
            {
                System.Console.WriteLine("BoVoyages: Veuillez entrer un chiffre");
                input = readInt();
            }
            return input;
        }

    }
}
