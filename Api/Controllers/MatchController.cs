using System;
using MediatR;
using Core.Dtos;
using Core.Dtos.DtosApi;
using Core.Dtos.AddDtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.MatchModule.List;
using Core.Modules.MatchModule.Add;
using Core.Modules.MatchModule.Close;
using Core.Modules.MatchModule.Remove;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GroupMatchsDto>> GetMatchs(Guid id)
        {
            return await _mediator.Send(new ListMatchByGroupQuery { GroupId = id });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PostMatchEntity(AddMatchDtoApi addMatchDtoApi)
        {
            AddMatchDto addMatchDto = new AddMatchDto
            { 
                Date = addMatchDtoApi.Date,  
                Hour = addMatchDtoApi.Hour,
                GroupId = addMatchDtoApi.GroupId,
                LocalId = addMatchDtoApi.LocalId,
                VisitorId = addMatchDtoApi.VisitorId
            };
            return await _mediator.Send(new AddMatchCommand { AddMatchDto = addMatchDto });
        }

        [HttpPost]
        [Route("Close")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> Close(CloseMatchDto closeMatchDto)
        {
            return await _mediator.Send(new CloseMatchCommand { CloseMatchDto = closeMatchDto });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> DeleteMatchEntity(Guid id)
        {
            return await _mediator.Send(new RemoveMatchCommand { Id = id });
        }
    }
}
