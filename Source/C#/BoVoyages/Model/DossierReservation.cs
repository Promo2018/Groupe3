using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    public class DossierReservation
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