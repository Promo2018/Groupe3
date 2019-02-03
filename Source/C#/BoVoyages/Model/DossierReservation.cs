using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    /**
     * Datastructure class.
     * Provides data attributes for this classs and via a facade pattern calls the appropirate database methods.
     */

    public class DossierReservation : Table
    {
        private DossierReservation_db dossier_Db = new DossierReservation_db();

        private int dossierId;
        private EtatDossierReservation etatReservation;
        private RaisonAnnulationDossier raisonAnnulation;
        private string numeroCarteBancaire;
        private int clientId;
        private int voyageId;

        public DossierReservation()
        {
            this.EtatReservation = new EtatDossierReservation(EtatDossierReservation.ENATTENTE);
            this.RaisonAnnulation = new RaisonAnnulationDossier(RaisonAnnulationDossier.NONE);
        }

        public DossierReservation(int dossierId, string dossierStatus, string cancelReason, string numeroCarteBancaire, int clientId, int voyageId)
        {
            this.DossierId = dossierId;
            this.EtatReservation = new EtatDossierReservation(dossierStatus);
            this.RaisonAnnulation = new RaisonAnnulationDossier(cancelReason);
            this.NumeroCarteBancaire = numeroCarteBancaire;
            this.ClientId = clientId;
            this.voyageId = voyageId;
        }

        public int DossierId { get => dossierId; set => dossierId = value; }
        public EtatDossierReservation EtatReservation { get => etatReservation; set => etatReservation = value; }
        public RaisonAnnulationDossier RaisonAnnulation { get => raisonAnnulation; set => raisonAnnulation = value; }
        public string NumeroCarteBancaire { get => numeroCarteBancaire; set => numeroCarteBancaire = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public int VoyageId { get => voyageId; set => voyageId = value; }

        public override void startTransaction()
        {
            dossier_Db.startTransaction();
        }

        public override int endTransaction(bool commit)
        {
            return dossier_Db.endTransaction(commit);
        }

        public DossierReservation getDossier(int dossierId)
        {
            return dossier_Db.getDossier(dossierId);
        }

        public List<DossierReservation> getDossiers(string key, string value)
        {
            return dossier_Db.getDossiers(key, value);
        }

        public List<DossierReservation> getDossiers()
        {
            return dossier_Db.getDossiers();
        }

        public List<Participant> getParticipantsForDossier(int dossierId)
        {
            return dossier_Db.getParticipantsForDossier(dossierId);
        }

        public List<Assurance> getAssurancesForDossier(int dossierId)
        {
            return dossier_Db.getAssurancesForDossier(dossierId);
        }

        public int updateDossier(string change, string condition)
        {
            return dossier_Db.updateDossier(change, condition);
        }

        public int deleteDossier(int dossierId)
        {
            return dossier_Db.deleteDossier(dossierId);
        }

        public int deleteDossiersForClient(int clientId)
        {
            return dossier_Db.deleteDossiersForClient(clientId);
        }

        public int insertDossier(DossierReservation dossier)
        {
            return dossier_Db.insertDossier(dossier);
        }

        public void insertDossierParticipants(int rId, List<int> participandIds)
        {
            dossier_Db.insertDossierParticipants(rId, participandIds);
        }

        public void insertDossierAssurance(int dossierId, int assuranceId)
        {
            dossier_Db.insertDossierAssurance(dossierId, assuranceId);
        }

        // Calculate the price of a voyage including assurance, if requested.
        // This amount is then multiplied by the reduction rate for each participant and added together for the Total Price of the Voyage. 
        public double getPrixTotal(int dossierId)
        {
            double prix = 0;
            double prixVoyage = getPrixVoyage(dossierId);
            List<Participant> participants = getParticipantsForDossier(dossierId);
            foreach(Participant participant in participants)
            {
                prix = (prix + (prixVoyage * (((double)participant.Reduction) / 100)));
            }
            return prix;
        }

        // Calculate the price of a voyage per participant, including assurance if requested.
        public double getPrixVoyage(int dossierId)
        {
            return dossier_Db.getPrixVoyage(voyageId, dossierId);
        }

        public void annuler(int dossierId, RaisonAnnulationDossier raison)
        {
            dossier_Db.annuler(dossierId, raison);
        }

        public void accepter(int dossierId)
        {
            dossier_Db.accepter(dossierId);
        }

        public void validerSolvabilite(int dossierId)
        {
            dossier_Db.validerSolvabilite(dossierId);
        }

        // ToString method for debug purposes.
        public override string ToString()
        {
            string dr = "";

            dr += "DossierId = " + DossierId + "\n";
            dr += "EtatReservation = " + EtatReservation.ToString() + "\n";
            dr += "RaisonAnnulation = " + RaisonAnnulation.ToString() + "\n";
            dr += "NumeroCarteBancaire = " + NumeroCarteBancaire + "\n";
            dr += "ClientId = " + ClientId + "\n";
            dr += "VoyageId = " + VoyageId + "\n";

            return dr;
        }

    }
}