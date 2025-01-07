CREATE TYPE LogTable AS TABLE
(
 Id int NOT NULL,
 Num varchar NOT NULL
)
GO

create procedure devolverConsecutivos (@LogEntries LogTable readonly)
as
begin
	select distinct L1.Num as ConsecutiveNums
	from @LogEntries L1
	join @LogEntries L2 on L1.Id = L2.Id - 1 AND L1.Num = L2.Num
	join @LogEntries L3 on L2.Id = L3.Id -1 AND L2.Num = L3.Num;
end
GO

DECLARE @LogTable LogTable;
INSERT INTO @LogTable (Id, Num)
VALUES (1, '1'), (2, '1'), (3, '1'), (4, '2'), (5, '1'), (6, '2'), (7, '2');

EXEC devolverConsecutivos @LogTable