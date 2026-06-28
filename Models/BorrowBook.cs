using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("BorrowBook")]
[Index("BorrowTicketId", "BookId", Name = "UQ_BorrowBook", IsUnique = true)]
public partial class BorrowBook
{
    [Key]
    public int BorrowBookId { get; set; }

    public int BorrowTicketId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public DateTime? ReturnDate { get; set; }

    public byte Status { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BorrowBooks")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("BorrowTicketId")]
    [InverseProperty("BorrowBooks")]
    public virtual BorrowTicket BorrowTicket { get; set; } = null!;
}
