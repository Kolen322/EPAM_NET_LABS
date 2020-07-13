using System;
using System.Collections.Generic;

namespace module_20.DAL.Entities
{
    /// <summary>
    /// Model that represent the lecture
    /// </summary>
    public class Lecture
    {
        /// <summary>
        /// Lecture Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date of lecture
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Name of lecture
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Homeworks of lecture
        /// </summary>
        public List<Homework> Homeworks { get; set; }
        /// <summary>
        /// Course Id
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// The course of lecture
        /// </summary>
        public Course Course { get; set; }
        /// <summary>
        /// Attendance at lecture
        /// </summary>
        public List<StudentLecture> StudentLectures { get; set; }
        public Lecture()
        {
            StudentLectures = new List<StudentLecture>();
        }
    }
}
