using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("Library")]
public partial class Library
{
    [Key]
    public int LibraryId { get; set; }

    [StringLength(255)]
    public string LibraryName { get; set; } = null!;

    [StringLength(500)]
    public string Address { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [InverseProperty("Library")]
    public virtual ICollection<BorrowTicket> BorrowTickets { get; set; } = new List<BorrowTicket>();

    [InverseProperty("Library")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("Library")]
    public virtual ICollection<LibraryBook> LibraryBooks { get; set; } = new List<LibraryBook>();

    [InverseProperty("Library")]
    public virtual ICollection<LibraryMagazine> LibraryMagazines { get; set; } = new List<LibraryMagazine>();

    [InverseProperty("Library")]
    public virtual ICollection<LibraryNewspaper> LibraryNewspapers { get; set; } = new List<LibraryNewspaper>();
}
