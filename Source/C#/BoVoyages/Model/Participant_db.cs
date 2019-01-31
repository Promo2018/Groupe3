using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    class Participant_db
    {
        private string table = "aParticipant";

        public Participant_db()
        {
        }

        public Participant getParticipant(int participantId)
        {
            Participant participant = new Participant();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where ParticipantId = " + participantId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                participant = getParticipant(row);
            }

            return participant;
        }

        public List<Participant> getParticipants(string key, string value)
        {
            List<Participant> participants = new List<Participant>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + " where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                participants.Add(getParticipant(row));
            }

            return participants;
        }

        public List<Participant> getParticipants()
        {
            List<Participant> participants = new List<Participant>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + table + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                participants.Add(getParticipant(row));
            }
            return participants;
        }

        public Participant getParticipant(DataRow row)
        {
            return new Participant(int.Parse(row["ParticipantId"].ToString()),
                                   row["civilite"].ToString(),
                                   row["nom"].ToString(),
                                   row["prenom"].ToString(),
                                   row["adresse"].ToString(),
                                   row["telephone"].ToString(),
                                   ((DateTime)row["dateNaissance"]),
                                   float.Parse(row["reduction"].ToString()));
        }

        public List<DossierReservation> getDossiersForParticipant(int participantId)
        {
            List<DossierReservation> dossiers = new List<DossierReservation>();
            string selectString = "select * from DossiersReservationPourParticipant where participantId = " + participantId + ";";
            DataSet ds = DBAccess.getInstance().execSelect(selectString);
            DossierReservation_db dossier = new DossierReservation_db();
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                dossiers.Add(dossier.getDossier(row));
            }
            return dossiers;
        }

        public int updateParticipant(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + table + " set " + change + " where " + condition + ";");
        }

        public int deleteParticipant(int ParticipantId)
        {
//            return DBAccess.getInstance().execNonQuery("delete from aParticipant where ParticipantId = " + ParticipantId + ";");
//              To be done ! ! !
            return 0;
        }

        public int insertParticipant(Participant Participant)
        {
            return DBAccess.getInstance().execNonQuery("insert into " + table + " (civilite, nom, prenom, adresse, telephone, DOB, email) values ('" +
                                                                                  Participant.Civilite + "', '" +
                                                                                  Participant.Nom + "', '" +
                                                                                  Participant.Prenom + "', '" +
                                                                                  Participant.Adresse + "', '" +
                                                                                  Participant.Telephone + "', '" +
                                                                                  Participant.DateNaissance.ToShortDateString() + "', " +
                                                                                  Participant.Reduction + ");");
        }

    }
}
