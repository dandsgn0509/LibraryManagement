using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("LibraryBook")]
[Index("LibraryId", "BookId", Name = "UQ_LibraryBook", IsUnique = true)]
public partial class LibraryBook
{
    [Key]
    public int LibraryBookId { get; set; }

    public int LibraryId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("LibraryBooks")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("LibraryId")]
    [InverseProperty("LibraryBooks")]
    public virtual Library Library { get; set; } = null!;
}
