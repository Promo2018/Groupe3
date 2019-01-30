use BoVoyagesTP2RD;
--select * from dossierParticipant order by dossierId;

declare @increment int;
declare @max int;
declare @count int;
declare @found int;
set @increment = 1;
set @max = 9;
set @found = 0;

while (@increment <= 1000)
	begin
		set @count = (select count(dossierId) from dossierParticipant where dossierId = @increment);
		if(@count > @max)
			begin
				print concat('Dossier ', @increment, ' has more than ', @max, ' participants');
				set @found = @found + 1;
			end;
		set @increment = @increment + 1;
	end;
if(@found > 0)
	print concat(@found, ' Dossier(s) with more than ', @max, ' participants found.');
else
	print concat('No dossier with more than ', @max, ' participants found.');

--select * from dossierParticipant where dossierId = 450;