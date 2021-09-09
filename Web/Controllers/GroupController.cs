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
using AutoMapper;
using Web.ViewModel;
using Web.Models;

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

        public async Task<ActionResult> Create(int id)
        {
            if (id < 1) return NotFound();
            GroupViewModel groupView = new GroupViewModel { Group = new Group { }, Data = new ActionResponse { } };

            ATournamentResponse tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });
            if (tournamentResponse == null)
                return NotFound();

            groupView.Group.Tournament = _mapper.Map<Tournament>(tournamentResponse.Tournament);
            return View(groupView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Group group)
        {
            GroupResponse groupResponse = _mapper.Map<GroupResponse>(group);
            ActionResponse create = await _mediator.Send(new AddGroupCommand { Group = groupResponse });
            if (!create.IsSuccess)
            {
                GroupViewModel groupView = new GroupViewModel { Group = group, Data = create };
                return View(groupView);
            }

            TempData["Data"] = JsonConvert.SerializeObject(create);
            return RedirectToAction("Details", "Tournament", new { id = group.Tournament.Id });
        }

        public async Task<ActionResult> Detail(int id)
        {
            if (id < 1) return NotFound();

            GroupViewModel groupView = new GroupViewModel { Data = new ActionResponse { } };
            AGroupResponse response = await _mediator.Send(new GetFullGroupQuery { Id = id });

            groupView.Group = _mapper.Map<Group>(response.Group);

            if (TempData["Data"] != null)
                groupView.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(groupView);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id < 1) return NotFound();

            GroupResponse groupResponse = await _mediator.Send(new GetGroupQuery { Id = id });
            Group group = _mapper.Map<Group>(groupResponse);
            GroupViewModel groupView = new GroupViewModel { Group = group, Data = new ActionResponse { } };

            if (TempData["Data"] != null)
                groupView.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(groupView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Group group)
        {
            group.Id = id;
            GroupResponse groupResponse = _mapper.Map<GroupResponse>(group);
            ActionResponse update = await _mediator.Send(new UpdateGroupCommand { Group = groupResponse });
            if (!update.IsSuccess)
            {
                GroupViewModel groupView = new GroupViewModel { Group = group, Data = update };
                return View(groupView);
            }

            TempData["Data"] = JsonConvert.SerializeObject(update);
            return RedirectToAction("Details", "Tournament", new { id = group.Tournament.Id });
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return NotFound();

            GroupResponse group = await _mediator.Send(new GetGroupWithTournamentQuery { Id = id });
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
