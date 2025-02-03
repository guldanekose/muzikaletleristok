using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace muzikaletleristok.Models;

[Table("Kullanicilar")]
public partial class Kullanicilar
{
    [Key]
    [Column("KullaniciID")]
    public int KullaniciId { get; set; }

    [StringLength(50)]
    public string KullaniciAdi { get; set; } = null!;

    [StringLength(50)]
    public string Sifre { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [InverseProperty("Kullanici")]
    public virtual ICollection<Satislar> Satislars { get; set; } = new List<Satislar>();
}
