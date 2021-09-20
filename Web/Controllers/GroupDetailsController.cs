using MediatR;
using AutoMapper;
using Web.ViewModel;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.GroupDetailsModule.Add;
using Core.Modules.GroupDetailsModule.Get;
using Core.Modules.GroupDetailsModule.Remove;
using Shared.Enums;
using Shared.Exceptions;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class GroupDetailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GroupDetailsController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Create(int idGroup, int idTournament)
        {
            //TODO: verificar que los grupos del torneo estan activos y quitar los team
            GroupDetailsResponse groupDetailsResponse = await _mediator.Send(new GetGroupDetailsByGroupQuery { IdGroup = idGroup, IdTournament = idTournament });
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
            try
            {
                GroupDetailsResponse groupDetailsResponse = _mapper.Map<GroupDetailsResponse>(groupDetail);
                ActionResponse create = await _mediator.Send(new AddGroupDetailsCommand { GroupDetail = groupDetailsResponse });
                TempData["Title"] = "Added";
                TempData["Message"] = $"The team was added to the group";
                TempData["State"] = $"{State.success}";

                return RedirectToAction("Detail", "Group", new { id = groupDetail.Group.Id });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;

                return View(groupDetail);
            }
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
