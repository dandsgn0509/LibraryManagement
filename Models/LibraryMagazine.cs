using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("LibraryMagazine")]
[Index("LibraryId", "MagazineId", Name = "UQ_LibraryMagazine", IsUnique = true)]
public partial class LibraryMagazine
{
    [Key]
    public int LibraryMagazineId { get; set; }

    public int LibraryId { get; set; }

    public int MagazineId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("LibraryId")]
    [InverseProperty("LibraryMagazines")]
    public virtual Library Library { get; set; } = null!;

    [ForeignKey("MagazineId")]
    [InverseProperty("LibraryMagazines")]
    public virtual Magazine Magazine { get; set; } = null!;
}
