using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("Reader")]
[Index("Username", Name = "UQ_Reader_Username", IsUnique = true)]
public partial class Reader
{
    [Key]
    public int ReaderId { get; set; }

    [StringLength(255)]
    public string FullName { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public bool Gender { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [InverseProperty("Reader")]
    public virtual ICollection<BorrowTicket> BorrowTickets { get; set; } = new List<BorrowTicket>();
}
