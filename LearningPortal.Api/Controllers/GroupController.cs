using LearningPortal.Api.DTOs;
using LearningPortal.Api.Services.Interfaces;
using LearningPortal.Data;
using LearningPortal.Data.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

namespace LearningPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IGroupService _groupService;

        public GroupController(AppDbContext context, IGroupService groupService)
        {
            _context = context;
            _groupService = groupService;
        }

        //GET: api/Group
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _groupService.GetAllGroupsAsync());
        }

        //Get: api/Group/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _groupService.GetGroupByIdAsync(id));
        }

        //POST: api/Group
        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupDTO createGroupDTO)
        {
            return Ok(await _groupService.CreateGroupAsync(createGroupDTO.Adapt<CreateGroupDTO>()));
        }

        //PUT: api/Group/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateGroupDTO groupDto)
        {
            return Ok(await _groupService.UpdateGroupAsync(id, groupDto.Adapt<UpdateGroupDTO>()));
        }

        //DELETE: api/Group/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _groupService.DeleteGroupAsync(id);
            if (!result)
                return NotFound("Group not found.");

            return Ok("Group deleted successfully.");
        }
    }
}