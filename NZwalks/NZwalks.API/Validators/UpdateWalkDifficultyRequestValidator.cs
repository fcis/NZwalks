using FluentValidation;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Validators
{
    public class UpdateWalkDifficultyRequestValidator :AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
