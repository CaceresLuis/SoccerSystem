using MediatR;
using Core.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupModule.Add;
using Core.Modules.GroupModule.Get;
using Core.Modules.GroupModule.List;
using Core.Modules.GroupModule.Update;
using Core.Modules.GroupModule.Remove;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GroupDto[]>> GetGroups()
        {
            return await _mediator.Send(new ListGroupQuery ());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetGroupEntity(int id)
        {
            return await _mediator.Send(new GetGroupQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutGroupEntity(int id, GroupDto groupDto)
        {
            groupDto.Id = id;
            return await _mediator.Send(new UpdateGroupCommand { Group = groupDto });
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostGroupEntity(GroupDto groupDto)
        {
            return await _mediator.Send(new AddGroupCommand { GroupDto = groupDto });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteGroupEntity(int id)
        {
            return await _mediator.Send(new RemoveGroupCommand { Id = id });
        }
    }
}
