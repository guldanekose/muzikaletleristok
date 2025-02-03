using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using muzikaletleristok.Models;

namespace muzikaletleristok.Models;

public partial class MuzikaaletleristokContext : DbContext
{
    public MuzikaaletleristokContext()
    {
    }

    public MuzikaaletleristokContext(DbContextOptions<MuzikaaletleristokContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CSHBGF1;initial Catalog=muzikaaletleristok;trusted_connection=yes; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<muzikaletleristok.Models.Kategoriler> Kategoriler { get; set; } = default!;

public DbSet<muzikaletleristok.Models.MuzikAletleri> MuzikAletleri { get; set; } = default!;

public DbSet<muzikaletleristok.Models.Kullanicilar> Kullanicilar { get; set; } = default!;

public DbSet<muzikaletleristok.Models.Satislar> Satislar { get; set; } = default!;

public DbSet<muzikaletleristok.Models.StokHareketleri> StokHareketleri { get; set; } = default!;

public DbSet<muzikaletleristok.Models.Tedarikciler> Tedarikciler { get; set; } = default!;

public DbSet<muzikaletleristok.Models.TedarikSiparisleri> TedarikSiparisleri { get; set; } = default!;

public DbSet<muzikaletleristok.Models.Logincs> Logincs { get; set; } = default!;
}
