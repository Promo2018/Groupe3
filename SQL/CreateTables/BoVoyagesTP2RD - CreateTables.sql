--create database BoVoyagesTP2RD;
use BoVoyagesTP2RD;

-- Drop Tables, if necessary, in the correct order.
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

-- Create Tables
create table AgenceVoyage (
agenceId int not null,
nom nvarchar(30) not null,
primary key(agenceId)
);

create table Destination (
destinationId int not null,
continent nvarchar(30) not null,
pays nvarchar(30) not null,
region nvarchar(30) not null,
description nvarchar(300) not null,
primary key(destinationId)
);

create table Voyage (
voyageId int not null,
dateAller date not null,
dateRetour date not null,
placesDisponible int not null,
tarifToutCompris decimal(10,2) not null,
destinationId int foreign key references Destination(destinationId),
agenceId int foreign key references AgenceVoyage(agenceId),
primary key(voyageId)
);

create table Personne (
personneId int not null identity(1,1),
civilite nvarchar(30) not null,
nom nvarchar(30) not null,
prenom nvarchar(30) not null,
adresse nvarchar(30) not null,
telephone nvarchar(30) not null,
dateNaissance date not null,
primary key(personneId)
);

create table Client (
clientId int not null identity(1,1),
email nvarchar(30) not null,
personneId int foreign key references Personne(personneId),
primary key(clientId)
);

create table Participant (
participantId int not null identity(1,1),
reduction float not null,
personneId int foreign key references Personne(personneId),
primary key(participantId)
);

create table Assurance (
assuranceId int not null,
type varchar(20) NOT NULL CHECK (type  IN('ANNULATION', 'BAGGAGE', 'RAPATRIEMENT')),
nom nvarchar(30) not null,
description nvarchar(300) not null,
primary key(assuranceId)
);

create table DossierReservation (
dossierId int not null identity(1,1),
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

