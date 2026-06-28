using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("Book")]
public partial class Book
{
    [Key]
    public int BookId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    public string Author { get; set; } = null!;

    [StringLength(255)]
    public string Publisher { get; set; } = null!;

    public int PublishYear { get; set; }

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<BorrowBook> BorrowBooks { get; set; } = new List<BorrowBook>();

    [InverseProperty("Book")]
    public virtual ICollection<LibraryBook> LibraryBooks { get; set; } = new List<LibraryBook>();
}
