using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.Model;

namespace BoVoyages.Controller
{
    /*Classe qui permet de gérer les clients en affichant une liste complète ou le résultat d'une recherche. 
    L'ajout et la suppression de clients seront gérés au moment de leur achat d'un voyage sur le site internet.*/

    class GestionClient
    {
        //Afficher une liste de tous les clients

        public void listerClients()
        {
            Console.WriteLine(">>>>>>>>>>   PAS ENCORE IMPLEMENTE !");
        }

        //Afficher seulement les clients demandés, en fonction d'une entrée et dans une colonne au choix
        //Cette recherche pourra servir ultérieurement à cibler les envois de mails et de publicité


        //Pas encore implémenté. Cette fonctionnalité fera une synthèse des questionnaires quand ceux-ci seront disponibles.
        public void syntheseMensuelle()
        {
            Console.WriteLine(">>>>>>>>>>   PAS ENCORE IMPLEMENTE !");
        }

        // Le questionnaire de satisfaction sera vraiment envoyé dans une prochaine version
        public void envoyerQuestionnaire()
        {
            Console.WriteLine("Questionnaire envoyé aux clients.");
        }

        // La promotion des voyages encore disponibles sera vraiment envoyé dans une prochaine version
        public void envoyerPub()
        {
            Console.WriteLine("Promotion des voyages encore disponibles envoyée aux clients.");
        }
    }
}
