using FluentValidation;
using module_20.Web.Resources;

namespace module_20.Web.Validators
{
    /// <summary>
    /// Validator for SaveCourseResource class
    /// </summary>
    public class SaveCourseResourceValidator : AbstractValidator<SaveCourseResource>
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public SaveCourseResourceValidator()
        {
            RuleFor(course => course.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(course => course.LecturerId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("'Lecturer Id' must be greater than 0");
                
        }
    }
}
