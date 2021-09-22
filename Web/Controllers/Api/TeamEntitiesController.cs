using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Infrastructure.Models;
using MediatR;
using Core.Modules.TeamModule.List;
using Core.Dtos;
using Core.Modules.TeamModule.Get;
using Core.ModelResponse;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamEntitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DataContext _context;

        public TeamEntitiesController(DataContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<TeamDto[]>> GetTeams()
        {
            return await _mediator.Send(new ListTeamsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeamEntity(int id)
        {
            return await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
        }

        // PUT: api/TeamEntities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamEntity(int id, TeamEntity teamEntity)
        {
            if (id != teamEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(teamEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TeamEntities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TeamEntity>> PostTeamEntity(TeamEntity teamEntity)
        {
            _context.Teams.Add(teamEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamEntity", new { id = teamEntity.Id }, teamEntity);
        }

        // DELETE: api/TeamEntities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeamEntity>> DeleteTeamEntity(int id)
        {
            TeamEntity teamEntity = await _context.Teams.FindAsync(id);
            if (teamEntity == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(teamEntity);
            await _context.SaveChangesAsync();

            return teamEntity;
        }

        private bool TeamEntityExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
