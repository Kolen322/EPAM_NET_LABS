using FluentValidation;
using module_20.Web.Resources;

namespace module_20.Web.Validators
{
    /// <summary>
    /// Validator for SaveLectureResource class
    /// </summary>
    public class SaveLectureResourceValidator : AbstractValidator<SaveLectureResource>
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public SaveLectureResourceValidator()
        {
            RuleFor(lecture => lecture.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(lecture => lecture.Date)
                .NotEmpty();

            RuleFor(lecture => lecture.CourseId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("'Course Id' must be greater 0");
        }
    }
}
