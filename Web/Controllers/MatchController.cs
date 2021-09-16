using MediatR;
using Core.Dtos;
using Core.ModelResponse;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.MatchModule.Get;
using Core.Modules.MatchModule.Add;
using Core.Modules.MatchModule.List;
using Core.Modules.MatchModule.Close;
using Core.Modules.MatchModule.Reset;

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

        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            AddMatchDto addMatchDto = await _mediator.Send(new GetGroupDetailsforMatchQuery { GroupId = id });
            return View(addMatchDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddMatchDto addMatchDto)
        {
            ActionResponse create = await _mediator.Send(new AddMatchCommand { AddMatchDto = addMatchDto });
            TempData["Title"] = create.Title;
            TempData["Message"] = create.Message;
            TempData["State"] = create.State.ToString();

            if (!create.IsSuccess)
                return View(addMatchDto);

            return RedirectToAction("Detail", "Group", new { id = addMatchDto.Group.Id });
        }

        public async Task<ActionResult> CloseMatch(int id)
        {
            MatchDto match = await _mediator.Send(new GetMatchQuery { Id = id });
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CloseMatch(MatchDto matchDto)
        {
            if(matchDto.IsClosed) //delete before update
                await _mediator.Send(new ResetMatchCommand { MatchDto = matchDto });

            ActionResponse update = await _mediator.Send(new CloseMatchCommand { MatchDto = matchDto });
            TempData["Title"] = update.Title;
            TempData["Message"] = update.Message;
            TempData["State"] = update.State.ToString();

            if (!update.IsSuccess)
                return View(matchDto);
            return RedirectToAction(nameof(Matchs), new { id = matchDto.GroupId });
        }
    }
}
