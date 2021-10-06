

using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;

namespace SimpleApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){ }
        DbSet<Student> Students { get; set; }
        DbSet<Course> Courses { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student").HasKey(k => k.StudentId);
                entity.HasMany(c => c.Courses).WithMany(s => s.Students);
                entity.Property(p => p.Name).HasMaxLength(150).IsUnicode(false);

            });
             modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course").HasKey(k => k.CourseId);
                entity.HasMany(s => s.Students).WithMany(c => c.Courses);
                entity.Property(p => p.CourseName).HasMaxLength(150).IsUnicode(false);

            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer").HasKey(k => k.Id);
                entity.HasMany(o => o.Orders)
                .WithOne(c => c.Customer).OnDelete(DeleteBehavior.Cascade);
                entity.Property(p => p.Name).HasMaxLength(100).IsUnicode(false);

            });
             
             modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order").HasKey(k => k.Id);
                entity.HasOne(c => c.Customer)
                .WithMany(o => o.Orders).HasForeignKey(o => o.CustomerId).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(250).IsUnicode(false);
                
            });



        }

    }
   
}