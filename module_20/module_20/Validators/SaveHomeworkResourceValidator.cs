using FluentValidation;
using module_20.Web.Resources;

namespace module_20.Web.Validators
{
    /// <summary>
    /// Validator for SaveHomeworkResource class
    /// </summary>
    public class SaveHomeworkResourceValidator : AbstractValidator<SaveHomeworkResource>
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public SaveHomeworkResourceValidator()
        {
            RuleFor(homework => homework.LectureId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("'Lecture Id' must be greater 0");

            RuleFor(homework => homework.StudentId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("'Student Id' must be greater 0");

            RuleFor(homework => homework.Mark)
                .GreaterThanOrEqualTo(0);

            RuleFor(homework => homework.Task)
                .NotEmpty();
        }
    }
}
