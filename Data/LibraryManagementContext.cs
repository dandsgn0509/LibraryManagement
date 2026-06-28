using System;
using System.Collections.Generic;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data;

public partial class LibraryManagementContext : DbContext
{
    public LibraryManagementContext()
    {
    }

    public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowBook> BorrowBooks { get; set; }

    public virtual DbSet<BorrowMagazine> BorrowMagazines { get; set; }

    public virtual DbSet<BorrowNewspaper> BorrowNewspapers { get; set; }

    public virtual DbSet<BorrowTicket> BorrowTickets { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<LibraryBook> LibraryBooks { get; set; }

    public virtual DbSet<LibraryMagazine> LibraryMagazines { get; set; }

    public virtual DbSet<LibraryNewspaper> LibraryNewspapers { get; set; }

    public virtual DbSet<Magazine> Magazines { get; set; }

    public virtual DbSet<Newspaper> Newspapers { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6NIC2J1\\MYLAPTOP;Database=LibraryManagement;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<BorrowBook>(entity =>
        {
            entity.HasOne(d => d.Book).WithMany(p => p.BorrowBooks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowBook_Book");

            entity.HasOne(d => d.BorrowTicket).WithMany(p => p.BorrowBooks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowBook_BorrowTicket");
        });

        modelBuilder.Entity<BorrowMagazine>(entity =>
        {
            entity.HasOne(d => d.BorrowTicket).WithMany(p => p.BorrowMagazines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowMagazine_BorrowTicket");

            entity.HasOne(d => d.Magazine).WithMany(p => p.BorrowMagazines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowMagazine_Magazine");
        });

        modelBuilder.Entity<BorrowNewspaper>(entity =>
        {
            entity.HasOne(d => d.BorrowTicket).WithMany(p => p.BorrowNewspapers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowNewspaper_BorrowTicket");

            entity.HasOne(d => d.Newspaper).WithMany(p => p.BorrowNewspapers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowNewspaper_Newspaper");
        });

        modelBuilder.Entity<BorrowTicket>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Employee).WithMany(p => p.BorrowTickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowTicket_Employee");

            entity.HasOne(d => d.Library).WithMany(p => p.BorrowTickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowTicket_Library");

            entity.HasOne(d => d.Reader).WithMany(p => p.BorrowTickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowTicket_Reader");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Library).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Library");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<LibraryBook>(entity =>
        {
            entity.HasOne(d => d.Book).WithMany(p => p.LibraryBooks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryBook_Book");

            entity.HasOne(d => d.Library).WithMany(p => p.LibraryBooks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryBook_Library");
        });

        modelBuilder.Entity<LibraryMagazine>(entity =>
        {
            entity.HasOne(d => d.Library).WithMany(p => p.LibraryMagazines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryMagazine_Library");

            entity.HasOne(d => d.Magazine).WithMany(p => p.LibraryMagazines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryMagazine_Magazine");
        });

        modelBuilder.Entity<LibraryNewspaper>(entity =>
        {
            entity.HasOne(d => d.Library).WithMany(p => p.LibraryNewspapers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryNewspaper_Library");

            entity.HasOne(d => d.Newspaper).WithMany(p => p.LibraryNewspapers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryNewspaper_Newspaper");
        });

        modelBuilder.Entity<Magazine>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Newspaper>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
