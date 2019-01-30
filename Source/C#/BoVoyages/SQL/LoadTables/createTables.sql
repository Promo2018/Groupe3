use BoVoyagesRD

drop table Insurance;
drop table Dossier;
drop table Commercial;
drop table CreditCard;
drop table Client;
drop table Permission;
drop table Voyage;
drop table Destination;
drop table TravelAgency;



create table TravelAgency (
AgencyId int not null,
AgencyName nvarchar(30) not null,
ContactName nvarchar(30) not null,
ContactDetails nvarchar(30) not null,
primary key(AgencyId)
);

create table Destination (
DestinationId int identity not null,
DestinationName nvarchar(30),
Continent nvarchar(30) not null,
Country nvarchar(30) not null,
Region nvarchar(30) not null,
Description nvarchar(300) not null,
primary key(DestinationId)
);

create table Voyage (
VoyageId int not null,
LeaveDate date not null,
ReturnDate date not null,
NumberParticipants int not null,
Price int not null,
DestinationId int foreign key references Destination(DestinationId),
AgencyId int foreign key references TravelAgency(AgencyId),
AgencyPrice int not null,
primary key(VoyageId)
);

create table Permission (
PermissionId int not null,
AccountLogin nvarchar(30) not null,
AccountPassword nvarchar(30) not null,
PermissionLevel int not null,
primary key(PermissionId)
);

create table Client (
ClientId int not null identity,
Civility nvarchar(30) not null,
FirstName nvarchar(30) not null,
LastName nvarchar(30) not null,
Address nvarchar(300) not null,
PostCode nvarchar(30) not null,
City nvarchar(30) not null,
Country nvarchar(30) not null,
Telephone nvarchar(30) not null,
email nvarchar(30) not null,
DOB date not null,
PermissionId int foreign key references Permission(PermissionId),
primary key(ClientId)
);

create table CreditCard (
CardNumber nvarchar(30) not null,
CardExpirationMMYY date not null,
Cryptogram nvarchar(3) not null,
CardHolderName nvarchar(30) not null,
BillingAddress nvarchar(300) not null,
ClientId int foreign key references Client(ClientId)
);

create table Commercial (
CommercialId int not null identity,
Civility nvarchar(30) not null,
FirstName nvarchar(30) not null,
LastName nvarchar(30) not null,
PermissionId int foreign key references Permission(PermissionId),
primary key(CommercialId)
);

create table Dossier (
DossierId int not null identity,
DossierStatus nvarchar(30) not null,
ClientId int foreign key references Client(ClientId),
VoyageId int foreign key references Voyage(VoyageId),
primary key(DossierId)
);

create table Insurance (
InsuranceId int not null,
InsuranceName nvarchar(30) not null,
InsuranceDescription nvarchar(30) not null,
DossierId int foreign key references Dossier(DossierId),
primary key(InsuranceId)
);
