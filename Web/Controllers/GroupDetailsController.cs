using System;
using MediatR;
using Newtonsoft.Json;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupModule.Get;
using Core.Modules.TeamModule.Remove;
using Core.Modules.GroupDetailsModule.Add;
using Core.Modules.GroupDetailsModule.Get;
using Core.Modules.GroupDetailsModule.Update;
using Web.ViewModel;
using AutoMapper;

namespace Web.Controllers
{
    public class GroupDetailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GroupDetailsController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Create(int id)
        {
            GroupDetailsResponse response = await _mediator.Send(new GetGroupDetailsByGroupQuery { IdGroup = id });
            CreateGroupDetailsViewModel detailsViewModel = _mapper.Map<CreateGroupDetailsViewModel>(response);

            return View(detailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateGroupDetailsViewModel groupDetail)
        {
            GroupDetailsResponse groupDetailsResponse = _mapper.Map<GroupDetailsResponse>(groupDetail);
            ActionResponse response = await _mediator.Send(new AddGroupDetailsCommand { GroupDetail = groupDetailsResponse });
            groupDetail.Data = response;
            if (!response.IsSuccess)
                return View(response);

            TempData["Data"] = JsonConvert.SerializeObject(response);
            return RedirectToAction("Detail", "Group", new { id = groupDetail.Group.Id });
        }

        public async Task<ActionResult> Edit(int id)
        {
            OneGroupDetailsResponse response = await _mediator.Send(new GetGroupDetailsQuery { Id = id });
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AGroupDetailResponse groupDetail)
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
