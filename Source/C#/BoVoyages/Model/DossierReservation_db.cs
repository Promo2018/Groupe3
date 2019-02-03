using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    /**
     * This class provides uses the DBAccess instance to select, update, delete and insert to the DossiersReservation database table.
     */

    class DossierReservation_db :Table_db
    {
        private string table = "DossiersReservation";

        public DossierReservation_db()
        {
        }

        // Get the DossiersReservation information for a particular dossierId
        public DossierReservation getDossier(int dossierId)
        {
            DossierReservation dossier = new DossierReservation();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where DossierId = " + dossierId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                dossier = getDossier(row);
            }

            return dossier;
        }

        // Get a list of the DossiersReservation with where key=value
        public List<DossierReservation> getDossiers(string key, string value)
        {
            List<DossierReservation> dossiers = new List<DossierReservation>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                dossiers.Add(getDossier(row));
            }

            return dossiers;
        }

        // Get a list of all the DossiersReservation.
        public List<DossierReservation> getDossiers()
        {
            List<DossierReservation> dossiers = new List<DossierReservation>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                dossiers.Add(getDossier(row));
            }

            return dossiers;
        }

        // Get a list of all Participants in a DossiersReservation with a particular dossierId
        public List<Participant> getParticipantsForDossier(int dossierId)
        {
            List<Participant> participants = new List<Participant>();
            string selectString = "select * from ParticipantsPourDossierReservation where dossierId = " + dossierId + ";";
            DataSet ds = DBAccess.getInstance().execSelect(selectString);
            Participant_db participant = new Participant_db();
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                participants.Add(participant.getParticipant(row));
            }
            return participants;
        }

        // Get a list of all Assurances objects in a DossiersReservation with a particular dossierId
        public List<Assurance> getAssurancesForDossier(int dossierId)
        {
            List<Assurance> assurances = new List<Assurance>();
            string selectString = "select * from AssurancesPourDossierReservation where dossierId = " + dossierId + ";";
            DataSet ds = DBAccess.getInstance().execSelect(selectString);
            Assurance_db assurance = new Assurance_db();
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                assurances.Add(assurance.getAssurance(row));
            }
            return assurances;
        }

        // Parse an DossiersReservation from a data row.
        public DossierReservation getDossier(DataRow row)
        {
            return new DossierReservation(int.Parse(row["dossierId"].ToString()),
                                          row["etatDossierReservation"].ToString(),
                                          row["raisonAnnulationDossier"].ToString(),
                                          row["numeroCarteBancaire"].ToString(),
                                          int.Parse(row["clientId"].ToString()),
                                          int.Parse(row["voyageId"].ToString()));
        }

        // Update a DossiersReservation object using the parameters change and condition.
        public int updateDossier(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        // Delete a DossiersReservation object with a particular id.
        public int deleteDossier(int dossierId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where dossierId = " + dossierId + ";");
        }

        // Delete a DossiersReservation object with a particular clientId.
        public int deleteDossiersForClient(int clientId)
        {
            return DBAccess.getInstance().execNonQuery("delete from DossierReservation where clientId = " + clientId + ";");
        }

        // Insert a new DossiersReservation object to the database table.
        public int insertDossier(DossierReservation dossier)
        {
            DBAccess.getInstance().execNonQuery("insert into " + table + " (etatDossierReservation, raisonAnnulationDossier, numeroCarteBancaire, clientId, voyageId) values ('" +
                                                                            dossier.EtatReservation.ToString() + "', '" +
                                                                            dossier.RaisonAnnulation.ToString() + "', '" +
                                                                            dossier.NumeroCarteBancaire + "', " +
                                                                            dossier.ClientId + ", " +
                                                                            dossier.VoyageId + ");");
            return getLastIdentityId();
        }

        // Insert a new DossiersParticipants object to the database table.
        public void insertDossierParticipants(int dossierId, List<int>  participandIds)
        {
            foreach(int participantId in  participandIds)
            {
                DBAccess.getInstance().execNonQuery("insert into DossiersParticipants (dossierId, participantId) values (" +
                                                                                       dossierId + ", " +
                                                                                       participantId + ");");
            }

        }

        // Insert a new AssurancesDossiers object to the database table.
        public void insertDossierAssurance(int dossierId, int assuranceId)
        {
            DBAccess.getInstance().execNonQuery("insert into AssurancesDossiers (dossierId, assuranceId) values (" +
                                                                                 dossierId + ", " +
                                                                                 assuranceId + ");");
        }

        // Calculate the price of a voyage per participant, including assurance if requested.
        public double getPrixVoyage(int voyageId, int dossierId)
        {
            double prixVoyage = 0;
            DataSet ds = DBAccess.getInstance().execSelect("select tarifToutCompris from voyages where voyageId = " + voyageId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                prixVoyage = double.Parse(row["tarifToutCompris"].ToString());
            }
            if(doesDossierHaveAssurance(dossierId))
            {
                prixVoyage = prixVoyage + (prixVoyage * new Assurance().getPrixAssurancePourcentage());
            }
            return prixVoyage;
        }

        // Returns true if the DossiersReservation with the provided dossierId has Assurances.
        private bool doesDossierHaveAssurance(int dossierId)
        {
            bool doesDossierHaveAssurance = false;
            DataSet ds = DBAccess.getInstance().execSelect("select * from AssurancesDossiers where dossierId = " + dossierId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                doesDossierHaveAssurance = true;
            }
            return doesDossierHaveAssurance;
        }

        // Cancel a DossiersReservation with the provided dossierId with the provided reason.
        // This method uses the stored procedure "annulerDossier".
        public void annuler(int dossierId, RaisonAnnulationDossier raison)
        {
            string[] parms = new string[2];
            parms[0] = dossierId.ToString();
            parms[1] = raison.getRaisonAnnulationDossier();
            DBAccess.getInstance().execProcedureWithParams("annulerDossier", parms);
        }

        // Set the status of a DossiersReservation (with the provided dossierId) to "Accepted".
        // This method uses the stored procedure "accepterDossier".
        public void accepter(int dossierId)
        {
            string[] parms = new string[1];
            parms[0] = dossierId.ToString();
            DBAccess.getInstance().execProcedureWithParams("accepterDossier", parms);
        }

        // Checks to see if the DossiersReservation (with the provided dossierId) is Solvable.
        public void validerSolvabilite(int dossierId)
        {
            if(isClientSolvable())
            {
                accepter(dossierId);
            } else
            {
                deleteDossier(dossierId);
            }
        }

        // Method to checks solvablility. Needs to be properly implemented !
        private bool isClientSolvable()
        {
            return (System.DateTime.Now.Millisecond % 2 == 0);
        }

    }
}
