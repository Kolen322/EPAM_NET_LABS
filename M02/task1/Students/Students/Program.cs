using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            // Create a string array "subjects" which contains 4 different shcool subjects ("Maths, "PE", etc..).
            string[] subjects = new string[] { "Maths", "Russian Language", "Biology", "English Language" };
            // Сreate 3 students with different names using first constructor.
            Student student1c1 = new Student("Vasya Pupkin", "vasya.pupkin@epam.com");
            Student student2c1 = new Student("Oleg Bur", "oleg.bur@epam.com");
            Student student3c1 = new Student("Nikita Slavnov", "nikita.slavnov@epam.com");
            // Create 3 students with the same names names using second constructor.
            Student student1c2 = new Student("vasya.pupkin@epam.com");
            Student student2c2 = new Student("oleg.bur@epam.com");
            Student student3c2 = new Student("nikita.slavnov@epam.com");
            Dictionary<Student, HashSet<string>> studentSubjectDict = new Dictionary<Student, HashSet<string>>();
            studentSubjectDict[student1c1] = new HashSet<string>(new string[] { subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)] });
            studentSubjectDict[student2c1] = new HashSet<string>(new string[] { subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)] });
            studentSubjectDict[student3c1] = new HashSet<string>(new string[] { subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)] });
            studentSubjectDict[student1c2] = new HashSet<string>(new string[] { subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)] });
            studentSubjectDict[student2c2] = new HashSet<string>(new string[] { subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)] });
            studentSubjectDict[student3c2] = new HashSet<string>(new string[] { subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)], subjects[rnd.Next(0, 4)] });
        }
    }
}
