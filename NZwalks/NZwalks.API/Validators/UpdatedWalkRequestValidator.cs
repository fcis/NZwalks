using FluentValidation;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Validators
{
    public class UpdatedWalkRequestValidator :AbstractValidator<UpdatedWalkRequest>
    {
        public UpdatedWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
