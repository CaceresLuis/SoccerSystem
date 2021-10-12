using System;
using MediatR;
using Core.Dtos;
using AutoMapper;
using Shared.Enums;
using Core.Dtos.AddDtos;
using Shared.Exceptions;
using Shared.Helpers.Image;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.ImageModule.Add;
using Microsoft.AspNetCore.Hosting;
using Core.Modules.TournamentModule.Add;
using Core.Modules.TournamentModule.Get;
using Core.Modules.TournamentModule.List;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.TournamentModule.Remove;
using Core.Modules.TournamentModule.Update;

namespace Web.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TournamentController(IMediator mediator, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            TournamentFullData[] tournaments = await _mediator.Send(new ListTournamentsQuery());

            return View(tournaments);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddTournamentDto addTournamentDto)
        {
            try
            {
                await _mediator.Send(new AddTournamentCommand { Tournament = addTournamentDto });
                var tournament = await _mediator.Send(new GetTournamentByNameQuery { Name = addTournamentDto.Name });
                ImageData img = new ImageData { File = addTournamentDto.LogoFile, Reference = tournament.Id, Folder = "Tournaments" };
                await _mediator.Send(new AddImageCommad { ImageData = img });

                TempData["Title"] = "Created!";
                TempData["Message"] = $"The tournament {addTournamentDto.Name} was created";
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

        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                TournamentFullData tournamentFullData = await _mediator.Send(new GetTournamentQuery { Id = id });
                return View(tournamentFullData);
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
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                Core.Dtos.TournamentDto tournamentFullData = await _mediator.Send(new FindTournamentQuery { Id = id });
                return View(tournamentFullData);
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
        public async Task<ActionResult> Edit(TournamentDto tournamentDto)
        {
            try
            {
                await _mediator.Send(new UpdateTournamentCommnad { TournamentDto = tournamentDto });
                TempData["Title"] = "Updated!";
                TempData["Message"] = $"The tournament {tournamentDto.Name} was Updated";
                TempData["State"] = State.success.ToString();
                return RedirectToAction(nameof(Details), new { id = tournamentDto.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View(tournamentDto);
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid id)
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
