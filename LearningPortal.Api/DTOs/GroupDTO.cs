using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPortal.Api.DTOs
{
    public record GroupDTO(long Id, uint Version, string Name, string? Description);

    public record CreateGroupDTO(string Name, string? Description);

    public record UpdateGroupDTO(uint Version, string Name, string? Description);

}
