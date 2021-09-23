using Core.Dtos.DtosApi;
using FluentValidation;
using Infrastructure.Models;

namespace Core.Validations
{
    public class ConfigValidations
    {}

    public class GroupEntityValidation : AbstractValidator<GroupEntity>
    {
    }

    public class AddTournamnetValidation : AbstractValidator<AddTournamentDto>
    {
        public AddTournamnetValidation()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("The name is requiered");
        }
    }

}
