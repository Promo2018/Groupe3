using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class DossierReservation_db
    {
        private string table = "DossiersReservation";

        public DossierReservation_db()
        {
        }

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

        public DossierReservation getDossier(DataRow row)
        {
            return new DossierReservation(int.Parse(row["dossierId"].ToString()),
                                          row["etatDossierReservation"].ToString(),
                                          row["raisonAnnulationDossier"].ToString(),
                                          row["numeroCarteBancaire"].ToString(),
                                          int.Parse(row["clientId"].ToString()),
                                          int.Parse(row["voyageId"].ToString()));
        }

        public int updateDossier(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        public int deleteDossier(int dossierId)
        {
            return DBAccess.getInstance().execNonQuery("delete from " + table + " where dossierId = " + dossierId + ";");
        }

        public int deleteDossiersForClient(int clientId)
        {
            return DBAccess.getInstance().execNonQuery("delete from DossierReservation where clientId = " + clientId + ";");
        }

        public int insertDossier(DossierReservation dossier)
        {
            return DBAccess.getInstance().execNonQuery("insert into " + table + " (etatDossierReservation, raisonAnnulationDossier, numeroCarteBancaire, clientId, voyageId) values ('" +
                                                                                        dossier.EtatReservation.ToString() + "', '" +
                                                                                        dossier.RaisonAnnulation.ToString() + "', '" +
                                                                                        dossier.NumeroCarteBancaire + "', " +
                                                                                        dossier.ClientId + ", " +
                                                                                        dossier.VoyageId + ");");
        }
    }
}
