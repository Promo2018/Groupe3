using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.View;
using BoVoyages.Controlleur;

namespace BoVoyages
{
    class Program
    {
        static int tries = 0;
        static int selection = 0;

        static void Main(string[] args)
        {
            System.Console.WriteLine("BoVoyages : Bienvenue.");
            showMenu(new MenuPrincipal());
/*
            new Model.Participant().deleteParticipant(7060);
            List<Model.Participant> drs = new Model.Participant().getParticipants();
            foreach (Model.Participant dr in drs)
            {
                if(dr.ParticipantId == 7005)
                    System.Console.WriteLine(dr.ToString());
            }

            //            System.Console.WriteLine("BoVoyages : Inserting new Client");
            //            Model.Client_db client = new Model.Client_db(0, "Mrs", "Ann", "Devon", "Ap #240-8152 Laoreet St.", "WX3 6FW", "London", "UK", "(171) 555-0297", "dolor.Nulla@lacusvarius.net", DateTime.Today, 5036);
            //            new Model.Client().insertClient(client);
            //            System.Console.WriteLine("BoVoyages : Inserting new Client - done");

            //            Model.DBAccess.getInstance().execNonQuery("begin transaction;");
            //            System.Console.WriteLine(client.GetClient(105).ToString());
            //            client.deleteClient(105);
            //            Model.DBAccess.getInstance().execNonQuery("rollback;");

            /*            if (isLoginOK()) 
                        {
                            showMenu(new MenuCommercial());
                        } */
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

        static bool isLoginOK()
        {
            bool loginOK = true;
            string login;
            string mdp;
            System.Console.WriteLine("BoVoyages : Login:");
            login = System.Console.ReadLine();
            System.Console.WriteLine("BoVoyages : Mot de passe:");
            mdp = System.Console.ReadLine();
            if(!GestionLogin.login(login, mdp))
            {
                if((tries++) > 2)
                {
                    System.Console.WriteLine("BoVoyages : Login Echoué. Au revoir.");
                    System.Console.ReadKey();
                    loginOK = false;
                } else
                {
                    System.Console.WriteLine("BoVoyages : Login et mot de passe invalide, merci de réessayer");
                    loginOK = isLoginOK();
                }
                
            }
            return loginOK;
        }
    }
}
