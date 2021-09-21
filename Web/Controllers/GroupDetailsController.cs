using MediatR;
using AutoMapper;
using Web.ViewModel;
using Shared.Enums;
using Shared.Exceptions;
using Core.ModelResponse;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.GroupDetailsModule.Add;
using Core.Modules.GroupDetailsModule.Get;
using Core.Modules.GroupDetailsModule.Remove;

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
            try
            {
                //TODO: verificar que los grupos del torneo estan activos y quitar los team
                GroupDetailsResponse groupDetailsResponse = await _mediator.Send(new GetGroupDetailsByGroupQuery { IdGroup = idGroup, IdTournament = idTournament });
                CreateGroupDetailsViewModels detailsViewModel = _mapper.Map<CreateGroupDetailsViewModels>(groupDetailsResponse);

                return View(detailsViewModel);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;
                return View();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateGroupDetailsViewModels groupDetail)
        {
            try
            {
                GroupDetailsResponse groupDetailsResponse = _mapper.Map<GroupDetailsResponse>(groupDetail);
                bool create = await _mediator.Send(new AddGroupDetailsCommand { GroupDetail = groupDetailsResponse });
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
            try
            {
                int delete = await _mediator.Send(new RemoveGroupDetailCommand { Id = id });
                TempData["Title"] = "Deleted!";
                TempData["Message"] = "Team has been deleted!";
                TempData["State"] = $"{State.success}"; 
                return RedirectToAction("Detail", "Group", new { id = delete });
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;

                return RedirectToAction("Detail", "Group", new { id = int.Parse(e.Error.Code) });
            }    
        }
    }
}
