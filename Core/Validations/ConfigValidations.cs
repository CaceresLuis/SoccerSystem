using Core.Dtos;
using FluentValidation;
using Core.Dtos.AddDtos;
using Infrastructure.Models;
using Core.Modules.UserModule.LoginWeb;

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

    public class LoginWebValidator : AbstractValidator<LoginWebQuery>
    {
        public LoginWebValidator()
        {
            RuleFor(l => l.UserName)
                .NotEmpty()
                .WithMessage("Email is requered");
            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("Password is requered");
        }
    }

    public class TeamValidator : AbstractValidator<TeamDto>
    {
        public TeamValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("The name is requered");
        }
    }

    public class GroupValidator : AbstractValidator<GroupDto>
    {
        public GroupValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty()
                .WithMessage("The name is requered");
        }
    }
   
    public class RoleValidator : AbstractValidator<RoleDto>
    {
        public RoleValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("The name is requered");
        }
    }
    
    public class AddMatchValidator : AbstractValidator<AddMatchDto>
    {
        public AddMatchValidator()
        {
            RuleFor(am => am.Date)
                .NotEmpty()
                .WithMessage("The Date is requered");
            RuleFor(am => am.Hour)
                .NotEmpty()
                .WithMessage("The Hour is requered");
            RuleFor(am => am.VisitorId)
                .NotNull()
                .WithMessage("Select the Visitor team");
            RuleFor(am => am.LocalId)
                .NotNull()
                .WithMessage("Select the Local team");
        }
    }
}
