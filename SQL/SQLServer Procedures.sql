--View for clients whose reservations are either accepted or refused so they can be notified, as one should. 
create view viewStatus as
select D.DossierId, D.etatDossierReservation, D.raisonAnnulationDossier, D.clientId, C.email
from DossiersReservation D, Clients C
where (D.etatDossierReservation = 'ACCEPTEE'or D.etatDossierReservation = 'REFUSEE') and D.clientId = C.clientId;
go
  
--view of the Client and Personne table together
create view aClient as 
select Clients.clientId, Personnes.civilite, Personnes.nom, Personnes.prenom, Personnes.adresse, Personnes.telephone, Personnes.dateNaissance, Clients.email 
from Clients, Personnes where Clients.personneId = Personnes.personneId;
go

create view aParticipant as 
select Participants.participantId, Personnes.civilite, Personnes.nom, Personnes.prenom, Personnes.adresse, Personnes.telephone, Personnes.dateNaissance, Participants.reduction 
from Participants, Personnes where Participants.personneId = Personnes.personneId;
go

update aParticipant set reduction = 0.6  where dateNaissance > '2007-01-31';
go

-- Join to select all DossierReservations for a particular Participant.
create view DossiersReservationPourParticipant as 
select DossiersParticipants.participantId, DossiersReservation.dossierId, DossiersReservation.etatDossierReservation, DossiersReservation.raisonAnnulationDossier, DossiersReservation.numeroCarteBancaire, DossiersReservation.clientId, DossiersReservation.voyageId 
from DossiersReservation, DossiersParticipants
where DossiersParticipants.dossierId = DossiersReservation.dossierId;
go

-- Select all Participants for a particular DossierReservation.
create view ParticipantsPourDossierReservation as 
select Participants.participantId, Personnes.civilite, Personnes.prenom, Personnes.nom, Personnes.adresse, Personnes.telephone, Personnes.dateNaissance, Participants.reduction 
from Participants, Personnes, DossiersParticipants, DossiersReservation
where Participants.personneId = Personnes.personneId and 
DossiersParticipants.participantId = Participants.participantId and 
DossiersReservation.dossierId = DossiersParticipants.dossierId;
go

-- Join to select all Assurances for a particular DossierReservation.
create view AssurancesPourDossierReservation as select Assurances.assuranceId, Assurances.type, 
Assurances.nom, Assurances.description, DossiersReservation.dossierId 
from DossiersReservation, AssurancesDossiers, Assurances 
where AssurancesDossiers.dossierId = DossiersReservation.dossierId 
and Assurances.assuranceId = AssurancesDossiers.assuranceId;
go


create view DossiersReservationPourAssurance as 
select Assurances.assuranceId, DossiersReservation.dossierId, DossiersReservation.etatDossierReservation, DossiersReservation.raisonAnnulationDossier, DossiersReservation.numeroCarteBancaire, DossiersReservation.clientId, DossiersReservation.voyageId 
from DossiersReservation, AssurancesDossiers, Assurances
where AssurancesDossiers.dossierId = DossiersReservation.dossierId
and Assurances.assuranceId = AssurancesDossiers.assuranceId;
go

--Procedure for booking a voyage including a check for available places
create procedure reserverVoyage
@idvoyage int,
@places int
as
DECLARE @PlaceRestantes int
SET @PlaceRestantes = (select placesDisponible from Voyages where voyageId = @idvoyage);

if @PlaceRestantes >= @places
	begin
		update Voyages set placesDisponible = (@PlaceRestantes-@places) where voyageId = @idvoyage;
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
update DossiersReservation set etatDossierReservation = 'ACCEPTEE' where dossierId = @iddossier;
Print concat ('dossier acceptee. Informer client : ', @clientmail);
go


--Procedure for annulation par client. 
create procedure annulerClient
@iddossier int
as
update DossiersReservation set raisonAnnulationDossier = 'CLIENT' where dossierId = @iddossier;
Print N'Annulation par client reussie';
go


--Procedure for annulation places insuffisantes. VIEWSTATUS view must be created !!!!
create procedure annulerNoPlaces
@iddossier int
as
DECLARE @clientmail nvarchar(32);
SET @clientmail = (select email from viewstatus where dossierId = @iddossier);
update DossiersReservation set raisonAnnulationDossier = 'placesInsuffisantes' where dossierId = @iddossier;
Print concat ('dossier annule. Informer client places insuffisantes : ', @clientmail);
go