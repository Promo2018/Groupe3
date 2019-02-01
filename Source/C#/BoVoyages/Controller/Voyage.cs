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
        private List<Model.Voyage> voyages;

        public Voyage()
        {
            voyages = new Model.Voyage().getVoyages();
        }

        public List<Model.Voyage> getVoyages()
        {
            return voyages;
        }

        public void deleteReservation(Menu menu)
        {
            Model.DossierReservation reservation = new DossierReservation();
            menu.display("Entrer le numéro de Dossier que vous voulez supprimer");
            reservation.deleteDossier(menu.readInt());
        }

        public void createReservation(Menu menu)
        {
            Model.DossierReservation reservation = new DossierReservation();
            menu.display("Entrer le numéro de Voyage que vous voulez reservez");
            reservation.VoyageId = menu.readInt();
            menu.display("Entrer le numéro de participants");
            int participants = menu.readInt();
            if(new Model.Voyage().arePlacesAvailable(reservation.VoyageId, participants))
            {
                reservation.startTransaction();
                List<int> participandIds = new List<int>();
                Client client = new Client();
                menu.display("Entrer votre numéro de client si vous avez, sinon 0");
                reservation.ClientId = menu.readInt();
                if(reservation.ClientId == 0)
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
                if(confirm(menu.readString()))
                {
                    Assurance assurance = new Assurance();
                    List<Assurance> assurances = assurance.getAssurances("type", Assurance.ANNULATION);
                    int assId = 0;
                    foreach(Assurance ass in assurances)
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
                if(confirmReservation)
                {
                    if (rc == -1)
                    {
                        menu.displayFinal("Felicitations, votre reservation est confirmé.");
                    } else
                    {
                        menu.displayFinal("Erreur - Reservation echoué");
                    }
                } else
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
                menu.display("Pas assez de places disponible pour ce voyages, essayer une autre.");
            }
        }

        private bool confirm(string answer)
        {
            bool confirm = false;
            if(answer.ToUpper() == "O" || answer.ToUpper() == "OUI")
            {
                confirm = true;
            }
            return confirm;
        }

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
            personne.DateNaissance = Convert.ToDateTime(menu.readString());
            return personne;
        }
    }
}
