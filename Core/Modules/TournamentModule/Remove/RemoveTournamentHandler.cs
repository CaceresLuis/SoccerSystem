using MediatR;
using System.Net;
using System.Linq;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Shared.Helpers.Image;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Remove
{
    public class RemoveTournamentHandler : IRequestHandler<RemoveTournamentCommand, bool>
    {
        private readonly IIMageHelper _iMageHelper;
        private readonly IImageRepository _imageRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public RemoveTournamentHandler(ITournamentRepository tournamentRepository, IIMageHelper iMageHelper, IImageRepository imageRepository)
        {
            _iMageHelper = iMageHelper;
            _imageRepository = imageRepository;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<bool> Handle(RemoveTournamentCommand request, CancellationToken cancellationToken)
        {
            TournamentEntity tournamnet = await _tournamentRepository.GetTournamentWithGroupAsync(request.Id);
            if(tournamnet == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The tournament does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (tournamnet.Groups.Count() > 0)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = $"The tournament {tournamnet.Name} has registered groups",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (!await _tournamentRepository.DeleteTournamentAsync(tournamnet))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Something has gone wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            ImageEntity img = await _imageRepository.GetImage(request.Id);
            if(img != null)
            {
                await _imageRepository.DeleteImage(img);
                _iMageHelper.DeleteImage(img.Path);
            }

            return true;
        }
    }
}
