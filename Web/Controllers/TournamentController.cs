using MediatR;
using AutoMapper;
using Web.ViewModel;
using Core.ModelResponse;
using System.Threading.Tasks;
using Core.ModelResponse.One;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TournamentModule.Add;
using Core.Modules.TournamentModule.Get;
using Core.Modules.TournamentModule.List;
using Core.Modules.TournamentModule.Remove;
using Core.Modules.TournamentModule.Update;

namespace Web.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TournamentController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            TournamentResponse[] tournaments = await _mediator.Send(new ListTournamentsQuery());

            TournamentViewModel[] tournamentViews = _mapper.Map<TournamentViewModel[]>(tournaments);

            return View(tournamentViews);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TournamentViewModel tournamentView)
        {
            TournamentResponse tournamentResponse = _mapper.Map<TournamentResponse>(tournamentView);
            ActionResponse create = await _mediator.Send(new AddTournamentCommand { Tournament = tournamentResponse });

            TempData["Title"] = create.Title;
            TempData["Message"] = create.Message;
            TempData["State"] = create.State.ToString();

            if (!create.IsSuccess)
                return View();
                
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int id)
        {
            ATournamentResponse tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });

            if (!tournamentResponse.Data.IsSuccess)
                return RedirectToAction(nameof(Index));

            TournamentViewModel tournamentView = _mapper.Map<TournamentViewModel>(tournamentResponse.Tournament);
            return View(tournamentView);
        }

        public async Task<ActionResult> Edit(int id)
        {
            ATournamentResponse tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });
            TempData["Title"] = tournamentResponse.Data.Title;
            TempData["Message"] = tournamentResponse.Data.Message;
            TempData["State"] = tournamentResponse.Data.State.ToString();

            if (!tournamentResponse.Data.IsSuccess)
                return RedirectToAction(nameof(Index));

            TournamentViewModel tournamentView = _mapper.Map<TournamentViewModel>(tournamentResponse.Tournament);
            return View(tournamentView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TournamentViewModel tournamentView)
        {
            TournamentResponse tournamentResponse = _mapper.Map<TournamentResponse>(tournamentView);
            ActionResponse update = await _mediator.Send(new UpdateTournamentCommnad { TournamentResponse = tournamentResponse });
            TempData["Title"] = update.Title;
            TempData["Message"] = update.Message;
            TempData["State"] = update.State.ToString();

            if (!update.IsSuccess)
                return View(tournamentView);

            return  RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            ActionResponse delete = await _mediator.Send(new RemoveTournamentCommand { Id = id });
            TempData["Title"] = delete.Title;
            TempData["Message"] = delete.Message;
            TempData["State"] = delete.State.ToString();

            return RedirectToAction(nameof(Index));
        }
    }
}
