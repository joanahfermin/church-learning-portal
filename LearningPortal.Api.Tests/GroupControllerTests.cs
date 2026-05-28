using LearningPortal.Api.Controllers;
using LearningPortal.Api.DTOs;
using LearningPortal.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Moq;

namespace LearningPortal.Api.Tests;

[TestClass]
public class GroupControllerTests
{
    private GroupController CreateController(IGroupService service)
    {
        return new GroupController(service);
    }

    [TestMethod]
    public async Task GetAll_ReturnOk()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.GetAllGroupsAsync()).ReturnsAsync(new List<GroupDTO>
        {
            new GroupDTO(1, 0, "Group 1", "Description 1"),
            new GroupDTO(2, 0, "Group 2", "Description 2")

        });
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task GetById_ReturnsOk()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.GetGroupByIdAsync(1)).ReturnsAsync(new GroupDTO(1, 0, "Group 1", "Description 1"));
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.GetById(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task GetById_ReturnsNotFound_WhenGroupDoesNotExist()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s=> s.GetGroupByIdAsync(999)).ThrowsAsync(new KeyNotFoundException());
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.GetById(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));  
    }

    [TestMethod]
    public async Task DeleteByIdAsync_ReturnsOk()
    {
        // Arrange 
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.DeleteGroupAsync(1)).ReturnsAsync(true);
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task DeletebyIdAsync_ReturnsNotFound_WhenGroupDoesNotExist()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.DeleteGroupAsync(999)).ReturnsAsync(false);
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Delete(999);

        //Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task CreateGroupAsync_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        var createGroupDTO = new CreateGroupDTO("New Group", "New Group Description");
        var createdGroupDTO = new GroupDTO(1, 0, createGroupDTO.Name, createGroupDTO.Description);
        mockService.Setup(s => s.CreateGroupAsync(It.IsAny<CreateGroupDTO>()))
                   .ReturnsAsync(createdGroupDTO); 
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Create(createGroupDTO);

        // Assert
        Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
    }

    [TestMethod]
    public async Task UpdateGroup_ReturnsOk()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        var updateGroupDTO = new UpdateGroupDTO(1, "Updated Group", "Updated Description");
        var updatedGroupDTO = new GroupDTO(1, 0, updateGroupDTO.Name, updateGroupDTO.Description);
        mockService.Setup(s => s.UpdateGroupAsync(1, It.IsAny<UpdateGroupDTO>()))
                   .ReturnsAsync(updatedGroupDTO);
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Update(1, updateGroupDTO);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task UpdateGroup_ReturnsNotFound_WhenGroupDoesNotExist()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        var updateGroupDTO = new UpdateGroupDTO(999, "Updated Group", "Updated Description");
        mockService.Setup(s => s.UpdateGroupAsync(999, It.IsAny<UpdateGroupDTO>()))
                   .ThrowsAsync(new KeyNotFoundException());
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Update(999, updateGroupDTO);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
}
