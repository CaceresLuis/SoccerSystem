using MediatR;
using AutoMapper;
using Shared.Enums;
using Web.ViewModel;
using Shared.Exceptions;
using Core.ModelResponse;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupModule.Add;
using Core.Modules.GroupModule.Get;
using Core.Modules.GroupModule.Remove;
using Core.Modules.GroupModule.Update;
using Core.Modules.TournamentModule.Get;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(int id)
        {
            GroupViewModels groupView = new GroupViewModels { };
            try
            {
                TournamentResponse tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });
                TournamentViewModels tournament = _mapper.Map<TournamentViewModels>(tournamentResponse);
                groupView.Tournament = tournament;

                return View(groupView);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;
                return View(groupView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupViewModels groupView)
        {
            groupView.Id = 0;
            try
            {
                GroupResponse groupResponse = _mapper.Map<GroupResponse>(groupView);
                bool create = await _mediator.Send(new AddGroupCommand { Group = groupResponse });

                TempData["Title"] = "Created!";
                TempData["Message"] = $"The group {groupView.Name} was created";
                TempData["State"] = $"{State.success}";

                return RedirectToAction("Details", "Tournament", new { id = groupView.Tournament.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;
                return View(groupView);
            }
        }

        public async Task<ActionResult> Detail(int id)
        {
            try
            {
                GroupResponse groupResponse = await _mediator.Send(new GetFullGroupQuery { Id = id });
                GroupViewModels groupView = _mapper.Map<GroupViewModels>(groupResponse);
                return View(groupView);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;
                return RedirectToAction("Details", "Tournament", new { Id = id });
            }

        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            GroupResponse groupResponse = await _mediator.Send(new GetFullGroupQuery { Id = id });
            GroupViewModels groupView = _mapper.Map<GroupViewModels>(groupResponse);
            return View(groupView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GroupViewModels groupView)
        {
            try
            {
                GroupResponse groupResponse = _mapper.Map<GroupResponse>(groupView);
                bool update = await _mediator.Send(new UpdateGroupCommand { Group = groupResponse });
                TempData["Title"] = "Updated!";
                TempData["Message"] = $"The group {groupView.Name} was updated";
                TempData["State"] = State.success.ToString();

                return RedirectToAction("Detail", "Group", new { id = groupView.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error;
                return View(groupView);
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            GroupResponse group = await _mediator.Send(new GetGroupWithTournamentQuery { Id = id });
            try
            {
                await _mediator.Send(new RemoveGroupCommand { Id = id });
                TempData["Title"] = "Updated!";
                TempData["Message"] = $"Tournament {group.Name} has been deleted!";
                TempData["State"] = State.success.ToString();
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
            }

            return RedirectToAction("Details", "Tournament", new { id = group.Tournament.Id });
        }
    }
}
