using LearningPortal.Api.Controllers;
using LearningPortal.Api.Dtos;
using LearningPortal.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LearningPortal.Api.Tests;

[TestClass]
public class GroupControllerTests
{
    [TestMethod]
    public async Task GetAll_ReturnsOk()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.GetAllGroupsAsync()).ReturnsAsync(new List<GroupDto>
        {
            new GroupDto { Id = 1, Version = 0, Name = "Group 1", Description = "Description 1" },
            new GroupDto { Id = 2, Version = 0, Name = "Group 2", Description = "Description 2" }
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
        mockService.Setup(s => s.GetGroupByIdAsync(1))
            .ReturnsAsync(new GroupDto { Id = 1, Version = 0, Name = "Group 1", Description = "Description 1" });
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
        mockService.Setup(s => s.GetGroupByIdAsync(999))
            .ThrowsAsync(new KeyNotFoundException());
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.GetById(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task Create_ReturnsCreatedAtAction()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        var createDto = new CreateGroupDto { Name = "New Group", Description = "New Description" };
        var createdDto = new GroupDto { Id = 1, Version = 0, Name = "New Group", Description = "New Description" };
        mockService.Setup(s => s.CreateGroupAsync(It.IsAny<CreateGroupDto>()))
            .ReturnsAsync(createdDto);
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Create(createDto);

        // Assert
        Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
    }

    [TestMethod]
    public async Task Update_ReturnsOk()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        var updateDto = new UpdateGroupDto { Version = 0, Name = "Updated Group", Description = "Updated Description" };
        var updatedDto = new GroupDto { Id = 1, Version = 1, Name = "Updated Group", Description = "Updated Description" };
        mockService.Setup(s => s.UpdateGroupAsync(1, It.IsAny<UpdateGroupDto>()))
            .ReturnsAsync(updatedDto);
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Update(1, updateDto);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task Update_ReturnsNotFound_WhenGroupDoesNotExist()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.UpdateGroupAsync(999, It.IsAny<UpdateGroupDto>()))
            .ThrowsAsync(new KeyNotFoundException());
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Update(999, new UpdateGroupDto { Version = 0, Name = "Updated Group", Description = "Updated Description" });

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task Update_ReturnsConflict_WhenVersionIsOutdated()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.UpdateGroupAsync(1, It.IsAny<UpdateGroupDto>()))
            .ThrowsAsync(new InvalidOperationException("Version conflict"));
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Update(1, new UpdateGroupDto { Version = 0, Name = "Updated Group", Description = "Updated Description" });

        // Assert
        Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
    }

    [TestMethod]
    public async Task Delete_ReturnsOk()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.DeleteGroupAsync(1)).Returns(Task.CompletedTask);
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task Delete_ReturnsNotFound_WhenGroupDoesNotExist()
    {
        // Arrange
        var mockService = new Mock<IGroupService>();
        mockService.Setup(s => s.DeleteGroupAsync(999))
            .ThrowsAsync(new KeyNotFoundException());
        var controller = new GroupController(mockService.Object);

        // Act
        var result = await controller.Delete(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
}
