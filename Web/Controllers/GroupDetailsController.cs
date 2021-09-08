using System;
using MediatR;
using Newtonsoft.Json;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TeamModule.Get;
using Core.Modules.GroupModule.Get;
using Core.Modules.TeamModule.Remove;
using Core.Modules.TeamModule.Update;
using Core.Modules.GroupDetailsModule.Add;
using Core.Modules.GroupDetailsModule.Get;
using Core.Modules.GroupDetailsModule.Update;

namespace Web.Controllers
{
    public class GroupDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public GroupDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public async Task<ActionResult> Index()
        //{
        //    Team[] team = await _mediator.Send(new ListTeamsQuery());

        //    ListTeamResponse response = new ListTeamResponse { Teams = team };
        //    response.Data = new ActionResponse { };

        //    if(team.Length <= 1)
        //        return View(response);


        //    if (TempData["Data"] != null)
        //        response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

        //    return View(response);
        //}

        public async Task<ActionResult> Create(int id)
        {
            GroupDetailsResponse response = await _mediator.Send(new GetGroupDetailsByGroupQuery { IdGroup = id });
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupDetailsResponse groupDetail)
        {
            ActionResponse create = await _mediator.Send(new AddGroupDetailsCommand { GroupDetail = groupDetail });
            if (!create.IsSuccess)
            {
                OneGroupDetailsResponse response = new OneGroupDetailsResponse { };
                response.Data = create;
                return View(response);
            }

            OneGroupResponse group = await _mediator.Send(new GetFullGroupQuery { Id = groupDetail.Group.Id });
            TempData["Data"] = JsonConvert.SerializeObject(group);
            return RedirectToAction("Detail", "Group", new { id = group.Group.Id });
        }

        public async Task<ActionResult> Edit(int id)
        {
            OneGroupDetailsResponse response = await _mediator.Send(new GetGroupDetailsQuery { Id = id });
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, GroupDetail groupDetail)
        {
            groupDetail.Id = id;
            ActionResponse update = await _mediator.Send(new UpdateGroupDetailsCommand { GroupDetail = groupDetail });
            if (!update.IsSuccess)
            {
                OneGroupDetailsResponse response = await _mediator.Send(new GetGroupDetailsQuery { Id = id });
                response.Data = update;
                return View(response);
            }

            TempData["Data"] = JsonConvert.SerializeObject(update);
            return RedirectToAction("Detail", "Group", new { id = groupDetail.Group.Id });
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return NotFound();

            ActionResponse response = await _mediator.Send(new RemoveTeamCommand { IdTeam = id });
            if (response.IsSuccess)
            {
                TempData["Data"] = JsonConvert.SerializeObject(response);
                return RedirectToAction(nameof(Index));
            }

            TempData["Data"] = JsonConvert.SerializeObject(response);
            return RedirectToAction(nameof(Index));
        }
    }
}
