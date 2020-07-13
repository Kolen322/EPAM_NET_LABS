using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Net.Http.Headers;
using module_20.Web.Resources;

namespace module_20.Web.Formatters
{
    /// <summary>
    /// Class thath realize txt output format
    /// </summary>
    public class TxtOutputAttendanceFormatter : TextOutputFormatter
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public TxtOutputAttendanceFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/txt"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        /// <summary>
        /// Checks whether type's output to txt
        /// </summary>
        /// <param name="type">Type of object</param>
        protected override bool CanWriteType(Type type)
        {
            if (typeof(AttendanceOfLectureResource).IsAssignableFrom(type)
                || typeof(IEnumerable<AttendanceOfLectureResource>).IsAssignableFrom(type)
                || typeof(AttendanceOfStudentResource).IsAssignableFrom(type)
                || typeof(IEnumerable<AttendanceOfStudentResource>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        /// <summary>
        /// Output context to txt format
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="selectedEncoding">Encoding</param>
        /// <returns>Http response with result</returns>
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<AttendanceOfStudentResource>)
            {
                foreach (var lecture in context.Object as IEnumerable<AttendanceOfStudentResource>)
                {
                    FormatAttendanceStudent(buffer, lecture);
                }
            }
            else if(context.Object is AttendanceOfStudentResource)
            {
                var lecture = context.Object as AttendanceOfStudentResource;
                FormatAttendanceStudent(buffer, lecture);
            }
            else if(context.Object is IEnumerable<AttendanceOfLectureResource>)
            {
                foreach (var student in context.Object as IEnumerable<AttendanceOfLectureResource>)
                {
                    FormatAttendanceLecture(buffer, student);
                }
            }
            else
            {
                var student = context.Object as AttendanceOfLectureResource;
                FormatAttendanceLecture(buffer, student);
            }
            return response.WriteAsync(buffer.ToString());
        }
        private static void FormatAttendanceStudent(StringBuilder buffer, AttendanceOfStudentResource attendanceOfStudentResource)
        {
            buffer.AppendLine($"Lecture - {attendanceOfStudentResource.Lecture.Name}");
            buffer.AppendLine($"Attendance - {attendanceOfStudentResource.Attendance}");
        }

        private static void FormatAttendanceLecture(StringBuilder buffer, AttendanceOfLectureResource attendanceOfLectureResource)
        {
            buffer.AppendLine($"Student - {attendanceOfLectureResource.Student.Name}");
            buffer.AppendLine($"Attendance - {attendanceOfLectureResource.Attendance}");
            buffer.AppendLine("-------");
        }
    }
}
