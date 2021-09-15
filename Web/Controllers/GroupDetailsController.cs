using MediatR;
using AutoMapper;
using Web.ViewModel;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupDetailsModule.Add;
using Core.Modules.GroupDetailsModule.Get;
using Core.Modules.GroupDetailsModule.Remove;

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
            GroupDetailsResponse groupDetailsResponse = await _mediator.Send(new GetGroupDetailsByGroupQuery { IdGroup = id });
            TempData["Title"] = groupDetailsResponse.Data.Title;
            TempData["Message"] = groupDetailsResponse.Data.Message;
            TempData["State"] = groupDetailsResponse.Data.State.ToString();

            CreateGroupDetailsViewModels detailsViewModel = _mapper.Map<CreateGroupDetailsViewModels>(groupDetailsResponse);

            return View(detailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateGroupDetailsViewModels groupDetail)
        {
            GroupDetailsResponse groupDetailsResponse = _mapper.Map<GroupDetailsResponse>(groupDetail);
            ActionResponse create = await _mediator.Send(new AddGroupDetailsCommand { GroupDetail = groupDetailsResponse });
            TempData["Title"] = create.Title;
            TempData["Message"] = create.Message;
            TempData["State"] = create.State.ToString();

            if (!create.IsSuccess)
                return View(groupDetail);

            return RedirectToAction("Detail", "Group", new { id = groupDetail.Group.Id });
        }

        public async Task<ActionResult> Delete(int id)
        {
            RGroupDetailsResponse delete = await _mediator.Send(new RemoveGroupDetailCommand { Id = id });
            TempData["Title"] = delete.Data.Title;
            TempData["Message"] = delete.Data.Message;
            TempData["State"] = delete.Data.State.ToString();

            return RedirectToAction("Detail", "Group", new { id = delete.GroupId });
        }
    }
}
