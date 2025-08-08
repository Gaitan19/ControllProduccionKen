using Infrastructure.Data;
using Infrastructure.DTO;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Repositories
{
    public class ReportesDapperRepository : IReportesDapperRepository
    {
        private readonly AppDbContext _context;
        public ReportesDapperRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Validates and ensures DateTime parameters are within SQL Server's valid range
        /// </summary>
        /// <param name="dateTime">DateTime to validate</param>
        /// <returns>Valid DateTime within SQL Server range</returns>
        private static DateTime ValidateSqlDateTime(DateTime dateTime)
        {
            // SQL Server DateTime range: 1/1/1753 12:00:00 AM to 12/31/9999 11:59:59 PM
            var sqlMinDate = SqlDateTime.MinValue.Value;
            var sqlMaxDate = SqlDateTime.MaxValue.Value;

            if (dateTime < sqlMinDate)
                return sqlMinDate;
            
            if (dateTime > sqlMaxDate)
                return sqlMaxDate;
            
            return dateTime;
        }

        public async Task<IEnumerable<PrdCerchaCovintecReporteDTO>> GetAllCerchaProduccionWithDetailsAsync(DateTime start, DateTime end)
        {
            // Validate DateTime parameters to ensure they're within SQL Server range
            start = ValidateSqlDateTime(start);
            end = ValidateSqlDateTime(end);

            using var connection = _context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var sql = @"
-- Encabezado: registros de cercha
SELECT 
    PrdCMain.Id,
    dbo.GetUserNamesFromIDs(PrdCMain.IdUsuarios)        AS Operarios,
    PrdCMain.Fecha,
    PrdCMain.ConteoInicial,
    PrdCMain.ConteoFinal,
    M.Nombre                                           AS Maquina,
    PrdCMain.Observaciones,
    PrdCMain.MermaAlambre,
    PrdCMain.TiempoParo,
    dbo.GetUserNamesFromIDs(PrdCMain.IdAprobadoSupervisor) AS Supervisor,
    dbo.GetUserNamesFromIDs(PrdCMain.IdAprobadoGerencia)   AS JefeProd
FROM cp.PrdCerchaCovintec PrdCMain
INNER JOIN cp.DetPrdCerchaCovintec DetCPC       ON DetCPC.IdCercha = PrdCMain.Id
INNER JOIN cp.DetAlambrePrdCerchaCovintec DetAl ON DetAl.IdCercha  = PrdCMain.Id
INNER JOIN cp.Maquinas M                       ON M.Id = PrdCMain.IdMaquina

WHERE CAST(PrdCMain.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE) AND 
      PrdCMain.AprobadoGerencia = 1
GROUP BY 
    PrdCMain.Id,
    PrdCMain.IdUsuarios,
    PrdCMain.Fecha,
    PrdCMain.ConteoInicial,
    PrdCMain.ConteoFinal,
    M.Nombre,
    PrdCMain.Observaciones,
    PrdCMain.MermaAlambre,
    PrdCMain.TiempoParo,
    PrdCMain.IdAprobadoSupervisor,
    PrdCMain.IdAprobadoGerencia;

-- Detalle 1: datos de cerchas
SELECT 
    DPC.IdCercha   ,
    PC.DescripcionArticulo AS Articulo,
    DPC.CantidadProducida,
    DPC.CantidadNoConforme,
    TF.Descripcion AS TipoFabricacion,
    ISNULL(DPC.NumeroPedido, '') AS NumeroPedido,
    DPC.ProduccionDia
FROM cp.DetPrdCerchaCovintec DPC
INNER JOIN cp.TipoFabricacion TF ON TF.Id = DPC.IdTipoFabricacion
INNER JOIN cp.CatalogoCercha     PC ON PC.Id = DPC.IdArticulo;

-- Detalle 2: datos de alambre de cerchas
SELECT 
    DAPC.IdCercha    ,
    DAPC.NumeroAlambre,
    DAPC.PesoAlambre
FROM cp.DetAlambrePrdCerchaCovintec DAPC;
";

            using var multi = await connection.QueryMultipleAsync(sql, new { start, end });

            // Leer encabezados
            var encabezados = (await multi.ReadAsync<PrdCerchaCovintecReporteDTO>()).ToList();
            // Leer detalle de cerchas
            var detallesPanel = (await multi.ReadAsync<DetalleCerchaDto>()).ToList();
            // Leer detalle de alambre
            var detallesAlambre = (await multi.ReadAsync<DetalleAlambreCerchaDto>()).ToList();

            // Asociar detalles a cada encabezado
            foreach (var encabezado in encabezados)
            {
                encabezado.DetallesPanel = detallesPanel
                    .Where(d => d.IdCercha == encabezado.Id)
                    .ToList();

                encabezado.DetallesAlambre = detallesAlambre
                    .Where(a => a.IdCercha == encabezado.Id)
                    .ToList();
            }

            return encabezados;
        }

        public async Task<IEnumerable<PrdMallasCovintecReporteDTO>> GetAllMallaProduccionWithDetailsAsync(DateTime start, DateTime end)
        {
            // Validate DateTime parameters to ensure they're within SQL Server range
            start = ValidateSqlDateTime(start);
            end = ValidateSqlDateTime(end);

            // Obtén la conexión (ajusta según dónde la expongas)
            using var connection = _context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var sql = @"
-- Encabezados de Mallas
SELECT 
    P.Id,
    dbo.GetUserNamesFromIDs(P.IdUsuarios) AS Operarios,
    P.Fecha,
    M.Nombre        AS Maquina,
    P.Observaciones,
    P.MermaAlambre,
    P.TiempoParo,
    dbo.GetUserNamesFromIDs(P.IdAprobadoSupervisor) AS Supervisor,
    dbo.GetUserNamesFromIDs(P.IdAprobadoGerencia)   AS JefeProd
FROM cp.PrdMallaCovintec P
INNER JOIN cp.DetPrdMallaCovintec D   ON D.IdMalla = P.Id
INNER JOIN cp.DetAlambrePrdMallaCovintec A ON A.IdMalla = P.Id
INNER JOIN cp.Maquinas M              ON M.Id = P.IdMaquina
WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE) AND 
      P.AprobadoGerencia = 1
GROUP BY
    P.Id, P.IdUsuarios, P.Fecha, M.Nombre, P.Observaciones, P.MermaAlambre, P.TiempoParo,
    P.IdAprobadoSupervisor, P.IdAprobadoGerencia

-- Detalle de Malla (artículos)
SELECT
    D.IdMalla     AS IdPanel,
    C.DescripcionArticulo AS Articulo,
    D.CantidadProducida,
    D.CantidadNoConforme,
    TF.Descripcion       AS TipoFabricacion,
    ISNULL(D.NumeroPedido, '') AS NumeroPedido,
    D.ProduccionDia
FROM cp.DetPrdMallaCovintec D
INNER JOIN cp.TipoFabricacion TF    ON TF.Id = D.IdTipoFabricacion
INNER JOIN cp.CatalogoMallasCovintec C ON C.Id = D.IdArticulo

-- Detalle de Alambre
SELECT
    A.IdMalla    AS IdPanel,
    CAST(A.NumeroAlambre AS VARCHAR(10)) AS NumeroAlambre,
    A.PesoAlambre
FROM cp.DetAlambrePrdMallaCovintec A;
";

            using var multi = await connection.QueryMultipleAsync(sql, new { start, end });

            // 1) Encabezados
            var encabezados = (await multi.ReadAsync<PrdMallasCovintecReporteDTO>()).ToList();
            // 2) Detalles de malla
            var detallesMalla = (await multi.ReadAsync<DetalleMallaDto>()).ToList();
            // 3) Detalles de alambre
            var detallesAlambre = (await multi.ReadAsync<DetalleAlambreMallaDto>()).ToList();

            // Asociar detalles a cada encabezado por Id
            foreach (var encabezado in encabezados)
            {
                encabezado.DetallesPanel = detallesMalla
                    .Where(d => d.IdPanel == encabezado.Id)
                    .ToList();
                encabezado.DetallesAlambre = detallesAlambre
                    .Where(a => a.IdPanel == encabezado.Id)
                    .ToList();
            }

            return encabezados;
        }

        public async Task<IEnumerable<PrdPanelesCovintecReporteDTO>> GetAllPanelProduccionWithDetailsAsync(DateTime start, DateTime end)
        {
            // Validate DateTime parameters to ensure they're within SQL Server range
            start = ValidateSqlDateTime(start);
            end = ValidateSqlDateTime(end);

            using (var connection = _context.Database.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var query = @"
-- Encabezado: Se recupera la lista de paneles (se usa DISTINCT para evitar duplicados)
select PrdPCMain.Id,dbo.GetUserNamesFromIDs(PrdPCMain.IdUsuarios) as Operarios,PrdPCMain.Fecha,M.Nombre as Maquina, PrdPCMain.Observaciones,PrdPCMain.MermaAlambre ,PrdPCMain.TiempoParo,
dbo.GetUserNamesFromIDs(PrdPCMain.IdAprobadoSupervisor) as Supervisor,dbo.GetUserNamesFromIDs(PrdPCMain.IdAprobadoGerencia) as JefeProd from cp.PrdPanelesCovintec PrdPCMain 
INNER JOIN cp.DetPrdPanelesCovintec DetPrdPC on DetPrdPC.IdPanel=PrdPCMain.Id
INNER JOIN cp.DetAlambrePrdPanelesCovintec DetAlamPC on DetAlamPC.IdPanel=PrdPCMain.Id
INNER JOIN CP.Maquinas M ON M.Id=PrdPCMain.IdMaquina
WHERE CAST(PrdPCMain.Fecha AS DATE) BETWEEN CAST(@start AS DATE)  AND CAST(@end AS DATE) AND 
      PrdPCMain.AprobadoGerencia = 1
GROUP BY PrdPCMain.IdUsuarios,PrdPCMain.Fecha,M.Nombre , PrdPCMain.Observaciones,PrdPCMain.MermaAlambre,PrdPCMain.TiempoParo,PrdPCMain.Id,PrdPCMain.IdAprobadoSupervisor,PrdPCMain.IdAprobadoGerencia

-- Detalle 1: Paneles
SELECT 
    DPC.IdPanel,
    PC.DescripcionArticulo AS Articulo,
    DPC.CantidadProducida,
    DPC.CantidadNoConforme,
    TF.Descripcion AS TipoFabricacion,
    ISNULL(DPC.NumeroPedido, '') AS NumeroPedido,
    DPC.ProduccionDia
FROM cp.DetPrdPanelesCovintec DPC
INNER JOIN CP.TipoFabricacion TF ON TF.Id = DPC.IdTipoFabricacion
INNER JOIN cp.CatalogoPanelesCovintec PC ON PC.Id = DPC.IdArticulo

-- Detalle 2: Alambre
SELECT 
    DAPC.IdPanel,
    DAPC.NumeroAlambre,
    DAPC.PesoAlambre
FROM cp.DetAlambrePrdPanelesCovintec DAPC;
";

                using (var multi = await connection.QueryMultipleAsync(query, new { start = start, end = end }))
                {
                    // Primer conjunto: Encabezado
                    var encabezados = (await multi.ReadAsync<PrdPanelesCovintecReporteDTO>()).ToList();
                    // Segundo conjunto: Detalle de Paneles
                    var detallesPanel = (await multi.ReadAsync<DetallePanelDto>()).ToList();
                    // Tercer conjunto: Detalle de Alambre
                    var detallesAlambre = (await multi.ReadAsync<DetalleAlambreDto>()).ToList();

                    // Asociar en memoria los detalles a cada encabezado usando el Id.
                    foreach (var encabezado in encabezados)
                    {
                        encabezado.DetallesPanel = detallesPanel.Where(d => d.IdPanel == encabezado.Id).ToList();
                        encabezado.DetallesAlambre = detallesAlambre.Where(a => a.IdPanel == encabezado.Id).ToList();
                    }

                    return encabezados;
                }
            }
        }

        public async Task<IEnumerable<PrdBloquesReporteDTO>> GetAllPrdBloqueWithDetailsAsync(DateTime start, DateTime end)
        {
            // Validate DateTime parameters to ensure they're within SQL Server range
            start = ValidateSqlDateTime(start);
            end = ValidateSqlDateTime(end);

            using (var connection = _context.Database.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var sql = @"
-- 1) Encabezados de bloques
SELECT
    P.Id,
    dbo.GetUserNamesFromIDs(P.IdUsuarios)           AS Operarios,
    dbo.GetUserNamesFromIDs(P.IdAprobadoSupervisor) AS Supervisor,
    dbo.GetUserNamesFromIDs(P.IdAprobadoGerencia)   AS JefeProd,
    P.Fecha
FROM cp.PrdBloques P
WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
  AND P.AprobadoGerencia = 1;

-- 2) Detalles de bloques
SELECT
    D.Id,
    D.PrdBloqueId,
    (CB.Bloque + CB.Medidas) AS Articulo,
    M.Nombre                AS Maquina,
    D.ProduccionDia
FROM cp.DetPrdBloques D
INNER JOIN cp.Maquinas M       ON M.Id       = D.IdMaquina
INNER JOIN cp.CatalogoBloques CB ON CB.Id     = D.IdCatBloque
WHERE D.PrdBloqueId IN (
    SELECT Id FROM cp.PrdBloques
    WHERE CAST(Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
      AND AprobadoGerencia = 1
);

-- 3) Subdetalles de bloques
SELECT
    S.DetPrdBloquesId,
    S.No,
    S.Silo,
    S.CodigoBloque,
    (CB.Bloque + CB.Medidas)      AS Articulo,
    TF.Descripcion                AS TipoFabricacion,
    S.NumeroPedido,
    S.Observaciones,
    S.Peso,
    S.Hora,
    TB.Descripcion                AS TipoBloque,
    MC.Descripcion                AS Densidad
FROM cp.SubDetPrdBloques S
INNER JOIN cp.CatalogoBloques CB    ON CB.Id = S.IdArticulo
INNER JOIN cp.MaestroCatalogo MC    ON MC.Id = S.IdDensidad
INNER JOIN cp.MaestroCatalogo TB    ON TB.Id = S.IdTipoBloque
INNER JOIN cp.TipoFabricacion TF    ON TF.Id = S.IdTipoFabricacion
WHERE S.DetPrdBloquesId IN (
    SELECT D.Id
    FROM cp.DetPrdBloques D
    INNER JOIN cp.PrdBloques P ON P.Id = D.PrdBloqueId
    WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
      AND P.AprobadoGerencia = 1
);
";

                using (var multi = await connection.QueryMultipleAsync(sql, new { start, end }))
                {
                    var headers = (await multi.ReadAsync<PrdBloquesReporteDTO>()).ToList();
                    var detalles = (await multi.ReadAsync<DetalleBloquesDto>()).ToList();
                    var subdetalles = (await multi.ReadAsync<SubDetalleBloquesDto>()).ToList();

                    // Asociar subdetalles a cada detalle
                    foreach (var det in detalles)
                    {
                        det.SubDetalles = subdetalles
                            .Where(sd => sd.DetPrdBloquesId == det.Id)
                            .ToList();
                    }

                    // Asociar detalles anidados a cada encabezado
                    foreach (var header in headers)
                    {
                        header.Detalles = detalles
                            .Where(d => d.PrdBloqueId == header.Id)
                            .ToList();
                    }

                    return headers;
                }
            }
        }

        public async Task<IEnumerable<PrdCorteTReporteDTO>> GetAllPrdCorteTWithDetailsAsync(DateTime start, DateTime end)
        {
            // Validate DateTime parameters to ensure they're within SQL Server range
            start = ValidateSqlDateTime(start);
            end = ValidateSqlDateTime(end);

            using (var connection = _context.Database.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var sql = @"
-- 1) Encabezados de Corte T
SELECT
    P.Id,
    P.TiempoParo,
    P.IdTipoReporte,
    P.IdUsuarios,
    dbo.GetUserNamesFromIDs(P.IdUsuarios)           AS Operarios,
    P.IdMaquina,
    M.Nombre                                        AS Maquina,
    P.Fecha,
    P.Observaciones,
    P.IdUsuarioCreacion,
    P.FechaCreacion,
    P.IdUsuarioActualizacion,
    P.FechaActualizacion,
    P.AprobadoSupervisor,
    P.AprobadoGerencia,
    P.IdAprobadoSupervisor,
    P.IdAprobadoGerencia,
    dbo.GetUserNamesFromIDs(P.IdAprobadoSupervisor) AS Supervisor,
    dbo.GetUserNamesFromIDs(P.IdAprobadoGerencia)   AS JefeProd
FROM cp.PrdCorteT P
INNER JOIN cp.Maquinas M ON M.Id = P.IdMaquina
WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
  AND P.AprobadoGerencia = 1;

-- 2) Detalles de Corte T
SELECT
    D.Id,
    D.PrdCorteTId              AS IdCorteT,
    D.No,
    MCA.Descripcion            AS Articulo,
    TF.Descripcion             AS TipoFabricacion,
    D.NumeroPedido,
    D.PrdCodigoBloque,
    MC.Descripcion             AS Densidad,
    TB.Descripcion             AS TipoBloque,
    D.CantidadPiezasConformes  AS CantidadConforme,
    D.CantidadPiezasNoConformes AS CantidadNoConforme,
    D.Nota
