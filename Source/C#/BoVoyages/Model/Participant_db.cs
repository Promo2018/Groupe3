using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    /**
     * This class provides uses the DBAccess instance to select, update, delete and insert to the aParticipant view and the Participants database table.
     * The aParticipant view contains information of the Participants and the Personnes database tables.
     */

    class Participant_db : Personne_db
    {
        private string table = "Participants";
        private string view = "aParticipant";

        public Participant_db()
        {
        }

        // Get the aParticipant information for a particular participantId
        public Participant getParticipant(int participantId)
        {
            Participant participant = new Participant();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + view + " where ParticipantId = " + participantId + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                participant = getParticipant(row);
            }

            return participant;
        }

        // Get a list of the participants with where key=value
        public List<Participant> getParticipants(string key, string value)
        {
            List<Participant> participants = new List<Participant>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + view + " where " + key + " = '" + value + "';");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                participants.Add(getParticipant(row));
            }

            return participants;
        }

        // Get a list of all the participants.
        public List<Participant> getParticipants()
        {
            List<Participant> participants = new List<Participant>();
            DataSet ds = DBAccess.getInstance().execSelect("select * from " + view + ";");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                participants.Add(getParticipant(row));
            }
            return participants;
        }

        // Parse a Participant from a data row.
        public Participant getParticipant(DataRow row)
        {
            return new Participant(int.Parse(row["ParticipantId"].ToString()),
                                   row["civilite"].ToString(),
                                   row["nom"].ToString(),
                                   row["prenom"].ToString(),
                                   row["adresse"].ToString(),
                                   row["telephone"].ToString(),
                                   ((DateTime)row["dateNaissance"]),
                                   int.Parse(row["reduction"].ToString()));
        }

        // Get a list of all the DossierReservation for a particular Participant with the provided participantId.
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

        // Update a Participant object using the parameters change and condition.
        public int updateParticipant(string change, string condition)
        {
            return DBAccess.getInstance().execNonQuery("update " + view + " set " + change + " where " + condition + ";");
        }

        // Delete a Participants object with a particular id. Also delete any Dossiers that this Participant has reserved.
        // And if the Participant is not a Client delete the Personne information paertaining to this Participant.
        public int deleteParticipant(int participantId)
        {
            int ret = 0;
            bool isPaC = isParticipantaClient(participantId);
            ret += DBAccess.getInstance().execNonQuery("delete from DossiersParticipants where participantId = " + participantId + ";");
            ret += DBAccess.getInstance().execNonQuery("delete from " + table + " where ParticipantId = " + participantId + ";");
            if (!isPaC)
            {
                ret = DBAccess.getInstance().execNonQuery("delete from Personnes where personneId = (select Personnes.personneId from Personnes, Participants where Personnes.personneId = Participants.personneId and  Participants.participantId = " + participantId + ");");
            }
            return ret;
        }

        // Returns true if the Participant is also a Client.
        private bool isParticipantaClient(int participantId)
        {
            bool participantIsAClient = false;
            DataSet ds = DBAccess.getInstance().execSelect("select * from Clients where personneId = (select distinct P.personneId from Participants P, Clients C where P.personneId = C.personneId and P.participantId = " + participantId + ");");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                participantIsAClient = true; ;
            }
            return participantIsAClient;
        }

        // Insert a new Participants object to the database table.
        public int insertParticipant(Participant participant)
        {
            int id = insertPersonne(participant);
            if(participant.Age() < 12)
            {
                participant.Reduction = 60;
            } else
            {
                participant.Reduction = 100;
            }
            DBAccess.getInstance().execNonQuery("insert into " + table + " (reduction, personneId) values (" +
                                                                            participant.Reduction + ", " +
                                                                            id + ");");
            id = getLastIdentityId();
            return id;
        }

    }
}
