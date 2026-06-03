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
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
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
            try
            {
                return Ok(await _groupService.GetGroupByIdAsync(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Group not found.");
            }        
        }

        //POST: api/Group
        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupDTO createGroupDTO)
        {
            var result = await _groupService.CreateGroupAsync(createGroupDTO);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
                );
        }


        //PUT: api/Group/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateGroupDTO groupDto)
        {
            try
            {
                return Ok(await _groupService.UpdateGroupAsync(id, groupDto.Adapt<UpdateGroupDTO>()));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Group not found.");
            }
            catch (InvalidOperationException)
            {
                return Conflict("This version is outdated.");
            }
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