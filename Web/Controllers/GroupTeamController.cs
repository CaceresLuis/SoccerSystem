using System;
using MediatR;
using Core.Dtos;
using Shared.Enums;
using Core.Dtos.AddDtos;
using Shared.Exceptions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupTeamModule.Add;
using Core.Modules.GroupTeamModule.Get;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.GroupTeamModule.Remove;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class GroupTeamController : Controller
    {
        private readonly IMediator _mediator;

        public GroupTeamController(IMediator mediator)
        {            
            _mediator = mediator;
        }

        public async Task<ActionResult> Create(Guid idGroup, Guid idTournament)
        {
            try
            {
                AddGroupTeamDto addGroupTeamDto = await _mediator.Send(new GetGroupTeamByGroupQuery { IdGroup = idGroup, IdTournament = idTournament });
                return View(addGroupTeamDto);
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
        public async Task<ActionResult> Create(AddGroupTeamDto addGroupTeamDto)
        {
            try
            {
                var addGroupTeam = new AddGroupTeam { IdGroup = addGroupTeamDto.Group.Id, IdTeam = addGroupTeamDto.TeamId };
                bool create = await _mediator.Send(new AddGroupTeamCommand { AddGroupTeam = addGroupTeam });
                TempData["Title"] = "Added";
                TempData["Message"] = $"The team was added to the group";
                TempData["State"] = $"{State.success}";

                return RedirectToAction("Detail", "Group", new { id = addGroupTeamDto.Group.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;

                return View(addGroupTeamDto);
            }
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                Guid delete = await _mediator.Send(new RemoveGroupDetailCommand { Id = id });
                TempData["Title"] = "Deleted!";
                TempData["Message"] = "Team has been deleted!";
                TempData["State"] = $"{State.success}"; 
                return RedirectToAction("Detail", "Group", new { id = delete });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;

                return RedirectToAction("Detail", "Group", new { id = e.Error.Code });
            }    
        }
    }
}
