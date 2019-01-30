/*
--view for clients whose reservations are either accepted or refused so they can be notified, as one should. MUST BE CREATED !
create view viewstatus as
select D.DossierId, D.etatDossierReservation, D.raisonAnnulationDossier, D.clientId, C.email
from DossierReservation D, Client C
where (D.etatDossierReservation = 'acceptee'or D.etatDossierReservation = 'refusee') and D.clientId = C.clientId
;
*/
 

/*
create procedure reserver
@idvoyage int,
@places int
as
if ((select placesDisponible from Voyage where voyageId = @idvoyage)+@places) <= 9
	begin
		update Voyage set placesDisponible = ((select placesDisponible from Voyage where voyageId = @idvoyage)+@places) where voyageId = @idvoyage;
		print N'Reservation effectuee';
	end;
else
	begin
		print N'Places insuffisantes';
	end;
go 
*/
--select voyageId,dateAller, dateRetour, placesDisponible from Voyage where voyageId = 210;

--exec reserver '210','1';

--select * from DossierReservation where dossierId = 3;

/*
--Procedure for changing dossier status to 'acceptee'. VIEWSTATUS view must be created !!!!
create procedure accepter
@iddossier int
as
DECLARE @clientmail nvarchar(32);
SET @clientmail = (select email from viewstatus where dossierId = @iddossier);
update DossierReservation set etatDossierReservation = 'ACCEPTEE' where dossierId = @iddossier;
Print concat ('dossier acceptee. Informer client : ', @clientmail);
go
*/

/*
--Procedure for annulation par client. 
create procedure annulerclient
@iddossier int
as
update DossierReservation set raisonAnnulationDossier = 'CLIENT' where dossierId = @iddossier;
Print N'Annulation par client reussie';
go
*/

/*
--Procedure for annulation places insuffisantes. VIEWSTATUS view must be created !!!!
create procedure annulernoplces
@iddossier int
as
DECLARE @clientmail nvarchar(32);
SET @clientmail = (select email from viewstatus where dossierId = @iddossier);
update DossierReservation set raisonAnnulationDossier = 'placesInsuffisantes' where dossierId = @iddossier;
Print concat ('dossier annule. Informer client places insuffisantes : ', @clientmail);
go
*/
--select * from Client, Personne where Client.personneId = Personne.personneId;
/*
create view aClient as 
select Client.clientId, Personne.civilite, Personne.nom, Personne.prenom, Personne.adresse, Personne.telephone, Personne.dateNaissance, Client.email 
from Client, Personne where Client.personneId = Personne.personneId;
*/
/*
create view aParticipant as 
select Participant.participantId, Personne.civilite, Personne.nom, Personne.prenom, Personne.adresse, Personne.telephone, Personne.dateNaissance, Participant.reduction 
from Participant, Personne where Participant.personneId = Personne.personneId;
*/