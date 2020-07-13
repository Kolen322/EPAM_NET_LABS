using FluentValidation;
using module_20.Web.Resources;

namespace module_20.Web.Validators
{
    /// <summary>
    /// Validator for SaveStudentResource class
    /// </summary>
    public class SaveStudentResourceValidator : AbstractValidator<SaveStudentResource>
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public SaveStudentResourceValidator()
        {
            RuleFor(student => student.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(student => student.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(student => student.Mobile)
                .Matches(@"([+]*\d\s+[(]*\d{3}[)]*\s+\d{3}[-]+\d{2}[-]+\d{2})")
                .WithMessage("Input mobile number with that format - +X (XXX) XXX-XX-XX");
        }
    }
}
