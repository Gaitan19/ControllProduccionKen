
CREATE FUNCTION dbo.GetUserNamesFromIDs
(
    @UserIds NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @Result NVARCHAR(MAX);

    SELECT @Result = STRING_AGG(u.UserName, ',')
    FROM dbo.AspNetUsers u
    INNER JOIN STRING_SPLIT(@UserIds, ',') s
       ON u.Id = s.value;

    RETURN @Result;
END