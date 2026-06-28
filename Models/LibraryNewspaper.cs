using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("LibraryNewspaper")]
[Index("LibraryId", "NewspaperId", Name = "UQ_LibraryNewspaper", IsUnique = true)]
public partial class LibraryNewspaper
{
    [Key]
    public int LibraryNewspaperId { get; set; }

    public int LibraryId { get; set; }

    public int NewspaperId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("LibraryId")]
    [InverseProperty("LibraryNewspapers")]
    public virtual Library Library { get; set; } = null!;

    [ForeignKey("NewspaperId")]
    [InverseProperty("LibraryNewspapers")]
    public virtual Newspaper Newspaper { get; set; } = null!;
}
