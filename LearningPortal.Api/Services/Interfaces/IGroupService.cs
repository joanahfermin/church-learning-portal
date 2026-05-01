using LearningPortal.Api.DTOs;

namespace LearningPortal.Api.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDTO>> GetAllGroupsAsync();
        Task<GroupDTO> GetGroupByIdAsync(long id);
        Task<GroupDTO> CreateGroupAsync(GroupDTO group);
        Task<GroupDTO> UpdateGroupAsync(long id, GroupDTO group);
        Task<bool> DeleteGroupAsync(long id);
    }
}
