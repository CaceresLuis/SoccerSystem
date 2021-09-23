using MediatR;
using Core.Dtos;
using Shared.Enums;
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

        public async Task<ActionResult> Create(int idGroup, int idTournament)
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
                bool create = await _mediator.Send(new AddGroupTeamCommand { AddGroupTeamDto = addGroupTeamDto });
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

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                int delete = await _mediator.Send(new RemoveGroupDetailCommand { Id = id });
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

                return RedirectToAction("Detail", "Group", new { id = int.Parse(e.Error.Code) });
            }    
        }
    }
}