FROM cp.DetPrdCorteT D
INNER JOIN cp.MaestroCatalogo   MCA ON MCA.Id = D.IdArticulo
INNER JOIN cp.TipoFabricacion   TF  ON TF.Id  = D.IdTipoFabricacion
INNER JOIN cp.MaestroCatalogo   MC  ON MC.Id  = D.IdDensidad
INNER JOIN cp.MaestroCatalogo   TB  ON TB.Id  = D.IdTipoBloque
WHERE D.PrdCorteTId IN (
    SELECT P.Id
    FROM cp.PrdCorteT P
    WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
      AND P.AprobadoGerencia = 1
);
";

                using (var multi = await connection.QueryMultipleAsync(sql, new { start, end }))
                {
                    var headers = (await multi.ReadAsync<PrdCorteTReporteDTO>()).ToList();
                    var detalles = (await multi.ReadAsync<DetalleCorteTDto>()).ToList();

                    foreach (var header in headers)
                    {
                        header.Detalles = detalles.Where(d => d.IdCorteT == header.Id).ToList();
                    }

                    return headers;
                }
            }
        }


        public async Task<IEnumerable<PrdIlKwangReporteDTO>> GetAllPrdIlKwangWithDetailsAsync(DateTime start, DateTime end)
        {
            // Validate DateTime parameters to ensure they're within SQL Server range
            start = ValidateSqlDateTime(start);
            end = ValidateSqlDateTime(end);

            using var connection = _context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var sql = @"
-- Encabezado: registros de PrdIlKwang con AprobadoGerencia = 1
SELECT
    P.Id,
    dbo.GetUserNamesFromIDs(P.IdUsuarios)        AS Operarios,
    
    dbo.GetArticulosTermoIsoPanelFromIDs(P.IdArticulo) as       Articulos,
    P.Fecha,
    M.Nombre                                     AS Maquina,
    P.HoraInicio,
    P.HoraFin,
    P.Cliente,
    P.NumeroPedido,
    TF.Descripcion                               AS TipoFabricacion,
    P.TiempoParo,
    P.Observaciones,
    dbo.GetUserNamesFromIDs(P.IdAprobadoSupervisor) AS Supervisor,
    dbo.GetUserNamesFromIDs(P.IdAprobadoGerencia)   AS JefeProd,

    -- Información de Bobina A (original)
    P.FabricanteBobinaA,
    P.CodigoBobinaA,
    UBA.Descripcion                              AS UbicacionBobinaA,
    CBA.Color                                    AS ColorBobinaA,

    -- Información de Bobina B (original)
    P.FabricanteBobinaB,
    P.CodigoBobinaB,
    ISNULL(UBB.Descripcion, 'N/A')               AS UbicacionBobinaB,
    ISNULL(CBB.Color, 'N/A')                     AS ColorBobinaB,

    -- Datos adicionales de Bobina A
    P.CalibreMmA,
    P.AnchoMmA,
    P.PesoInicialKgA,
    P.PesoFinalKgA,
    P.MetrosInicialA,
    P.MetrosFinalA,
    P.EspesorInicialCmA,
    P.EspesorFinalCmA,
    P.ConsumoBobinaKgA,
    P.PesoInicialA,
    P.CantidadUtilizadaA,
    P.PesoFinalA,
    P.VelocidadSuperiorA,
    P.VelocidadInferiorA,
    P.LoteA,
    P.VencimientoA,

    -- Datos adicionales de Bobina B
    P.CalibreMmB,
    P.AnchoMmB,
    P.PesoInicialKgB,
    P.PesoFinalKgB,
    P.MetrosInicialB,
    P.MetrosFinalB,
    P.EspesorInicialCmB,
    P.EspesorFinalCmB,
    P.ConsumoBobinaKgB,
    P.PesoInicialB,
    P.CantidadUtilizadaB,
    P.PesoFinalB,
    P.VelocidadSuperiorB,
    P.VelocidadInferiorB,
    P.LoteB,
    P.VencimientoB,

    -- Producción / Totales
    P.VelocidadMaquina,
    P.TotalProduccion,
    P.MetrosConDefecto,
    P.MermaM,
    P.MetrosAdicionales,
    P.PorcentajeMerma,
    P.PorcentajeDefecto,
    P.CantidadArranques

FROM cp.PrdIlKwang P
INNER JOIN cp.Maquinas            M  ON M.Id  = P.IdMaquina
INNER JOIN cp.TipoFabricacion     TF ON TF.Id = P.IdTipoFabricacion
INNER JOIN cp.UbicacionBobina     UBA ON UBA.Id = P.IdUbicacionBobinaA
INNER JOIN cp.ColoresBobinas      CBA ON CBA.Id = P.IdColorBobinaA
LEFT  JOIN cp.UbicacionBobina     UBB ON UBB.Id = P.IdUbicacionBobinaB
LEFT  JOIN cp.ColoresBobinas      CBB ON CBB.Id = P.IdColorBobinaB

WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
  AND P.AprobadoGerencia = 1;

-- Detalle: datos de DetPrdIlKwang
SELECT 
    D.PrdIlKwangId AS IdPrdIlKwang,
    D.Posicion,
    E.Valor AS Espesor,
    D.Cantidad,
    D.Medida,
    D.MetrosCuadrados,
    S.Descripcion AS Status,
    T.Descripcion AS Tipo
FROM cp.DetPrdIlKwang D
INNER JOIN cp.CatEspesor E ON E.Id = D.IdEspesor
INNER JOIN cp.CatalogoStatus S ON S.Id = D.IdStatus
INNER JOIN cp.CatalogoTipo T ON T.Id = D.IdTipo
WHERE D.PrdIlKwangId IN (
    SELECT P.Id 
    FROM cp.PrdIlKwang P 
    WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
      AND P.AprobadoGerencia = 1
);
";

            using var multi = await connection.QueryMultipleAsync(sql, new { start, end });

            // Leer encabezados
            var encabezados = (await multi.ReadAsync<PrdIlKwangReporteDTO>()).ToList();
            // Leer detalle de producción
            var detallesProduccion = (await multi.ReadAsync<DetalleIlKwangDto>()).ToList();

            // Asociar detalles a cada encabezado
            foreach (var encabezado in encabezados)
            {
                encabezado.DetallesProduccion = detallesProduccion
                    .Where(d => d.IdPrdIlKwang == encabezado.Id)
                    .ToList();
            }

            return encabezados;
        }

        public async Task<IEnumerable<PrdNeveraReporteDTO>> GetAllPrdNeveraWithDetailsAsync(DateTime start, DateTime end)
        {
            // Validate DateTime parameters to ensure they're within SQL Server range
            start = ValidateSqlDateTime(start);
            end = ValidateSqlDateTime(end);

            using (var connection = _context.Database.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var sql = @"
-- 1) Encabezados de nevera
SELECT 
    PN.Id,
    PN.TiempoParo,
    PN.IdTipoReporte,
    PN.IdUsuarios,
    dbo.GetUserNamesFromIDs(PN.IdUsuarios) AS Operarios,
    PN.IdMaquina,
    M.Nombre AS Maquina,
    PN.Fecha,
    PN.HoraInicio,
    PN.HoraFin,
    PN.Observaciones,
    PN.IdUsuarioCreacion,
    PN.FechaCreacion,
    PN.IdUsuarioActualizacion,
    PN.FechaActualizacion,
    PN.AprobadoSupervisor,
    PN.AprobadoGerencia,
    PN.IdAprobadoSupervisor,
    PN.IdAprobadoGerencia,
    dbo.GetUserNamesFromIDs(PN.IdAprobadoSupervisor) AS Supervisor,
    dbo.GetUserNamesFromIDs(PN.IdAprobadoGerencia) AS JefeProd
FROM cp.PrdNevera PN
INNER JOIN cp.Maquinas M ON M.Id = PN.IdMaquina
WHERE CAST(PN.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
 AND PN.AprobadoGerencia = 1;

-- 2) Detalles de cada nevera
SELECT 
    D.PrdNeveraId    AS IdNevera,
    D.Posicion,
    MC.Descripcion   AS Articulo,
    D.CantidadConforme,
    D.CantidadNoConforme,
    TF.Descripcion   AS TipoFabricacion,
    D.NumeroPedido
FROM cp.DetPrdNevera D
INNER JOIN cp.MaestroCatalogo   MC ON MC.Id = D.IdArticulo
INNER JOIN cp.TipoFabricacion   TF ON TF.Id = D.IdTipoFabricacion
WHERE  D.PrdNeveraId IN (
    SELECT P.Id 
    FROM cp.PrdNevera P 
    WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
      AND P.AprobadoGerencia = 1
);

";

                using (var multi = await connection.QueryMultipleAsync(sql, new { start, end }))
                {
                    // Leer encabezados
                    var headers = (await multi.ReadAsync<PrdNeveraReporteDTO>()).ToList();
                    // Leer todos los detalles
                    var detalles = (await multi.ReadAsync<DetalleNeveraDto>()).ToList();

                    // Asociar detalles
                    foreach (var header in headers)
                    {
                        header.Detalles = detalles
                            .Where(d => d.IdNevera == header.Id)
                            .ToList();
                    }

                    return headers;
                }
            }
        }

        public async Task<IEnumerable<PrdOtroReporteDTO>> GetAllPrdOtroWithDetailsAsync(DateTime start, DateTime end)
        {
            // Validate DateTime parameters to ensure they're within SQL Server range
            start = ValidateSqlDateTime(start);
            end = ValidateSqlDateTime(end);

            using (var connection = _context.Database.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var sql = @"
-- 1) Encabezados de PrdOtro
SELECT 
    PO.Id,
    PO.IdTipoReporte,
    PO.IdUsuarios,
    dbo.GetUserNamesFromIDs(PO.IdUsuarios) AS Operarios,
    PO.Fecha,
    PO.IdUsuarioCreacion,
    PO.FechaCreacion,
    PO.IdUsuarioActualizacion,
    PO.FechaActualizacion,
    PO.AprobadoSupervisor,
    PO.AprobadoGerencia,
    PO.IdAprobadoSupervisor,
    PO.IdAprobadoGerencia,
    dbo.GetUserNamesFromIDs(PO.IdAprobadoSupervisor) AS Supervisor,
    dbo.GetUserNamesFromIDs(PO.IdAprobadoGerencia) AS JefeProd
FROM cp.PrdOtro PO
WHERE CAST(PO.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
 AND PO.AprobadoGerencia = 1;

-- 2) Detalles de cada PrdOtro
SELECT 
    D.PrdOtroId      AS IdPrdOtro,
    D.Actividad,
    D.DescripcionProducto,
    TF.Descripcion   AS TipoFabricacion,
    D.NumeroPedido,
    D.Nota,
    D.Merma,
    D.Comentario,
    D.HoraInicio,
    D.HoraFin
FROM cp.DetPrdOtro D
INNER JOIN cp.TipoFabricacion   TF ON TF.Id = D.IdTipoFabricacion
WHERE  D.PrdOtroId IN (
    SELECT P.Id 
    FROM cp.PrdOtro P 
    WHERE CAST(P.Fecha AS DATE) BETWEEN CAST(@start AS DATE) AND CAST(@end AS DATE)
      AND P.AprobadoGerencia = 1
);

";

                using (var multi = await connection.QueryMultipleAsync(sql, new { start, end }))
                {
                    // Leer encabezados
                    var headers = (await multi.ReadAsync<PrdOtroReporteDTO>()).ToList();
                    // Leer todos los detalles
                    var detalles = (await multi.ReadAsync<DetalleOtroDto>()).ToList();

                    // Asociar detalles
                    foreach (var header in headers)
                    {
                        header.Detalles = detalles
                            .Where(d => d.IdPrdOtro == header.Id)
                            .ToList();
                    }

                    return headers;
                }
            }
        }
    }
}
