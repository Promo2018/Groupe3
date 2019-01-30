--select * from Client, Personne where Client.personneId = Personne.personneId;

--create view aClient as 
--select Client.clientId, Personne.civilite, Personne.nom, Personne.prenom, Personne.adresse, Personne.telephone, Personne.dateNaissance, Client.email 
--from Client, Personne where Client.personneId = Personne.personneId;


--create view aParticipant as 
--select Participant.participantId, Personne.civilite, Personne.nom, Personne.prenom, Personne.adresse, Personne.telephone, Personne.dateNaissance, Participant.reduction 
--from Participant, Personne where Participant.personneId = Personne.personneId;

select * from aClient where aClient.clientId = 2;

