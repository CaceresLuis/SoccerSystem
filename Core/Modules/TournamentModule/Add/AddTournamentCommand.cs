using MediatR;
using FluentValidation;
using Core.Dtos.AddDtos;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentCommand : IRequest<bool>
    {
        public AddTournamentDto Tournament { get; set; }
    }
    public class TournamentValidator : AbstractValidator<AddTournamentDto>
    {
        public TournamentValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("The name is requered");
            RuleFor(t => t.StartDate).NotEmpty();
            RuleFor(t => t.EndDate).NotEmpty();

            RuleFor(t => t.IsActive)
                .NotNull();
        }
    }
}
