using LearningPortal.Api.DTOs;
using LearningPortal.Api.Services.Interfaces;
using LearningPortal.Data;
using LearningPortal.Data.Model;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LearningPortal.Api.Services
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _context;

        public GroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupDTO>> GetAllGroupsAsync()
        {
            var groups = await _context.Groups.ToListAsync();
            return groups.Adapt<IEnumerable<GroupDTO>>();
        }

        public async Task<GroupDTO> GetGroupByIdAsync(long id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) throw new KeyNotFoundException("Group not found.");
                return group.Adapt<GroupDTO>();
        }

        public async Task<GroupDTO> CreateGroupAsync(CreateGroupDTO group)
        {
            var entity = group.Adapt<Group>();
            _context.Groups.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Adapt<GroupDTO>();
        }

        public async Task<GroupDTO> UpdateGroupAsync(long id, UpdateGroupDTO group)
        {
            var entity = await _context.Groups.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Group not found.");
            
            entity.Name = group.Name;
            entity.Description = group.Description;

            await _context.SaveChangesAsync();
            return entity.Adapt<GroupDTO>();
        }

        public async Task<bool> DeleteGroupAsync(long id)
        {
            var entity = await _context.Groups.FindAsync(id);
            if (entity == null) return false;

            _context.Groups.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
