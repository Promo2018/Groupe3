using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyages.Model;
using BoVoyages.View;

namespace BoVoyages.Controller
{
    public class Voyage
    {
        /**
         * Class to manage all aspects of voyage reservations.
         */

        public Voyage() {}

        // Get a list of all voyages
        public List<Model.Voyage> getVoyages()
        {
            return new Model.Voyage().getVoyages(); ;
        }

        // Delete a voyage reservation. 
        public void deleteReservation(Menu menu)
        {
            Model.DossierReservation reservation = new DossierReservation();
            menu.display("Entrer le numéro de Dossier que vous voulez supprimer");
            int dossierId = menu.readInt();
            int ret = reservation.deleteDossier(dossierId);
            if(ret == 1)
            {
                menu.display("Dossier avec le numéro " + dossierId + " était bien supprimé;");
            } else
            {
                menu.display("Dossier avec le numéro " + dossierId + " n'existe pas.");
            }
        }

        // Create a voyage reservation. 
        // If the voyage exists:
        // 1. Get number of participants.
        // 2. If the person resering is not yet a client, create client information.
        // 3. Fill in information for each Participant.
        // 4. If assurance is requested, add it.
        // 5. Display total voyage price.
        // 6. Ask for confirmation, if yes commit otherwise abandon.
        public void createReservation(Menu menu)
        {
            Model.DossierReservation reservation = new DossierReservation();
            menu.display("Entrer le numéro de Voyage que vous voulez reservez");
            reservation.VoyageId = menu.readInt();
            Model.Voyage voyage = new Model.Voyage();
            if(voyage.exists(reservation.VoyageId))
            {
                menu.display("Entrer le numéro de participants");
                int participants = menu.readInt();
                if (new Model.Voyage().arePlacesAvailable(reservation.VoyageId, participants))
                {
                    reservation.startTransaction();
                    List<int> participandIds = new List<int>();
                    Client client = new Client();
                    menu.display("Entrer votre numéro de client si vous avez, sinon 0");
                    reservation.ClientId = menu.readInt();
                    bool clientExists = client.exists(reservation.ClientId);
                    if(!clientExists)
                    {
                        menu.display("Numéro de client " + reservation.ClientId + " n'existe pas.");
                        reservation.ClientId = 0;
                    }
                    if (reservation.ClientId == 0)
                    {
                        menu.display("Entrer l'information suivante pour créer votre profil:");
                        client = (Client)fillPersonne(menu, client);
                        menu.display("eMail:");
                        client.Email = menu.readString();
                        reservation.ClientId = client.insertClient(client);
                    }
                    menu.display("Entrer l'information pour chaque participant:");
                    for (int i = 0; i < participants; i++)
                    {
                        menu.display("----------------------------------------------------------------------------------");
                        menu.display("Pour participant " + (i + 1));
                        Participant participant = new Participant();
                        participant = (Participant)fillPersonne(menu, participant);
                        participandIds.Add(participant.insertParticipant(participant));
                    }

                    menu.display("-------------Fin d'information pour Participants----------------------------------");
                    menu.display("Entrer votre numéro de carte de credit");
                    reservation.NumeroCarteBancaire = menu.readString();
                    int dossierId = reservation.DossierId = reservation.insertDossier(reservation);
                    reservation.insertDossierParticipants(dossierId, participandIds);

                    menu.display("Voulez vous suscrire a une assurance annulation ?(Oui/Non):");
                    if (confirm(menu.readString()))
                    {
                        Assurance assurance = new Assurance();
                        List<Assurance> assurances = assurance.getAssurances("type", Assurance.ANNULATION);
                        int assId = 0;
                        foreach (Assurance ass in assurances)
                        {
                            assId = ass.AssuranceId;
                        }
                        reservation.insertDossierAssurance(dossierId, assId);
                    }

                    double prixTotal = reservation.getPrixTotal(dossierId);
                    menu.display("----------------------------------------------------------------------------------");
                    menu.display("==>   Prix Total pour votre reservation est " + prixTotal.ToString("C"));
                    menu.display("----------------------------------------------------------------------------------");
                    menu.display("Confirmer votre reservation(Oui/Non):");
                    bool confirmReservation = confirm(menu.readString());
                    if (confirmReservation)
                    {
                        new Model.Voyage().updateVoyage("placesDisponible = (placesDisponible -" + participants + ")", "voyageId = " + reservation.VoyageId);
                    }

                    int rc = reservation.endTransaction(confirmReservation);
                    if (confirmReservation)
                    {
                        if (rc == -1)
                        {
                            menu.displayFinal("Felicitations, votre reservation est confirmé.");
                        }
                        else
                        {
                            menu.displayFinal("Erreur - Reservation echoué");
                        }
                    }
                    else
                    {
                        if (rc == -1)
                        {
                            menu.displayFinal("Votre reservation est abandonée.");
                        }
                        else
                        {
                            menu.displayFinal("Erreur - Annulation echoué");
                        }
                    }
                }
                else
                {
                    menu.display("Pas assez de places disponible pour ce voyage, essayer une autre.");
                }
            }
            else
            {
                menu.display("Voyage avec le numéro " + reservation.VoyageId + " n'existe pas.");
            }
        }

        // Returns true if the string contains "Oui" or "O", otherwise false.
        private bool confirm(string answer)
        {
            bool confirm = false;
            if(answer.ToUpper() == "O" || answer.ToUpper() == "OUI")
            {
                confirm = true;
            }
            return confirm;
        }

        // Requests infomation for client and participants.
        private Personne fillPersonne(Menu menu, Personne personne)
        {
            menu.display("Civilité:");
            personne.Civilite = menu.readString();
            menu.display("Nom:");
            personne.Nom = menu.readString();
            menu.display("Prenom:");
            personne.Prenom = menu.readString();
            menu.display("Adresse:");
            personne.Adresse = menu.readString();
            menu.display("Telephone:");
            personne.Telephone = menu.readString();
            menu.display("Date de naissance(dd/mm/aaaa):");
            personne.DateNaissance = getDate(menu.readString());
            return personne;
        }

        // If date entered is not a valid date, create a default date.
        private DateTime getDate(string d)
        {
            DateTime date;
            if (!DateTime.TryParse(d, out date))
            {
                d = DateTime.Today.ToShortDateString();
            }
            return date;
        }
    }
}
