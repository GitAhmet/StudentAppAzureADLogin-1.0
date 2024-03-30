
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

namespace StudentApp.Data;
public class StudentAppContext : DbContext
{
    public StudentAppContext()
    {
    }
    
    public StudentAppContext(DbContextOptions<StudentAppContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StudentApp.Data;Trusted_Connection=True");
    }
    
    public DbSet<Students> Student { get; set; }
    public DbSet<StudentPhoneNo> StudentPhoneNo { get; set; }
    public DbSet<StudentEmailAddress> StudentEmailAddress { get; set; }
    public DbSet<StudentAddress> StudentAddress { get; set; }
    public DbSet<StudentImage> StudentImage { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        {
            modelBuilder.Entity<StudentPhoneNo>()
                        .HasOne(e => e.Students)
                        .WithMany(d => d.PhoneStudent)
                        .HasForeignKey(e => e.StudentsId)
                        .IsRequired(false);

            modelBuilder.Entity<StudentEmailAddress>()
                        .HasOne(e => e.Students)
                        .WithMany(d => d.EmailAddressStudent)
                        .HasForeignKey(e => e.StudentsId)
                        .IsRequired(false);

            modelBuilder.Entity<StudentAddress>()
                        .HasOne(e => e.Students)
                        .WithMany(d => d.AddressStudent)
                        .HasForeignKey(e => e.StudentsId)
                        .IsRequired(false);

            modelBuilder.Entity<StudentImage>()
                        .HasOne(e => e.Students)
                        .WithMany(d => d.ImageStudent)
                        .HasForeignKey(e => e.StudentsId)
                        .IsRequired(false);
        }
    }
}
