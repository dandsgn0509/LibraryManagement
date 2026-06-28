using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("BorrowMagazine")]
[Index("BorrowTicketId", "MagazineId", Name = "UQ_BorrowMagazine", IsUnique = true)]
public partial class BorrowMagazine
{
    [Key]
    public int BorrowMagazineId { get; set; }

    public int BorrowTicketId { get; set; }

    public int MagazineId { get; set; }

    public int Quantity { get; set; }

    public DateTime? ReturnDate { get; set; }

    public byte Status { get; set; }

    [ForeignKey("BorrowTicketId")]
    [InverseProperty("BorrowMagazines")]
    public virtual BorrowTicket BorrowTicket { get; set; } = null!;

    [ForeignKey("MagazineId")]
    [InverseProperty("BorrowMagazines")]
    public virtual Magazine Magazine { get; set; } = null!;
}
