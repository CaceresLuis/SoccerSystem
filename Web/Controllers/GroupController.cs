using System;
using MediatR;
using Core.Dtos;
using Shared.Enums;
using Shared.Exceptions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupModule.Add;
using Core.Modules.GroupModule.Get;
using Core.Modules.GroupModule.Remove;
using Core.Modules.GroupModule.Update;
using Core.Modules.TournamentModule.Get;
using Microsoft.AspNetCore.Authorization;
using Core.Dtos.DtosApi;

namespace Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(Guid id)
        {
            try
            {
                TournamentDto tournamentDto = await _mediator.Send(new FindTournamentQuery { Id = id });
                GroupDto groupDto = new GroupDto { Tournament = tournamentDto };

                return View(groupDto);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LiteGroupDto groupDto)
        {
            try
            {
                bool create = await _mediator.Send(new AddGroupCommand { GroupDto = groupDto });

                TempData["Title"] = "Created!";
                TempData["Message"] = $"The group {groupDto.Name} was created";
                TempData["State"] = $"{State.success}";

                return RedirectToAction("Details", "Tournament", new { id = groupDto.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;
                return View(groupDto);
            }
        }

        public async Task<ActionResult> Detail(Guid id)
        {
            try
            {
                GroupFullData groupFullData = await _mediator.Send(new GetFullGroupQuery { Id = id });
                return View(groupFullData);
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
        public async Task<ActionResult> Edit(Guid id)
        {
            GroupFullDataApi groupDto = await _mediator.Send(new GetGroupQuery { Id = id });
            return View(groupDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GroupDto groupDto)
        {
            try
            {
                bool update = await _mediator.Send(new UpdateGroupCommand { Group = groupDto });
                TempData["Title"] = "Updated!";
                TempData["Message"] = $"The group {groupDto.Name} was updated";
                TempData["State"] = State.success.ToString();

                return RedirectToAction("Detail", "Group", new { id = groupDto.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error;
                return View(groupDto);
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            GroupFullDataApi group = await _mediator.Send(new GetGroupQuery { Id = id });
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

            return RedirectToAction("Details", "Tournament", new { id = group.TournamentId });
        }
    }
}
