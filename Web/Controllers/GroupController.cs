using MediatR;
using AutoMapper;
using Web.ViewModel;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Create(int id)
        {
            ATournamentResponse tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });
            TempData["Title"] = tournamentResponse.Data.Title;
            TempData["Message"] = tournamentResponse.Data.Message;
            TempData["State"] = tournamentResponse.Data.State.ToString();
            if (tournamentResponse == null)
                return RedirectToAction("Details", "Tournament", new { Id = id });

            TournamentViewModel tournament = _mapper.Map<TournamentViewModel>(tournamentResponse.Tournament);
            GroupViewModel groupView = new GroupViewModel { Tournament = tournament };

            return View(groupView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupViewModel groupView)
        {
            groupView.Id = 0;
            GroupResponse groupResponse = _mapper.Map<GroupResponse>(groupView);
            ActionResponse create = await _mediator.Send(new AddGroupCommand { Group = groupResponse });
            TempData["Title"] = create.Title;
            TempData["Message"] = create.Message;
            TempData["State"] = create.State.ToString();

            if (!create.IsSuccess)
                return View(groupView);

            return RedirectToAction("Details", "Tournament", new { id = groupView.Tournament.Id });
        }

        public async Task<ActionResult> Detail(int id)
        {
            AGroupResponse aGroupResponse = await _mediator.Send(new GetFullGroupQuery { Id = id });

            if (!aGroupResponse.Data.IsSuccess)
                return RedirectToAction("Details", "Tournament", new { Id = id });

            GroupViewModel groupView = _mapper.Map<GroupViewModel>(aGroupResponse.Group);

            return View(groupView);
        }

        public async Task<ActionResult> Edit(int id)
        {
            AGroupResponse AgroupResponse = await _mediator.Send(new GetFullGroupQuery { Id = id });
            TempData["Title"] = AgroupResponse.Data.Title;
            TempData["Message"] = AgroupResponse.Data.Message;
            TempData["State"] = AgroupResponse.Data.State.ToString();

            if (!AgroupResponse.Data.IsSuccess)
                return RedirectToAction("Details", "Tournament", new { Id = id });

            GroupViewModel groupView = _mapper.Map<GroupViewModel>(AgroupResponse.Group);

            return View(groupView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GroupViewModel groupView)
        {
            GroupResponse groupResponse = _mapper.Map<GroupResponse>(groupView);
            ActionResponse update = await _mediator.Send(new UpdateGroupCommand { Group = groupResponse });
            TempData["Title"] = update.Title;
            TempData["Message"] = update.Message;
            TempData["State"] = update.State.ToString();

            if (!update.IsSuccess)
                return View(groupView);

            return RedirectToAction("Details", "Tournament", new { id = groupView.Tournament.Id });
        }

        public async Task<ActionResult> Delete(int id)
        {
            GroupResponse group = await _mediator.Send(new GetGroupWithTournamentQuery { Id = id });
            ActionResponse delete = await _mediator.Send(new RemoveGroupCommand { Id = id });
            TempData["Title"] = delete.Title;
            TempData["Message"] = delete.Message;
            TempData["State"] = delete.State.ToString();

            return RedirectToAction("Details", "Tournament", new { id = group.Tournament.Id });
        }
    }
}
