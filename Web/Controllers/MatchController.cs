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
using Core.Modules.MatchModule.Reset;
using Core.Modules.MatchModule.Remove;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class MatchController : Controller 
    {
        private readonly IMediator _mediator;

        public MatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Matchs(int id)
        {
            GroupMatchsDto list = await _mediator.Send(new ListMatchByGroupQuery { GroupId = id });
            return View(list);
        }

        public ActionResult Details()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(int id)
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
                return View(addMatchDto);
            }           
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CloseMatch(int id)
        {
            MatchDto match = await _mediator.Send(new GetMatchQuery { Id = id });
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CloseMatch(MatchDto matchDto)
        {
            try
            {
                if (matchDto.IsClosed) //delete before update
                    await _mediator.Send(new ResetMatchCommand { MatchDto = matchDto });

                await _mediator.Send(new CloseMatchCommand { MatchDto = matchDto });

                TempData["Title"] = "Success";
                TempData["Message"] = "Macht Closet!";
                TempData["State"] = State.success.ToString();

                return RedirectToAction(nameof(Matchs), new { id = matchDto.GroupId });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View(matchDto);
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteMatch(int id)
        {
            MatchDto match = await _mediator.Send(new GetMatchQuery { Id = id });
            try
            {
                await _mediator.Send(new RemoveMatchCommand { Id = id });
                TempData["Title"] = "Updated!";
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
