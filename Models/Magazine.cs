using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("Magazine")]
public partial class Magazine
{
    [Key]
    public int MagazineId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public int IssueNumber { get; set; }

    [StringLength(255)]
    public string Publisher { get; set; } = null!;

    public DateOnly PublishDate { get; set; }

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [InverseProperty("Magazine")]
    public virtual ICollection<BorrowMagazine> BorrowMagazines { get; set; } = new List<BorrowMagazine>();

    [InverseProperty("Magazine")]
    public virtual ICollection<LibraryMagazine> LibraryMagazines { get; set; } = new List<LibraryMagazine>();
}
