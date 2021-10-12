using System;
using MediatR;
using Core.Dtos;

namespace Core.Modules.TournamentModule.Get
{
    public class FindTournamentQuery : IRequest<TournamentDto>
    {
        public Guid Id { get; set; }
    }
}
