using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("Newspaper")]
public partial class Newspaper
{
    [Key]
    public int NewspaperId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    public string Publisher { get; set; } = null!;

    public DateOnly PublishDate { get; set; }

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [InverseProperty("Newspaper")]
    public virtual ICollection<BorrowNewspaper> BorrowNewspapers { get; set; } = new List<BorrowNewspaper>();

    [InverseProperty("Newspaper")]
    public virtual ICollection<LibraryNewspaper> LibraryNewspapers { get; set; } = new List<LibraryNewspaper>();
}
