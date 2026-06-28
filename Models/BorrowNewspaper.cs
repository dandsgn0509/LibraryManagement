using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("BorrowNewspaper")]
[Index("BorrowTicketId", "NewspaperId", Name = "UQ_BorrowNewspaper", IsUnique = true)]
public partial class BorrowNewspaper
{
    [Key]
    public int BorrowNewspaperId { get; set; }

    public int BorrowTicketId { get; set; }

    public int NewspaperId { get; set; }

    public int Quantity { get; set; }

    public DateTime? ReturnDate { get; set; }

    public byte Status { get; set; }

    [ForeignKey("BorrowTicketId")]
    [InverseProperty("BorrowNewspapers")]
    public virtual BorrowTicket BorrowTicket { get; set; } = null!;

    [ForeignKey("NewspaperId")]
    [InverseProperty("BorrowNewspapers")]
    public virtual Newspaper Newspaper { get; set; } = null!;
}
