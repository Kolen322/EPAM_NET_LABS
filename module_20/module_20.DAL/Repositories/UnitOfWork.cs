using module_20.DAL.EntityFramework;
using module_20.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace module_20.DAL.Repositories
{
    /// <summary>
    /// Class that executes the contract of a unit of work pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _db;
        private CourseRepository _courseRepository;
        private HomeworkRepository _homeworkRepository;
        private LectureRepository _lectureRepository;
        private LecturerRepository _lecturerRepository;
        private StudentRepository _studentRepository;

        /// <summary>
        /// Constuctor with specified context
        /// </summary>
        /// <param name="context">Application context</param>
        public UnitOfWork(ApplicationContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Course repository
        /// </summary>
        public ICourseRepository Courses => _courseRepository = _courseRepository ?? new CourseRepository(_db);
        /// <summary>
        /// Homework repository
        /// </summary>
        public IHomeworkRepository Homeworks => _homeworkRepository = _homeworkRepository ?? new HomeworkRepository(_db);
        /// <summary>
        /// Lecture repository
        /// </summary>
        public ILectureRepository Lectures => _lectureRepository = _lectureRepository ?? new LectureRepository(_db);
        /// <summary>
        /// Lecturer repository
        /// </summary>
        public ILecturerRepository Lecturers => _lecturerRepository = _lecturerRepository ?? new LecturerRepository(_db);
        /// <summary>
        /// Student repository
        /// </summary>
        public IStudentRepository Students => _studentRepository = _studentRepository ?? new StudentRepository(_db);

        /// <summary>
        /// Save changes in repositories
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }

        /// <summary>
        /// Save changes in repositories
        /// </summary>
        /// <returns>Task</returns>
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
