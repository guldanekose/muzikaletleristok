using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace muzikaletleristok.Models;

[Table("MuzikAletleri")]
public partial class MuzikAletleri
{
    [Key]
    [Column("MuzikAletiID")]
    public int MuzikAletiId { get; set; }

    [Column("KategoriID")]
    public int? KategoriId { get; set; }

    [StringLength(100)]
    public string MuzikAletiAdi { get; set; } = null!;

    [StringLength(100)]
    public string? Marka { get; set; }

    [StringLength(100)]
    public string? Model { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Fiyat { get; set; }

    public int? StokMiktari { get; set; }

    [StringLength(100)]
    public string? MuzikAletiPhoto { get; set; }
    [NotMapped]
    [DisplayName("Upload Image File")]
    public IFormFile? ImageFile { get; set; }

    [ForeignKey("KategoriId")]
    [InverseProperty("MuzikAletleris")]
    public virtual Kategoriler? Kategori { get; set; }

    [InverseProperty("MuzikAleti")]
    public virtual ICollection<Satislar> Satislars { get; set; } = new List<Satislar>();

    [InverseProperty("MuzikAleti")]
    public virtual ICollection<StokHareketleri> StokHareketleris { get; set; } = new List<StokHareketleri>();

    [InverseProperty("MuzikAleti")]
    public virtual ICollection<TedarikSiparisleri> TedarikSiparisleris { get; set; } = new List<TedarikSiparisleri>();
}
