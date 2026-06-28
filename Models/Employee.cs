using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models;

[Table("Employee")]
[Index("Username", Name = "UQ_Employee_Username", IsUnique = true)]
public partial class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    public int LibraryId { get; set; }

    [StringLength(255)]
    public string FullName { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    public byte Role { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<BorrowTicket> BorrowTickets { get; set; } = new List<BorrowTicket>();

    [ForeignKey("LibraryId")]
    [InverseProperty("Employees")]
    public virtual Library Library { get; set; } = null!;
}
