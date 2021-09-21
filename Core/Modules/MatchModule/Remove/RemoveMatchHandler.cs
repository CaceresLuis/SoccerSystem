using Infrastructure.Interfaces;
using MediatR;
using Shared.Enums;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Modules.MatchModule.Remove
{
    public class RemoveMatchHandler : IRequestHandler<RemoveMatchCommand, bool>
    {
        private readonly IMatchRepository _matchRepository;

        public RemoveMatchHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<bool> Handle(RemoveMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.FindMatchByIdAsync(request.Id);
            if (match.IsClosed)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "This match cannot be deleted",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            var delete = await _matchRepository.DeleteMatchAsync(match);
            if (!delete)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Something has was wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            return delete;
        }
    }
}
