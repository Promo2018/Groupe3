--select * from Client order by PermissionId;
--select * from Permission;
--select * from CreditCard

/* 

create view aClient as 
select Client.ClientId, Civility, FirstName, LastName, Address, PostCode, City, Country, Telephone, email, DOB, PermissionId, CardNumber, CardExpirationMMYY, Cryptogram, CardHolderName, BillingAddress 
from Client, CreditCard
where Client.ClientId = CreditCard.ClientId;
*/

select * from aClient order by ClientId;


--update aClient set CardHolderName = concat(Civility,' ',FirstName, ' ', LastName);

--select * from aClient order by ClientId;

