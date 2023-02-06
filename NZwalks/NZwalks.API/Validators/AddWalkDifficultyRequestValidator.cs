using FluentValidation;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Validators
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
