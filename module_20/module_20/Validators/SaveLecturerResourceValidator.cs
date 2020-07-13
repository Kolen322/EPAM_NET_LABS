using FluentValidation;
using module_20.Web.Resources;

namespace module_20.Web.Validators
{
    /// <summary>
    /// Validator for SaveLecturerResource class
    /// </summary>
    public class SaveLecturerResourceValidator : AbstractValidator<SaveLecturerResource>
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        public SaveLecturerResourceValidator()
        {
            RuleFor(lecturer => lecturer.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(lecturer => lecturer.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(lecturer => lecturer.Mobile)
                .Matches(@"([+]*\d\s+[(]*\d{3}[)]*\s+\d{3}[-]+\d{2}[-]+\d{2})")
                .WithMessage("Input mobile number with that format - +X (XXX) XXX-XX-XX");
        }
    }
}
