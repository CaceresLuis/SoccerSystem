using MediatR;
using Newtonsoft.Json;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TeamModule.Get;
using Core.Modules.GroupModule.Add;
using Core.Modules.TournamentModule.Get;
using Core.Modules.GroupModule.Remove;
using Core.Modules.GroupModule.Get;

namespace Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
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

        //public async Task<ActionResult> Edit(int id)
        //{
        //    if (id < 1) return NotFound();

        //    OneTeamResponse team = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });

        //    return View(team);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(int id, Team team)
        //{
        //    team.Id = id;
        //    ActionResponse update = await _mediator.Send(new UpdateTeamCommand { Team = team });
        //    if (!update.IsSuccess)
        //    {
        //        OneTeamResponse response = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
        //        response.Data = update;
        //        return View(response);
        //    }

        //    TempData["Data"] = JsonConvert.SerializeObject(update);
        //    return RedirectToAction(nameof(Index));
        //}

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
