using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("BorrowTicket")]
public partial class BorrowTicket
{
    [Key]
    public int BorrowTicketId { get; set; }

    public int LibraryId { get; set; }

    public int ReaderId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime DueDate { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [InverseProperty("BorrowTicket")]
    public virtual ICollection<BorrowBook> BorrowBooks { get; set; } = new List<BorrowBook>();

    [InverseProperty("BorrowTicket")]
    public virtual ICollection<BorrowMagazine> BorrowMagazines { get; set; } = new List<BorrowMagazine>();

    [InverseProperty("BorrowTicket")]
    public virtual ICollection<BorrowNewspaper> BorrowNewspapers { get; set; } = new List<BorrowNewspaper>();

    [ForeignKey("EmployeeId")]
    [InverseProperty("BorrowTickets")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("LibraryId")]
    [InverseProperty("BorrowTickets")]
    public virtual Library Library { get; set; } = null!;

    [ForeignKey("ReaderId")]
    [InverseProperty("BorrowTickets")]
    public virtual Reader Reader { get; set; } = null!;
}
