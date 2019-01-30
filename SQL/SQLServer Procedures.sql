/*
--To drop these views and procedures. Decomment only when necessary.
drop view aParticipant 
drop view aClient 
drop view viewstatus
drop view DossierReservationPourParticipant
drop view ParticipantPourDossierReservation
drop view AssurancePourDossierReservation
drop view DossierReservationPourAssurance
drop procedure reserverVoyage
drop procedure accepterDossier
drop procedure annulerClient
drop procedure annulerNoPlaces
*/

--View for clients whose reservations are either accepted or refused so they can be notified, as one should. 
create view viewStatus as
select D.DossierId, D.etatDossierReservation, D.raisonAnnulationDossier, D.clientId, C.email
from DossierReservation D, Client C
where (D.etatDossierReservation = 'ACCEPTEE'or D.etatDossierReservation = 'REFUSEE') and D.clientId = C.clientId
;

  
--view of the Client and Personne table together
create view aClient as 
select Client.clientId, Personne.civilite, Personne.nom, Personne.prenom, Personne.adresse, Personne.telephone, Personne.dateNaissance, Client.email 
from Client, Personne where Client.personneId = Personne.personneId;


--view of the Participant and Personne together
create view aParticipant as 
select Participant.participantId, Personne.civilite, Personne.nom, Personne.prenom, Personne.adresse, Personne.telephone, Personne.dateNaissance, Participant.reduction 
from Participant, Personne where Participant.personneId = Personne.personneId;


-- Join to select all DossierReservations for a particular Participant.
create view DossierReservationPourParticipant as 
select DossierReservation.dossierId, DossierReservation.etatDossierReservation, DossierReservation.raisonAnnulationDossier, DossierReservation.numeroCarteBancaire, DossierReservation.clientId, DossierReservation.voyageId 
from DossierReservation
inner join DossierParticipant on DossierParticipant.dossierId = DossierReservation.dossierId;


-- Select all Participants for a particular DossierReservation.
create view ParticipantPourDossierReservation as 
select Participant.participantId, Personne.civilite, Personne.prenom, Personne.nom, Personne.adresse, Personne.telephone, Personne.dateNaissance, Participant.reduction 
from Participant, Personne, DossierParticipant, DossierReservation
where Participant.personneId = Personne.personneId and 
DossierParticipant.participantId = Participant.participantId and 
DossierReservation.dossierId = DossierParticipant.dossierId;


-- Join to select all Assurances for a particular DossierReservation.
create view AssurancePourDossierReservation as 
select Assurance.assuranceId, Assurance.type, Assurance.nom, Assurance.description, DossierReservation.dossierId
from DossierReservation, AssuranceDossier, Assurance
where AssuranceDossier.dossierId = DossierReservation.dossierId
and Assurance.assuranceId = AssuranceDossier.assuranceId;


-- Join to select all DossierReservations for a particular Assurance.
create view DossierReservationPourAssurance as 
select Assurance.assuranceId, DossierReservation.dossierId, DossierReservation.etatDossierReservation, DossierReservation.raisonAnnulationDossier, DossierReservation.numeroCarteBancaire, DossierReservation.clientId, DossierReservation.voyageId 
from DossierReservation, AssuranceDossier, Assurance
where AssuranceDossier.dossierId = DossierReservation.dossierId
and Assurance.assuranceId = AssuranceDossier.assuranceId;



--Procedure for booking a voyage including a check for available places
create procedure reserverVoyage
@idvoyage int,
@places int
as
DECLARE @PlaceRestantes int
SET @PlaceRestantes = (select placesDisponible from Voyage where voyageId = @idvoyage);

if @PlaceRestantes >= @places
	begin
		update Voyage set placesDisponible = (@PlaceRestantes-@places) where voyageId = @idvoyage;
		print N'Reservation effectuee';
	end;
else
	begin
		print concat('Places insuffisantes. Il ne reste que ',@PlaceRestantes, ' places.') ;
	end;
go  



--Procedure for changing dossier status to 'acceptee'. VIEWSTATUS view must be created !!!!
create procedure accepterDossier
@iddossier int
as
DECLARE @clientmail nvarchar(32);
SET @clientmail = (select email from viewstatus where dossierId = @iddossier);
update DossierReservation set etatDossierReservation = 'ACCEPTEE' where dossierId = @iddossier;
Print concat ('dossier acceptee. Informer client : ', @clientmail);
go


--Procedure for annulation par client. 
create procedure annulerClient
@iddossier int
as
update DossierReservation set raisonAnnulationDossier = 'CLIENT' where dossierId = @iddossier;
Print N'Annulation par client reussie';
go


--Procedure for annulation places insuffisantes. VIEWSTATUS view must be created !!!!
create procedure annulerNoPlaces
@iddossier int
as
DECLARE @clientmail nvarchar(32);
SET @clientmail = (select email from viewstatus where dossierId = @iddossier);
update DossierReservation set raisonAnnulationDossier = 'placesInsuffisantes' where dossierId = @iddossier;
Print concat ('dossier annule. Informer client places insuffisantes : ', @clientmail);
go


