using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace muzikaletleristok.Models;

[Table("Satislar")]
public partial class Satislar
{
    [Key]
    [Column("SatisID")]
    public int SatisId { get; set; }

    [Column("MuzikAletiID")]
    public int? MuzikAletiId { get; set; }

    [Column("KullaniciID")]
    public int? KullaniciId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SatisTarihi { get; set; }

    public int? Miktar { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? ToplamFiyat { get; set; }

    [ForeignKey("KullaniciId")]
    [InverseProperty("Satislars")]
    public virtual Kullanicilar? Kullanici { get; set; }

    [ForeignKey("MuzikAletiId")]
    [InverseProperty("Satislars")]
    public virtual MuzikAletleri? MuzikAleti { get; set; }
}
