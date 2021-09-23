﻿using MediatR;
using Core.Dtos;
using Core.Dtos.DtosApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.MatchModule.List;
using Core.Modules.MatchModule.Add;
using Core.Modules.MatchModule.Close;
using Core.Modules.MatchModule.Remove;

namespace Web.Controllers.Api
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

        [HttpGet]
        public async Task<ActionResult<GroupMatchsDto>> GetMatchs(int idGroup)
        {
            return await _mediator.Send(new ListMatchByGroupQuery { GroupId = idGroup });
        }

        [HttpPost]
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
        public async Task<ActionResult<bool>> Close(CloseMatchDto closeMatchDto)
        {
            MatchDto matchDto = new MatchDto 
            {
                Id = closeMatchDto.IdMatch,
                GoalsLocal = closeMatchDto.GoalsLocal,
                GoalsVisitor = closeMatchDto.GoalsVisitor,
                GroupId = closeMatchDto.GroupId,
                LocalId = closeMatchDto.LocalId,
                VisitorId = closeMatchDto.VisitorId
            };
            return await _mediator.Send(new CloseMatchCommand { MatchDto = matchDto });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteMatchEntity(int id)
        {
            return await _mediator.Send(new RemoveMatchCommand { Id = id });
        }
    }
}
