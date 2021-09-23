using System;
using MediatR;
using Core.Dtos;
using AutoMapper;
using Shared.Enums;
using Shared.Exceptions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Modules.RoleModule.Add;
using Core.Modules.RoleModule.List;
using Microsoft.AspNetCore.Identity;
using Core.Modules.RoleModule.Remove;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            List<IdentityRole> role = await _mediator.Send(new ListRolesQuery());
            List<RoleDto> roles = new List<RoleDto>();
            foreach (IdentityRole item in role)
            {
                RoleDto rol = new RoleDto
                {
                    Id = item.Id,
                    Name = item.Name
                };
                roles.Add(rol);
            }

            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleDto roleDto)
        {
            try
            {
                await _mediator.Send(new AddRoleCommand { Name = roleDto.Name });
                TempData["Title"] = "Registered";
                TempData["Message"] = $"The role: {roleDto.Name} has been created";
                TempData["State"] = $"{State.success}";
                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;

                return View();
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                bool delete = await _mediator.Send(new RemoveRoleCommand { Name = id });
                TempData["Title"] = "Deleted";
                TempData["Message"] = $"The role: {id} has been deleted";
                TempData["State"] = $"{State.success}";
            }
            catch (Exception)
            {
                TempData["Title"] = "Error";
                TempData["Message"] = $"The role: {id} has not been deleted";
                TempData["State"] = $"{State.error}";
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
