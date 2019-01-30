--select * from Voyage order by DestinationId;
--select * from Destination order by DestinationId;
--select * from TravelAgency;

/*

create view aVoyage as 
select VoyageId, LeaveDate, ReturnDate, NumberParticipants, Price, AgencyPrice, DestinationName, Continent, Country, Region, Description, AgencyName, ContactName ContactDetails 
from Voyage, Destination, TravelAgency
where Voyage.AgencyId = TravelAgency.AgencyId and Voyage.DestinationId = Destination.DestinationId;

*/

select * from aVoyage;

