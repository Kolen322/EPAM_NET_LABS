using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M09
{
    /// <summary>
    /// Represent a information about the test
    /// </summary>
    public class TestOfStudent
    {
        private int _assessment;
        /// <summary>
        /// Name of student which pass the test
        /// </summary>
        public string NameOfStudent { get; private set; }
        /// <summary>
        /// Name of the test
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Date of the test
        /// </summary>
        public DateTime Date { get; private set; }
        /// <summary>
        /// The assessment of the test
        /// </summary>
        public int Assessment
        {
            get
            {
                return _assessment;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Assessment can't be lower than 1");
                }
                else
                {
                    _assessment = value;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the TestOfStudent class with the specifieds parameters
        /// </summary>
        /// <param name="nameOfStudent">Name of student</param>
        /// <param name="name">Name of subject</param>
        /// <param name="date">Date of the test</param>
        /// <param name="assessment">The assessment of the test</param>
        public TestOfStudent(string nameOfStudent, string name, DateTime date, int assessment)
        {
            NameOfStudent = nameOfStudent;
            Name = name;
            Date = date;
            Assessment = assessment;
        }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return NameOfStudent + " " + Name + " " + Date.ToShortDateString() + " " + Assessment;
        }
        /// <summary>
        /// Determines whether two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TestOfStudent test = (TestOfStudent)obj;
                return (Name == test.Name) && (Assessment == test.Assessment) && (Date == test.Date) && (NameOfStudent == test.NameOfStudent);
            }
        }
        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Assessment + Date.GetHashCode();
        }
    }
}
