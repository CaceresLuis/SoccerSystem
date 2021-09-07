using MediatR;
using Newtonsoft.Json;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupModule.Add;
using Core.Modules.GroupModule.Get;
using Core.Modules.GroupModule.Remove;
using Core.Modules.GroupModule.Update;
using Core.Modules.TournamentModule.Get;

namespace Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Create(int id)
        {
            if (id < 1) return NotFound();
            OneGroupResponse response = new OneGroupResponse { Group = new Group { } };
            response.Data = new ActionResponse { };

            OneTournamentResponse tournament = await _mediator.Send(new GetTournamentQuery { Id = id });
            if (tournament == null)
                return NotFound();

            response.Group.Tournament = tournament.Tournament;
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Group group)
        {
            ActionResponse create = await _mediator.Send(new AddGroupCommand { Group = group });
            if (!create.IsSuccess)
            {
                OneTeamResponse response = new OneTeamResponse { };
                response.Data = create;
                return View(response);
            }

            TempData["Data"] = JsonConvert.SerializeObject(create);
            return RedirectToAction("Details", "Tournament", new { id = group.Tournament.Id });
        }

        public async Task<ActionResult> Detail(int id)
        {
            if (id < 1) return NotFound();

            OneGroupResponse response = await _mediator.Send(new GetFullGroupQuery { Id = id });
            response.Data = new ActionResponse { };
            if (TempData["Data"] != null)
                response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(response);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id < 1) return NotFound();

            Group group = await _mediator.Send(new GetGroupQuery { Id = id });
            OneGroupResponse response = new OneGroupResponse { Group = group };
            response.Data = new ActionResponse { };

            if (TempData["Data"] != null)
                response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Group group)
        {
            group.Id = id;
            ActionResponse update = await _mediator.Send(new UpdateGroupCommand { Group = group });
            if (!update.IsSuccess)
            {
                Group getGroup = await _mediator.Send(new GetGroupQuery { Id = id });
                OneGroupResponse response = new OneGroupResponse { Group = getGroup };
                response.Data = update;
                return View(response);
            }

            TempData["Data"] = JsonConvert.SerializeObject(update);
            return RedirectToAction("Details", "Tournament", new { id = group.Tournament.Id });
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return NotFound();

            Group group = await _mediator.Send(new GetGroupWithTournamentQuery { Id = id });
            ActionResponse response = await _mediator.Send(new RemoveGroupCommand { Id = id });
            if (!response.IsSuccess)
            {
                TempData["Data"] = JsonConvert.SerializeObject(response);
                return RedirectToAction("Details", "Tournament", new { id = group.Tournament.Id });
            }

            TempData["Data"] = JsonConvert.SerializeObject(response);
            return RedirectToAction("Details", "Tournament", new { id = group.Tournament.Id });
        }
    }
}
