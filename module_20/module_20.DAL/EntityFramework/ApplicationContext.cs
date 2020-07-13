using Microsoft.EntityFrameworkCore;
using module_20.DAL.Entities;

namespace module_20.DAL.EntityFramework
{
    /// <summary>
    /// Context of application
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Course table
        /// </summary>
        public DbSet<Course> Courses { get; set; }
        /// <summary>
        /// Lecturers table
        /// </summary>
        public DbSet<Lecturer> Lecturers { get; set; }
        /// <summary>
        /// Students table
        /// </summary>
        public DbSet<Student> Students { get; set; }
        /// <summary>
        /// Lectures table
        /// </summary>
        public DbSet<Lecture> Lectures { get; set; }
        /// <summary>
        /// Homeworks table
        /// </summary>
        public DbSet<Homework> Homeworks { get; set; }
        /// <summary>
        /// Constructs a new application context
        /// </summary>
        /// <param name="options">Options of context</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new LecturerConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new LectureConfiguration());
            builder.ApplyConfiguration(new HomeworkConfiguration());
            builder.ApplyConfiguration(new StudentCourseConfiguration());
            builder.ApplyConfiguration(new StudentLectureConfiguration());
            
        }
    }
}
