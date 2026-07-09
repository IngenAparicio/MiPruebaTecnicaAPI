using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MiPruebaTecnicaAccess.entities;

namespace MiPruebaTecnicaAccess.context;

public partial class PruebaTecnicaDbContext : DbContext
{
    public PruebaTecnicaDbContext(DbContextOptions<PruebaTecnicaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RegistroAtencion> RegistroAtencion { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegistroAtencion>(entity =>
        {
            entity.HasKey(e => e.IdAtencion).HasName("PK__Registro__223C362EC89B2C5A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
