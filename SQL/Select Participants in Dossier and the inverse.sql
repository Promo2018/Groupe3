/*
select * from DossierReservation;
*/

-- Join to select all DossierReservations for a particular Participant.
select DossierReservation.dossierId, DossierReservation.etatDossierReservation, DossierReservation.raisonAnnulationDossier, DossierReservation.numeroCarteBancaire, DossierReservation.clientId, DossierReservation.voyageId 
from DossierReservation
inner join DossierParticipant on DossierParticipant.dossierId = DossierReservation.dossierId 
where DossierParticipant.participantId = 1;

-- Select all Participants for a particular DossierReservation.
select Participant.participantId, Personne.civilite, Personne.prenom, Personne.nom, Personne.adresse, Personne.telephone, Personne.dateNaissance, Participant.reduction 
from Participant, Personne, DossierParticipant, DossierReservation
where DossierReservation.dossierId = 2 and 
Participant.personneId = Personne.personneId and 
DossierParticipant.participantId = Participant.participantId and 
DossierReservation.dossierId = DossierParticipant.dossierId;

/*

*/
