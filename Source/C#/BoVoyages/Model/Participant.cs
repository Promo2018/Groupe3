using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    public class Participant : Personne
    {
        private Participant_db participant_Db = new Participant_db();
        private int participantId;
        private int reduction;

        public Participant() {}

        public Participant(int participantId,
                           string civilite, 
                           string nom, 
                           string prenom, 
                           string adresse, 
                           string telephone, 
                           DateTime dateNaissance,
                           int reduction) : base(civilite, nom, prenom, adresse, telephone, dateNaissance)
        {
            this.ParticipantId = participantId;
            this.Reduction = reduction;
        }

        public int ParticipantId { get => participantId; set => participantId = value; }
        public int Reduction { get => reduction; set => reduction = value; }

        public override void startTransaction()
        {
            participant_Db.startTransaction();
        }

        public override int endTransaction(bool commit)
        {
            return participant_Db.endTransaction(commit);
        }

        public Participant getParticipant(int participantId)
        {
            return participant_Db.getParticipant(participantId);
        }

        public List<Participant> getParticipants(string key, string value)
        {
            return participant_Db.getParticipants(key, value);
        }

        public List<Participant> getParticipants()
        {
            return participant_Db.getParticipants();
        }

        public List<DossierReservation> getDossiersForParticipant(int participantId)
        {
            return participant_Db.getDossiersForParticipant(participantId);
        }

        public int updateParticipant(string change, string condition)
        {
            return participant_Db.updateParticipant(change, condition);
        }

        public int deleteParticipant(int participantId)
        {
            return participant_Db.deleteParticipant(participantId);
        }

        public int insertParticipant(Participant participant)
        {
            return participant_Db.insertParticipant(participant);
        }

        public override string ToString()
        {
            string part = "";

            part += "ParticipantId = " + participantId + "\n";
            part += "Civilite = " + Civilite + "\n";
            part += "Nom = " + Nom + "\n";
            part += "Prenom = " + Prenom + "\n";
            part += "Adresse = " + Adresse + "\n";
            part += "Telephone = " + Telephone + "\n";
            part += "Date of Birth = " + DateNaissance.ToString() + "\n";
            part += "Age = " + Age() + "\n";
            part += "Reduction = " + Reduction + "\n";

            return part;
        }

    }
}
