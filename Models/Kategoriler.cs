using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace muzikaletleristok.Models;

[Table("Kategoriler")]
public partial class Kategoriler
{
    [Key]
    [Column("KategoriID")]
    public int KategoriId { get; set; }

    [StringLength(100)]
    public string KategoriAdi { get; set; } = null!;

    [InverseProperty("Kategori")]
    public virtual ICollection<MuzikAletleri> MuzikAletleris { get; set; } = new List<MuzikAletleri>();
}
