using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace muzikaletleristok.Models;

[Table("StokHareketleri")]
public partial class StokHareketleri
{
    [Key]
    [Column("StokHareketID")]
    public int StokHareketId { get; set; }

    [Column("MuzikAletiID")]
    public int? MuzikAletiId { get; set; }

    [StringLength(50)]
    public string? HareketTipi { get; set; }

    public int? Miktar { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HareketTarihi { get; set; }

    [ForeignKey("MuzikAletiId")]
    [InverseProperty("StokHareketleris")]
    public virtual MuzikAletleri? MuzikAleti { get; set; }
}
