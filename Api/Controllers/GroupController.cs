using System;
using MediatR;
using Core.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupModule.Add;
using Core.Modules.GroupModule.Get;
using Core.Modules.GroupModule.List;
using Core.Modules.GroupModule.Update;
using Core.Modules.GroupModule.Remove;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
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
        [AllowAnonymous]
        public async Task<ActionResult<GroupDto[]>> GetGroups()
        {
            return await _mediator.Send(new ListGroupQuery ());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GroupDto>> GetGroupEntity(Guid id)
        {
            return await _mediator.Send(new GetGroupQuery { Id = id });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PutGroupEntity(Guid id, GroupDto groupDto)
        {
            groupDto.Id = id;
            return await _mediator.Send(new UpdateGroupCommand { Group = groupDto });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PostGroupEntity(GroupDto groupDto)
        {
            return await _mediator.Send(new AddGroupCommand { GroupDto = groupDto });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> DeleteGroupEntity(Guid id)
        {
            return await _mediator.Send(new RemoveGroupCommand { Id = id });
        }
    }
}
