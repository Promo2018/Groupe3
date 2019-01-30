--create database BoVoyagesTP2RD;
use BoVoyagesTP2RD;

-- Drop Tables, if necessary, in the correct order.
/*
drop table AssuranceDossier;
drop table DossierParticipant;
drop table AgenceVoyage;
drop table Destination;
drop table Voyage;
drop table Personne;
drop table Client;
drop table Participant;
drop table Assurance;
drop table DossierReservation;
*/
-- Create Tables
-- agencyId foreign key must be between 1-100
create table AgenceVoyage (
agenceId int not null identity(1,1),
nom nvarchar(30) not null,
primary key(agenceId)
);

-- destinationId foreign key must be between 1001-1099
create table Destination (
destinationId int not null identity(1001,1),
continent nvarchar(30) not null,
pays nvarchar(30) not null,
region nvarchar(30) not null,
description nvarchar(300) not null,
primary key(destinationId)
);

-- voyageId foreign key must be between 2001-2200
create table Voyage (
voyageId int not null identity(2001,1),
dateAller date not null,
dateRetour date not null,
placesDisponible int not null,
tarifToutCompris decimal(10,2) not null,
destinationId int foreign key references Destination(destinationId),
agenceId int foreign key references AgenceVoyage(agenceId),
primary key(voyageId)
);

-- personneId foreign key must be between 5001-5100
create table Personne (
personneId int not null identity(5001,1),
civilite nvarchar(30) not null,
nom nvarchar(30) not null,
prenom nvarchar(30) not null,
adresse nvarchar(30) not null,
telephone nvarchar(30) not null,
dateNaissance date not null,
primary key(personneId)
);

-- clientId foreign key must be between 6001-6030
create table Client (
clientId int not null identity(6001,1),
email nvarchar(30) not null,
personneId int foreign key references Personne(personneId),
primary key(clientId)
);

-- participantId foreign key must be between 7001-7090
create table Participant (
participantId int not null identity(7001,1),
reduction float not null,
personneId int foreign key references Personne(personneId),
primary key(participantId)
);

-- assuranceId foreign key must be between 8001-8015
create table Assurance (
assuranceId int not null identity(8001,1),
type varchar(20) NOT NULL CHECK (type  IN('ANNULATION', 'BAGGAGE', 'RAPATRIEMENT')),
nom nvarchar(30) not null,
description nvarchar(300) not null,
primary key(assuranceId)
);

-- assuranceId foreign key must be between 9001-10000
create table DossierReservation (
dossierId int not null identity(9001,1),
etatDossierReservation varchar(10) NOT NULL CHECK (etatDossierReservation IN('ENATTENTE', 'ENCOURS', 'REFUSEE', 'ACCEPTEE')),
raisonAnnulationDossier varchar(20) CHECK (raisonAnnulationDossier IN('CLIENT', 'PLACESINSUFFISANTES')),
numeroCarteBancaire varchar(30) not null,
clientId int not null foreign key references Client(clientId),
voyageId int not null foreign key references Voyage(voyageId),
primary key(dossierId)
);

create table DossierParticipant (
dossierId int not null foreign key references DossierReservation(dossierId),
participantId int not null foreign key references Participant(participantId)
);

create table AssuranceDossier (
dossierId int not null foreign key references DossierReservation(dossierId),
assuranceId int not null foreign key references Assurance(assuranceId)
);

-- Load Tables

