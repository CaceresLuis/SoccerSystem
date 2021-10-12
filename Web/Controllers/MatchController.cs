using System;
using MediatR;
using Core.Dtos;
using Shared.Enums;
using Shared.Exceptions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.MatchModule.Get;
using Core.Modules.MatchModule.Add;
using Core.Modules.MatchModule.List;
using Core.Modules.MatchModule.Close;
using Core.Modules.MatchModule.Remove;
using Microsoft.AspNetCore.Authorization;
using Core.Dtos.DtosApi;

namespace Web.Controllers
{
    public class MatchController : Controller 
    {
        private readonly IMediator _mediator;

        public MatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Matchs(Guid id)
        {
            GroupMatchsDto list = await _mediator.Send(new ListMatchByGroupQuery { GroupId = id });
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(Guid id)
        {
            AddMatchDto addMatchDto = await _mediator.Send(new GetGroupDetailsforMatchQuery { GroupId = id });
            return View(addMatchDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddMatchDto addMatchDto)
        {                
            try
            {
                await _mediator.Send(new AddMatchCommand { AddMatchDto = addMatchDto });
                TempData["Title"] = "Success!";
                TempData["Message"] = "Macht added";
                TempData["State"] = State.success.ToString();
                return RedirectToAction("Detail", "Group", new { id = addMatchDto.Group.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return RedirectToAction(nameof(Create), new { id = addMatchDto.Group.Id });
            }           
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CloseMatch(Guid id)
        {
            MatchDto match = await _mediator.Send(new GetMatchQuery { Id = id });
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CloseMatch(CloseMatchDto closeMatchDto)
        {
            try
            {
                await _mediator.Send(new CloseMatchCommand { CloseMatchDto = closeMatchDto });
                TempData["Title"] = "Success";
                TempData["Message"] = "Macht Closet!";
                TempData["State"] = State.success.ToString();

                return RedirectToAction(nameof(Matchs), new { id = closeMatchDto.GroupId });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View(closeMatchDto);
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteMatch(Guid id)
        {
            MatchDto match = await _mediator.Send(new GetMatchQuery { Id = id });
            try
            {
                await _mediator.Send(new RemoveMatchCommand { Id = id });
                TempData["Title"] = "Deleted!";
                TempData["Message"] = $"Match has been deleted!";
                TempData["State"] = State.success.ToString();
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
            }

            return RedirectToAction("Matchs", "Match", new { Id = match.GroupId });
        }
    }
}
