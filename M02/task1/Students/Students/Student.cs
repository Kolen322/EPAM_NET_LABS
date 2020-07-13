using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Students
{
    /// <summary>
    /// The Student class.
    /// Contains all methods that are required in Task 1.
    /// </summary>
    class Student
    {
        /// <summary>
        /// Student name.
        /// </summary>
        public string Name;
        /// <summary>
        /// Student email.
        /// </summary>
        public string Email;

        /// <summary>
        /// Email should matches Name.
        /// </summary>
        /// <param name="name">Student name</param>
        /// <param name="email">Student email</param>
        public Student(string name, string email)
        {
            if (!email.Contains(name.ToLower().Replace(" ", ".")))
            {
                throw new ArgumentException("Email doesn't match name");
            }
            else
            {
                Name = name;
                Email = email;
            }
        }
        /// <summary>
        /// Name will take from email
        /// </summary>
        /// <param name="email">Student email</param>
        public Student(string email) : this(email, email)
        {
            char[] nameFromEmail = email.Substring(0, email.IndexOf("@")).Replace(".", " ").ToCharArray();
            nameFromEmail[0] = Char.ToUpper(nameFromEmail[0]);
            int indexOfSurnameFirstLetter = email.IndexOf(".") + 1;
            nameFromEmail[indexOfSurnameFirstLetter] = Char.ToUpper(nameFromEmail[indexOfSurnameFirstLetter]);
            Name = new string(nameFromEmail);
        }
        /// <summary>
        /// Override object method Equals.
        /// Determines whether two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>Boolean type</returns>
        public override bool Equals(object obj)
        {
            // check for null and compare run-time types
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Student student = (Student)obj;
                return (Name == student.Name) && (Email == student.Email);
            }
        }
        /// <summary>
        /// Override object method GetHashCode.
        /// </summary>
        /// <returns>(Int32) A hast code for the current object.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}