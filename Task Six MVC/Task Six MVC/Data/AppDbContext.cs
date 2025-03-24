using Microsoft.EntityFrameworkCore;
using Task_Two_MVC.Entities;

namespace Task_Two_MVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MVCTaskTwo;Integrated Security=True;Trust Server Certificate=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StuCrsRes>()
                .HasKey(k => new { k.StudentId, k.CourseId });

          
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Students)
                .WithOne(s => s.Department)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);   

            
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Teachers)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);  
            

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Courses)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);  

          
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Teachers)
                .WithOne(t => t.Course)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.NoAction);  

      
            modelBuilder.Entity<StuCrsRes>()
                .HasOne(scr => scr.Student)
                .WithMany(s => s.StuCrsRes)
                .HasForeignKey(scr => scr.StudentId)
                .OnDelete(DeleteBehavior.Cascade); 

   
            modelBuilder.Entity<StuCrsRes>()
                .HasOne(scr => scr.Course)
                .WithMany(c => c.StuCrsRes)
                .HasForeignKey(scr => scr.CourseId)
                .OnDelete(DeleteBehavior.Cascade);  
        }


        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses  { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers  { get; set; }
        public DbSet<StuCrsRes> StuCrsRes  { get; set; }
    }
}
