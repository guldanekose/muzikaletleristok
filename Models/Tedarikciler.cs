using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace muzikaletleristok.Models;

[Table("Tedarikciler")]
public partial class Tedarikciler
{
    [Key]
    [Column("TedarikciID")]
    public int TedarikciId { get; set; }

    [StringLength(100)]
    public string? TedarikciAdi { get; set; }

    [StringLength(255)]
    public string? IletisimBilgileri { get; set; }

    [InverseProperty("Tedarikci")]
    public virtual ICollection<TedarikSiparisleri> TedarikSiparisleris { get; set; } = new List<TedarikSiparisleri>();
}
