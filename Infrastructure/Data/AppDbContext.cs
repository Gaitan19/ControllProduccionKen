using System;
using System.Collections.Generic;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnchoBobina> AnchoBobinas { get; set; }

    public virtual DbSet<CatEspesor> CatEspesors { get; set; }

    public virtual DbSet<CatLamina> CatLaminas { get; set; }

    public virtual DbSet<CatPantografo> CatPantografos { get; set; }

    public virtual DbSet<CatProdTermoIsoPanel> CatProdTermoIsoPanels { get; set; }

    public virtual DbSet<CatTipoMalla> CatTipoMallas { get; set; }

    public virtual DbSet<CatalogoAccesorio> CatalogoAccesorios { get; set; }

    public virtual DbSet<CatalogoBloque> CatalogoBloques { get; set; }

    public virtual DbSet<CatalogoCercha> CatalogoCerchas { get; set; }

    public virtual DbSet<CatalogoMallasCovintec> CatalogoMallasCovintecs { get; set; }

    public virtual DbSet<CatalogoPanelesCovintec> CatalogoPanelesCovintecs { get; set; }

    public virtual DbSet<CatalogoPanelesPch> CatalogoPanelesPches { get; set; }

    public virtual DbSet<CatalogoStatus> CatalogoStatuses { get; set; }

    public virtual DbSet<CatalogoTipo> CatalogoTipos { get; set; }

    public virtual DbSet<CatalogosPermitidosPorReporte> CatalogosPermitidosPorReportes { get; set; }

    public virtual DbSet<ColoresBobina> ColoresBobinas { get; set; }

    public virtual DbSet<DetAlambrePrdCerchaCovintec> DetAlambrePrdCerchaCovintecs { get; set; }

    public virtual DbSet<DetAlambrePrdMallaCovintec> DetAlambrePrdMallaCovintecs { get; set; }

    public virtual DbSet<DetAlambrePrdPanelesCovintec> DetAlambrePrdPanelesCovintecs { get; set; }

    public virtual DbSet<DetPrdAccesorio> DetPrdAccesorios { get; set; }

    public virtual DbSet<DetPrdBloque> DetPrdBloques { get; set; }

    public virtual DbSet<DetPrdCerchaCovintec> DetPrdCerchaCovintecs { get; set; }

    public virtual DbSet<DetPrdCorteP> DetPrdCortePs { get; set; }

    public virtual DbSet<DetPrdCorteT> DetPrdCorteTs { get; set; }

    public virtual DbSet<DetPrdIlKwang> DetPrdIlKwangs { get; set; }

    public virtual DbSet<DetPrdMallaCovintec> DetPrdMallaCovintecs { get; set; }

    public virtual DbSet<DetPrdNevera> DetPrdNeveras { get; set; }

    public virtual DbSet<DetPrdOtro> DetPrdOtros { get; set; }

    public virtual DbSet<DetPrdPaneladoraPch> DetPrdPaneladoraPches { get; set; }

    public virtual DbSet<DetPrdPanelesCovintec> DetPrdPanelesCovintecs { get; set; }

    public virtual DbSet<DetPrdPchMaquinaA> DetPrdPchMaquinaAs { get; set; }

    public virtual DbSet<DetPrdPchMaquinaB> DetPrdPchMaquinaBs { get; set; }

    public virtual DbSet<DetPrdPchMaquinaC> DetPrdPchMaquinaCs { get; set; }

    public virtual DbSet<DetPrdpreExpansion> DetPrdpreExpansions { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<LineaProduccion> LineaProduccions { get; set; }

    public virtual DbSet<MaestroCatalogo> MaestroCatalogos { get; set; }

    public virtual DbSet<Maquina> Maquinas { get; set; }

    public virtual DbSet<PrdAccesorio> PrdAccesorios { get; set; }

    public virtual DbSet<PrdBloque> PrdBloques { get; set; }

    public virtual DbSet<PrdCerchaCovintec> PrdCerchaCovintecs { get; set; }

    public virtual DbSet<PrdCorteP> PrdCortePs { get; set; }

    public virtual DbSet<PrdCorteT> PrdCorteTs { get; set; }

    public virtual DbSet<PrdIlKwang> PrdIlKwangs { get; set; }

    public virtual DbSet<PrdMallaCovintec> PrdMallaCovintecs { get; set; }

    public virtual DbSet<PrdMallaPch> PrdMallaPches { get; set; }

    public virtual DbSet<PrdNevera> PrdNeveras { get; set; }

    public virtual DbSet<PrdOtro> PrdOtros { get; set; }

    public virtual DbSet<PrdPaneladoraPch> PrdPaneladoraPches { get; set; }

    public virtual DbSet<PrdPanelesCovintec> PrdPanelesCovintecs { get; set; }

    public virtual DbSet<PrdpreExpansion> PrdpreExpansions { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<SubDetPrdBloque> SubDetPrdBloques { get; set; }

    public virtual DbSet<TipoFabricacion> TipoFabricacions { get; set; }

    public virtual DbSet<UbicacionBobina> UbicacionBobinas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnchoBobina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AnchoBob__3214EC07F0126C54");

            entity.ToTable("AnchoBobina", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<CatEspesor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CatEspes__3214EC070A714CE2");

            entity.ToTable("CatEspesor", "cp");

            entity.Property(e => e.Valor)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CatLamina>(entity =>
        {
            entity.ToTable("CatLaminas", "cp");

            entity.Property(e => e.CodigoArticulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CatPantografo>(entity =>
        {
            entity.ToTable("CatPantografo", "cp");

            entity.Property(e => e.CodigoArticulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CatProdTermoIsoPanel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CatalogoPanelesCovintec");

            entity.ToTable("CatProdTermoIsoPanel", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CodigoArticulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CatTipoMalla>(entity =>
        {
            entity.ToTable("CatTipoMalla", "cp");

            entity.HasIndex(e => e.Cuadricula, "UQ_CatTipoMalla_Cuadricula").IsUnique();

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Cuadricula).HasMaxLength(100);
            entity.Property(e => e.PesoPorMts2).HasMaxLength(50);
        });

        modelBuilder.Entity<CatalogoAccesorio>(entity =>
        {
            entity.ToTable("CatalogoAccesorios", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CodigoArticulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FamiliaProductos)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.CatalogoAccesorios)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CatalogoBloque>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC078305E86A");

            entity.ToTable("CatalogoBloques", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Bloque)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CubicajeM3).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Medidas)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CatalogoCercha>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC07BB4888BA");

            entity.ToTable("CatalogoCercha", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.AreaM2).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.CodigoArticulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.EspesorMetros).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.LongitudMetros).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdLineaProduccionNavigation).WithMany(p => p.CatalogoCerchas)
                .HasForeignKey(d => d.IdLineaProduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogoCercha_LineaProduccion");
        });

        modelBuilder.Entity<CatalogoMallasCovintec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC0761F9603A");

            entity.ToTable("CatalogoMallasCovintec", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CodigoArticulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LongitudCentimetros).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdLineaProduccionNavigation).WithMany(p => p.CatalogoMallasCovintecs)
                .HasForeignKey(d => d.IdLineaProduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogoMallasCovintec_LineaProduccion");
        });

        modelBuilder.Entity<CatalogoPanelesCovintec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC070C1CCFCA");

            entity.ToTable("CatalogoPanelesCovintec", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CodigoArticulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Mts2PorPanel).HasColumnType("decimal(10, 4)");

            entity.HasOne(d => d.IdLineaProduccionNavigation).WithMany(p => p.CatalogoPanelesCovintecs)
                .HasForeignKey(d => d.IdLineaProduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogoPanelesCovintec_LineaProduccion");
        });

        modelBuilder.Entity<CatalogoPanelesPch>(entity =>
        {
            entity.ToTable("CatalogoPanelesPCH", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CodigoArticulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FamiliaProducto)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CatalogoStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC072BC8E676");

            entity.ToTable("CatalogoStatus", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CatalogoTipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC0720BB5CEE");

            entity.ToTable("CatalogoTipo", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CatalogosPermitidosPorReporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC078125FB99");

            entity.ToTable("CatalogosPermitidosPorReporte", "cp");

            entity.Property(e => e.Catalogo).HasMaxLength(150);
        });

        modelBuilder.Entity<ColoresBobina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ColoresB__3214EC079AEFF819");

            entity.ToTable("ColoresBobinas", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ral)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("RAL");
        });

        modelBuilder.Entity<DetAlambrePrdCerchaCovintec>(entity =>
        {
            entity.ToTable("DetAlambrePrdCerchaCovintec", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.PesoAlambre).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdCerchaNavigation).WithMany(p => p.DetAlambrePrdCerchaCovintecs)
                .HasForeignKey(d => d.IdCercha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetAlambrePrdCerchaCovintec_PrdCerchaCovintec");
        });

        modelBuilder.Entity<DetAlambrePrdMallaCovintec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetAlamb__3214EC078BCE7449");

            entity.ToTable("DetAlambrePrdMallaCovintec", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.PesoAlambre).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdMallaNavigation).WithMany(p => p.DetAlambrePrdMallaCovintecs)
                .HasForeignKey(d => d.IdMalla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetAlambrePrdMallaCovintec_Principal");
        });

        modelBuilder.Entity<DetAlambrePrdPanelesCovintec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetAlamb__3214EC075E4EAC6A");

            entity.ToTable("DetAlambrePrdPanelesCovintec", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.PesoAlambre).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdPanelNavigation).WithMany(p => p.DetAlambrePrdPanelesCovintecs)
                .HasForeignKey(d => d.IdPanel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetAlambrePrdPanelesCovintec_Principal");
        });

        modelBuilder.Entity<DetPrdAccesorio>(entity =>
        {
            entity.ToTable("DetPrdAccesorios", "cp");

            entity.HasIndex(e => e.IdAnchoBobina, "IX_DetPrdAcc_IdAnchoBobina").HasFilter("([IdAnchoBobina] IS NOT NULL)");

            entity.HasIndex(e => e.IdCalibre, "IX_DetPrdAcc_IdCalibre").HasFilter("([IdCalibre] IS NOT NULL)");

            entity.HasIndex(e => e.IdMallaCovintec, "IX_DetPrdAcc_IdMallaCovintec").HasFilter("([IdMallaCovintec] IS NOT NULL)");

            entity.HasIndex(e => e.IdTipoMallaPch, "IX_DetPrdAcc_IdTipoMallaPCH").HasFilter("([IdTipoMallaPCH] IS NOT NULL)");

            entity.HasIndex(e => e.PrdAccesoriosId, "IX_DetPrdAcc_PrdAccesoriosId");

            entity.Property(e => e.CantidadBobinaKg).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.CantidadCalibreKg).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.CantidadPchKg).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdTipoMallaPch).HasColumnName("IdTipoMallaPCH");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);

            entity.HasOne(d => d.IdAnchoBobinaNavigation).WithMany(p => p.DetPrdAccesorios)
                .HasForeignKey(d => d.IdAnchoBobina)
                .HasConstraintName("FK_DetPrdAccesorios_AnchoBobina");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetPrdAccesorios)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdAccesorios_CatalogoAccesorios");

            entity.HasOne(d => d.IdCalibreNavigation).WithMany(p => p.DetPrdAccesorioIdCalibreNavigations)
                .HasForeignKey(d => d.IdCalibre)
                .HasConstraintName("FK_DetPrdAccesorios_MaestroCatalogo1");

            entity.HasOne(d => d.IdMallaCovintecNavigation).WithMany(p => p.DetPrdAccesorios)
                .HasForeignKey(d => d.IdMallaCovintec)
                .HasConstraintName("FK_DetPrdAccesorios_CatalogoMallasCovintec");

            entity.HasOne(d => d.IdTipoArticuloNavigation).WithMany(p => p.DetPrdAccesorioIdTipoArticuloNavigations)
                .HasForeignKey(d => d.IdTipoArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdAccesorios_MaestroCatalogo");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdAccesorios)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdAccesorios_TipoFabricacion");

            entity.HasOne(d => d.IdTipoMallaPchNavigation).WithMany(p => p.DetPrdAccesorios)
                .HasForeignKey(d => d.IdTipoMallaPch)
                .HasConstraintName("FK_DetPrdAccesorios_CatTipoMalla");
        });

        modelBuilder.Entity<DetPrdBloque>(entity =>
        {
            entity.ToTable("DetPrdBloques", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.ProduccionDia).HasColumnType("decimal(10, 1)");

            entity.HasOne(d => d.IdCatBloqueNavigation).WithMany(p => p.DetPrdBloques)
                .HasForeignKey(d => d.IdCatBloque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdBloques_CatalogoBloques");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.DetPrdBloques)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdBloques_Maquinas");

            entity.HasOne(d => d.PrdBloque).WithMany(p => p.DetPrdBloques)
                .HasForeignKey(d => d.PrdBloqueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdBloques_PrdBloques");
        });

        modelBuilder.Entity<DetPrdCerchaCovintec>(entity =>
        {
            entity.ToTable("DetPrdCerchaCovintec", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.ProduccionDia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetPrdCerchaCovintecs)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCerchaCovintec_CatalogoCercha");

            entity.HasOne(d => d.IdCerchaNavigation).WithMany(p => p.DetPrdCerchaCovintecs)
                .HasForeignKey(d => d.IdCercha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCerchaCovintec_PrdCerchaCovintec");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdCerchaCovintecs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCerchaCovintec_TipoFabricacion");
        });

        modelBuilder.Entity<DetPrdCorteP>(entity =>
        {
            entity.ToTable("DetPrdCorteP", "cp");

            entity.Property(e => e.CantidadPiezasConformes).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.CantidadPiezasNoConformes).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Nota)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PrdCodigoBloque)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PrdCortePid).HasColumnName("PrdCortePId");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetPrdCortePs)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCorteP_CatPantografo");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdCortePs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCorteP_CatalogoTipo");

            entity.HasOne(d => d.PrdCorteP).WithMany(p => p.DetPrdCortePs)
                .HasForeignKey(d => d.PrdCortePid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCorteP_PrdCorteP");
        });

        modelBuilder.Entity<DetPrdCorteT>(entity =>
        {
            entity.ToTable("DetPrdCorteT", "cp");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CantidadPiezasConformes).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.CantidadPiezasNoConformes).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Nota)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PrdCodigoBloque)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PrdCorteTid).HasColumnName("PrdCorteTId");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.DetPrdCorteT)
                .HasForeignKey<DetPrdCorteT>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCorteT_CatLaminas");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdCorteTs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCorteT_CatalogoTipo");

            entity.HasOne(d => d.PrdCorteT).WithMany(p => p.DetPrdCorteTs)
                .HasForeignKey(d => d.PrdCorteTid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdCorteT_PrdCorteT");
        });

        modelBuilder.Entity<DetPrdIlKwang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetPrdIl__3214EC078979E06C");

            entity.ToTable("DetPrdIlKwang", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Medida).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MetrosCuadrados)
                .HasComputedColumnSql("(CONVERT([decimal](10,2),[Cantidad])*[Medida])", true)
                .HasColumnType("decimal(21, 4)");

            entity.HasOne(d => d.IdEspesorNavigation).WithMany(p => p.DetPrdIlKwangs)
                .HasForeignKey(d => d.IdEspesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdIlKwangDetalle_PrdIlKwang_Spesor");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.DetPrdIlKwangs)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdIlKwang_DetPrdIlKwang_status");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.DetPrdIlKwangs)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdIlKwang_DetPrdIlKwang_tipo");
        });

        modelBuilder.Entity<DetPrdMallaCovintec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetPrdMa__3214EC07YYYYYYYY");

            entity.ToTable("DetPrdMallaCovintec", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.ProduccionDia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetPrdMallaCovintecs)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdMallaCovintec_Catalogo");

            entity.HasOne(d => d.IdMallaNavigation).WithMany(p => p.DetPrdMallaCovintecs)
                .HasForeignKey(d => d.IdMalla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdMallaCovintec_Principal");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdMallaCovintecs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdMallaCovintec_TipoFabricacion");
        });

        modelBuilder.Entity<DetPrdNevera>(entity =>
        {
            entity.ToTable("DetPrdNevera", "cp");

            entity.Property(e => e.CantidadConforme).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.CantidadNoConforme).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetPrdNeveras)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdNevera_MaestroCatalogo");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdNeveras)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdNevera_TipoFabricacion");

            entity.HasOne(d => d.PrdNevera).WithMany(p => p.DetPrdNeveras)
                .HasForeignKey(d => d.PrdNeveraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdNevera_PrdNevera");
        });

        modelBuilder.Entity<DetPrdOtro>(entity =>
        {
            entity.ToTable("DetPrdOtro", "cp");

            entity.Property(e => e.Actividad).HasMaxLength(4000);
            entity.Property(e => e.Comentario).HasMaxLength(4000);
            entity.Property(e => e.DescripcionProducto).HasMaxLength(4000);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Merma).HasMaxLength(4000);
            entity.Property(e => e.Nota).HasMaxLength(4000);

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdOtros)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdOtro_TipoFabricacion");

            entity.HasOne(d => d.PrdOtro).WithMany(p => p.DetPrdOtros)
                .HasForeignKey(d => d.PrdOtroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdOtro_PrdOtro");
        });

        modelBuilder.Entity<DetPrdPaneladoraPch>(entity =>
        {
            entity.ToTable("DetPrdPaneladoraPch", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Longitud).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MermaAlambreKg).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Mts2PorPanel).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.PesoAlambreKg).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetPrdPaneladoraPches)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cp_DetPrdPaneladoraPch_CatPanelesCovintec");

            entity.HasOne(d => d.IdArticulo1).WithMany(p => p.DetPrdPaneladoraPches)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPaneladoraPch_CatalogoPanelesPCH");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdPaneladoraPches)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cp_DetPrdPaneladoraPch_TipoFabricacion");

            entity.HasOne(d => d.PrdPaneladoraPch).WithMany(p => p.DetPrdPaneladoraPches)
                .HasForeignKey(d => d.PrdPaneladoraPchId)
                .HasConstraintName("FK_cp_DetPrdPaneladoraPch_PrdPaneladoraPch");
        });

        modelBuilder.Entity<DetPrdPanelesCovintec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetPrdPa__3214EC07BC260943");

            entity.ToTable("DetPrdPanelesCovintec", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.ProduccionDia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetPrdPanelesCovintecs)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPanelesCovintec_CatalogoPanelesCovintec");

            entity.HasOne(d => d.IdPanelNavigation).WithMany(p => p.DetPrdPanelesCovintecs)
                .HasForeignKey(d => d.IdPanel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPanelesCovintec_Principal");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdPanelesCovintecs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPanelesCovintec_TipoFabricacion");
        });

        modelBuilder.Entity<DetPrdPchMaquinaA>(entity =>
        {
            entity.ToTable("DetPrdPchMaquinaA", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.HilosTransversalesUn).HasColumnName("HilosTransversalesUN");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Longitud).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MermaHilosTransversalesKg).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.PesoAlambreKgA).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Produccion).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.DetPrdPchMaquinaAs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaA_Maquinas");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdPchMaquinaAs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaA_TipoFabricacion");

            entity.HasOne(d => d.PrdMallaPch).WithMany(p => p.DetPrdPchMaquinaAs)
                .HasForeignKey(d => d.PrdMallaPchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaA_PrdMallaPch");
        });

        modelBuilder.Entity<DetPrdPchMaquinaB>(entity =>
        {
            entity.ToTable("DetPrdPchMaquinaB", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.HilosLongitudinalesUn).HasColumnName("HilosLongitudinalesUN");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Longitud).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MermaHilosLongitudinalesKg).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.PesoAlambreKgB).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Produccion).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.DetPrdPchMaquinaBs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaB_Maquinas");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdPchMaquinaBs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaB_TipoFabricacion");

            entity.HasOne(d => d.PrdMallaPch).WithMany(p => p.DetPrdPchMaquinaBs)
                .HasForeignKey(d => d.PrdMallaPchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaB_PrdMallaPch");
        });

        modelBuilder.Entity<DetPrdPchMaquinaC>(entity =>
        {
            entity.ToTable("DetPrdPchMaquinaC", "cp");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Longitud).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MermaMallasKg).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Produccion).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.DetPrdPchMaquinaCs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaC_Maquinas");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.DetPrdPchMaquinaCs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaC_TipoFabricacion");

            entity.HasOne(d => d.IdTipoMallaNavigation).WithMany(p => p.DetPrdPchMaquinaCs)
                .HasForeignKey(d => d.IdTipoMalla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaC_CatTipoMalla");

            entity.HasOne(d => d.PrdMallaPch).WithMany(p => p.DetPrdPchMaquinaCs)
                .HasForeignKey(d => d.PrdMallaPchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPrdPchMaquinaC_PrdMallaPch");
        });

        modelBuilder.Entity<DetPrdpreExpansion>(entity =>
        {
            entity.ToTable("DetPrdpreExpansion", "cp");

            entity.Property(e => e.Densidad).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.PesoBatchGr).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.PresionPsi).HasColumnName("PresionPSI");

            entity.HasOne(d => d.PrdpreExpansion).WithMany(p => p.DetPrdpreExpansions)
                .HasForeignKey(d => d.PrdpreExpansionId)
                .HasConstraintName("FK_DetPrdpreExpansion_PrdpreExpansion");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ErrorLog");

            entity.ToTable("ErrorLogs", "cp");

            entity.Property(e => e.CorrelationId).HasMaxLength(100);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(45)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Level).HasMaxLength(32);
            entity.Property(e => e.Message).HasMaxLength(4000);
            entity.Property(e => e.QueryString).HasMaxLength(2048);
            entity.Property(e => e.RequestMethod).HasMaxLength(16);
            entity.Property(e => e.RequestPath).HasMaxLength(2048);
            entity.Property(e => e.TimeUtc)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserAgent).HasMaxLength(512);
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<LineaProduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LineaPro__3214EC07A41B3118");

            entity.ToTable("LineaProduccion", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MaestroCatalogo>(entity =>
        {
            entity.ToTable("MaestroCatalogo", "cp");

            entity.Property(e => e.Codigo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Maquina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Maquinas__3214EC0785E406CB");

            entity.ToTable("Maquinas", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PrdAccesorio>(entity =>
        {
            entity.ToTable("PrdAccesorios", "cp");

            entity.HasIndex(e => e.IdMaquina, "IX_PrdAccesorios_IdMaquina");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.MermaBobinasKg).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.MermaLitewallKg).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.MermaMallaCovintecKg).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.MermaMallaPchKg).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.Observaciones).HasMaxLength(4000);

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdAccesorios)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdAccesorios_Maquinas");
        });

        modelBuilder.Entity<PrdBloque>(entity =>
        {
            entity.ToTable("PrdBloques", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
        });

        modelBuilder.Entity<PrdCerchaCovintec>(entity =>
        {
            entity.ToTable("PrdCerchaCovintec", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdTipoReporte).HasDefaultValueSql("((7))");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.MermaAlambre).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.TiempoParo).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdCerchaCovintecs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdCerchaCovintec_Maquinas");
        });

        modelBuilder.Entity<PrdCorteP>(entity =>
        {
            entity.ToTable("PrdCorteP", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.TiempoParo).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdCortePs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdCorteP_Maquinas");
        });

        modelBuilder.Entity<PrdCorteT>(entity =>
        {
            entity.ToTable("PrdCorteT", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.TiempoParo).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdCorteTs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdCorteT_Maquinas");
        });

        modelBuilder.Entity<PrdIlKwang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PrdIlKwa__3214EC07DFC0EC0E");

            entity.ToTable("PrdIlKwang", "cp");

            entity.Property(e => e.CalibreMmA).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CalibreMmB).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CantidadUtilizadaA).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CantidadUtilizadaB).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Cliente).HasMaxLength(500);
            entity.Property(e => e.CodigoBobinaA).HasMaxLength(200);
            entity.Property(e => e.CodigoBobinaB).HasMaxLength(200);
            entity.Property(e => e.ConsumoBobinaKgA)
                .HasComputedColumnSql("(CONVERT([decimal](10,2),[PesoInicialKgA])-CONVERT([decimal](10,2),[PesoFinalKgA]))", true)
                .HasColumnType("decimal(11, 2)");
            entity.Property(e => e.ConsumoBobinaKgB)
                .HasComputedColumnSql("(CONVERT([decimal](10,2),[PesoInicialKgB])-CONVERT([decimal](10,2),[PesoFinalKgB]))", true)
                .HasColumnType("decimal(11, 2)");
            entity.Property(e => e.FabricanteBobinaA).HasMaxLength(200);
            entity.Property(e => e.FabricanteBobinaB).HasMaxLength(200);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.LoteA).HasMaxLength(150);
            entity.Property(e => e.LoteB).HasMaxLength(100);
            entity.Property(e => e.MermaM).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MetrosAdicionales).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MetrosConDefecto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.PesoFinalA).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PesoFinalB).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PesoInicialA).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PesoInicialB).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PorcentajeDefecto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PorcentajeMerma).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TiempoParo).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.TotalProduccion).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VelocidadInferiorA).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.VelocidadInferiorB).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.VelocidadSuperiorA).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.VelocidadSuperiorB).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.VencimientoA).HasColumnType("date");
            entity.Property(e => e.VencimientoB).HasColumnType("date");

            entity.HasOne(d => d.IdAnchoBobinaANavigation).WithMany(p => p.PrdIlKwangIdAnchoBobinaANavigations)
                .HasForeignKey(d => d.IdAnchoBobinaA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdIlKwang_AnchoBobina");

            entity.HasOne(d => d.IdAnchoBobinaBNavigation).WithMany(p => p.PrdIlKwangIdAnchoBobinaBNavigations)
                .HasForeignKey(d => d.IdAnchoBobinaB)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdIlKwang_PrdIlKwang_anchoBobinaB");

            entity.HasOne(d => d.IdColorBobinaANavigation).WithMany(p => p.PrdIlKwangIdColorBobinaANavigations)
                .HasForeignKey(d => d.IdColorBobinaA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdIlKwang_PrdIlKwang_coloresBobina");

            entity.HasOne(d => d.IdColorBobinaBNavigation).WithMany(p => p.PrdIlKwangIdColorBobinaBNavigations)
                .HasForeignKey(d => d.IdColorBobinaB)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdIlKwang_PrdIlKwang_coloresBobinaB");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdIlKwangs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdIlKwang_Maquinas");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.PrdIlKwangs)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdIlKwang_PrdIlKwang_tipoFabricacion");

            entity.HasOne(d => d.IdUbicacionBobinaANavigation).WithMany(p => p.PrdIlKwangs)
                .HasForeignKey(d => d.IdUbicacionBobinaA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdIlKwang_UbicacionBobina");
        });

        modelBuilder.Entity<PrdMallaCovintec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PrdMalla__3214EC07XXXXXXXX");

            entity.ToTable("PrdMallaCovintec", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.MermaAlambre).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.TiempoParo).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdMallaCovintecs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdMallaCovintec_Maquinas");

            entity.HasOne(d => d.IdTipoReporteNavigation).WithMany(p => p.PrdMallaCovintecs)
                .HasForeignKey(d => d.IdTipoReporte)
                .HasConstraintName("FK_PrdMallaCovintec_Reporte");
        });

        modelBuilder.Entity<PrdMallaPch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PrdPaneladoraPch");

            entity.ToTable("PrdMallaPch", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.ProduccionDia).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PrdNevera>(entity =>
        {
            entity.ToTable("PrdNevera", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.TiempoParo).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdNeveras)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdNevera_Maquinas");
        });

        modelBuilder.Entity<PrdOtro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PrdOtros");

            entity.ToTable("PrdOtro", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
        });

        modelBuilder.Entity<PrdPaneladoraPch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_cp_PrdPaneladoraPch");

            entity.ToTable("PrdPaneladoraPch", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.ProduccionDia).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.TiempoParo).HasColumnType("decimal(10, 1)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdPaneladoraPches)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cp_PrdPaneladoraPch_Maquinas");
        });

        modelBuilder.Entity<PrdPanelesCovintec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PrdPanel__3214EC078A18F682");

            entity.ToTable("PrdPanelesCovintec", "cp");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.MermaAlambre).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.TiempoParo).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.PrdPanelesCovintecs)
                .HasForeignKey(d => d.IdMaquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrdPanelesCovintec_Maquinas");

            entity.HasOne(d => d.IdTipoReporteNavigation).WithMany(p => p.PrdPanelesCovintecs)
                .HasForeignKey(d => d.IdTipoReporte)
                .HasConstraintName("FK_PrdPanelesCovintec_Reporte");
        });

        modelBuilder.Entity<PrdpreExpansion>(entity =>
        {
            entity.ToTable("PrdpreExpansion", "cp");

            entity.Property(e => e.CodigoSaco).HasMaxLength(150);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaProduccion).HasColumnType("date");
            entity.Property(e => e.IdAprobadoGerencia).HasMaxLength(450);
            entity.Property(e => e.IdAprobadoSupervisor).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Lote).HasMaxLength(150);
            entity.Property(e => e.Observaciones).HasMaxLength(4000);
            entity.Property(e => e.PresionCaldera).HasMaxLength(150);
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reporte__3214EC077329D102");

            entity.ToTable("Reporte", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TablaReporte)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdLineaProduccionNavigation).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.IdLineaProduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporte_LineaProduccion");
        });

        modelBuilder.Entity<SubDetPrdBloque>(entity =>
        {
            entity.ToTable("SubDetPrdBloques", "cp");

            entity.Property(e => e.CodigoBloque)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(450);
            entity.Property(e => e.IdUsuarioCreacion).HasMaxLength(450);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Peso).HasColumnType("decimal(10, 1)");

            entity.HasOne(d => d.DetPrdBloques).WithMany(p => p.SubDetPrdBloques)
                .HasForeignKey(d => d.DetPrdBloquesId)
                .HasConstraintName("FK_SubDetPrdBloques_DetPrdBloques");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.SubDetPrdBloques)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubDetPrdBloques_CatalogoBloques");

            entity.HasOne(d => d.IdDensidadNavigation).WithMany(p => p.SubDetPrdBloqueIdDensidadNavigations)
                .HasForeignKey(d => d.IdDensidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubDetPrdBloques_MaestroCatalogo");

            entity.HasOne(d => d.IdTipoBloqueNavigation).WithMany(p => p.SubDetPrdBloqueIdTipoBloqueNavigations)
                .HasForeignKey(d => d.IdTipoBloque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubDetPrdBloques_MaestroCatalogo1");

            entity.HasOne(d => d.IdTipoFabricacionNavigation).WithMany(p => p.SubDetPrdBloques)
                .HasForeignKey(d => d.IdTipoFabricacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubDetPrdBloques_TipoFabricacion");
        });

        modelBuilder.Entity<TipoFabricacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoFabr__3214EC070F68A431");

            entity.ToTable("TipoFabricacion", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UbicacionBobina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ubicacio__3214EC0737C77406");

            entity.ToTable("UbicacionBobina", "cp");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
