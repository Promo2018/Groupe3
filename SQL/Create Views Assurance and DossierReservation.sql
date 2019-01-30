--select * from Assurance ;
--select  * from DossierReservation;
--select  * from AssuranceDossier;

-- Join to select all Assurances for a particular DossierReservation.
/*
create view AssurancePourDossierReservation as 
select Assurance.assuranceId, Assurance.type, Assurance.nom, Assurance.description, DossierReservation.dossierId
from DossierReservation, AssuranceDossier, Assurance
where AssuranceDossier.dossierId = DossierReservation.dossierId
and Assurance.assuranceId = AssuranceDossier.assuranceId;
*/
-- Join to select all DossierReservations for a particular Assurance.

/*
create view DossierReservationPourAssurance as 
select Assurance.assuranceId, DossierReservation.dossierId, DossierReservation.etatDossierReservation, DossierReservation.raisonAnnulationDossier, DossierReservation.numeroCarteBancaire, DossierReservation.clientId, DossierReservation.voyageId 
from DossierReservation, AssuranceDossier, Assurance
where AssuranceDossier.dossierId = DossierReservation.dossierId
and Assurance.assuranceId = AssuranceDossier.assuranceId;
*/

select * from AssurancePourDossierReservation  where dossierId = 2;
select * from DossierReservationPourAssurance where assuranceId = 5002;

