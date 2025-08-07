

CREATE FUNCTION [dbo].[GetArticulosTermoIsoPanelFromIDs]
(
    @ArtIds NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @Result NVARCHAR(MAX);

    SELECT @Result = STRING_AGG(u.DescripcionArticulo, ',')
    FROM cp.CatProdTermoIsoPanel u
    INNER JOIN STRING_SPLIT(@ArtIds, ',') s
       ON u.Id = s.value;

    RETURN @Result;
END