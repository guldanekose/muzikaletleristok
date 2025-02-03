using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace muzikaletleristok.Models;

[Table("TedarikSiparisleri")]
public partial class TedarikSiparisleri
{
    [Key]
    [Column("TedarikSiparisID")]
    public int TedarikSiparisId { get; set; }

    [Column("TedarikciID")]
    public int? TedarikciId { get; set; }

    [Column("MuzikAletiID")]
    public int? MuzikAletiId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SiparisTarihi { get; set; }

    public int? Miktar { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? ToplamFiyat { get; set; }

    [ForeignKey("MuzikAletiId")]
    [InverseProperty("TedarikSiparisleris")]
    public virtual MuzikAletleri? MuzikAleti { get; set; }

    [ForeignKey("TedarikciId")]
    [InverseProperty("TedarikSiparisleris")]
    public virtual Tedarikciler? Tedarikci { get; set; }
}
