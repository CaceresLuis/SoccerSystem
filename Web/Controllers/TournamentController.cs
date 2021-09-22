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
using Microsoft.AspNetCore.Authorization;
using Core.Modules.TournamentModule.Remove;
using Core.Modules.TournamentModule.Update;
using Shared.Exceptions;
using Shared.Enums;

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

            TournamentViewModels[] tournamentViews = _mapper.Map<TournamentViewModels[]>(tournaments);

            return View(tournamentViews);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TournamentViewModels tournamentView)
        {
            try
            {
                TournamentResponse tournamentResponse = _mapper.Map<TournamentResponse>(tournamentView);
                await _mediator.Send(new AddTournamentCommand { Tournament = tournamentResponse });

                TempData["Title"] = "Created!";
                TempData["Message"] = $"The tournament {tournamentResponse.Name} was created";
                TempData["State"] = State.success.ToString();

                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View();
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });
                TournamentViewModels tournamentView = _mapper.Map<TournamentViewModels>(tournamentResponse);
                return View(tournamentView);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return RedirectToAction("Index", "Tournament");
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });
                TournamentViewModels tournamentView = _mapper.Map<TournamentViewModels>(tournamentResponse);
                return View(tournamentView);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return RedirectToAction("Index", "Tournament");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TournamentViewModels tournamentView)
        {
            try
            {
                TournamentResponse tournamentResponse = _mapper.Map<TournamentResponse>(tournamentView);
                await _mediator.Send(new UpdateTournamentCommnad { TournamentResponse = tournamentResponse });
                TempData["Title"] = "Updated!";
                TempData["Message"] = $"The tournament {tournamentResponse.Name} was Updated";
                TempData["State"] = State.success.ToString();
                return RedirectToAction(nameof(Details), new { id = tournamentResponse.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View(tournamentView);
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new RemoveTournamentCommand { Id = id });
                TempData["Title"] = "Deleted!";
                TempData["Message"] = "Tournament has been deleted!";
                TempData["State"] = State.success.ToString();

            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
