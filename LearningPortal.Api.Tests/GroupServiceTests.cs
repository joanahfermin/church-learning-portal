using LearningPortal.Api.Dtos;
using LearningPortal.Api.Services;
using LearningPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearningPortal.Api.Tests;

[TestClass]
public sealed class GroupServiceTests
{
    private AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [TestMethod]
    public async Task GetAllGroupsAsync_ReturnsAllGroups()
    {
        // Arrange
        var context = CreateContext();
        context.Groups.AddRange(
            new Data.Model.Group { Name = "Group 1", Description = "First group" },
            new Data.Model.Group { Name = "Group 2", Description = "Second group" }
        );
        await context.SaveChangesAsync();
        var service = new GroupService(context);

        // Act
        var result = await service.GetAllGroupsAsync();

        // Assert
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public async Task GetGroupByIdAsync_ReturnsGroup()
    {
        // Arrange
        var context = CreateContext();
        var group = new Data.Model.Group { Name = "Group 1", Description = "First group" };
        context.Groups.Add(group);
        await context.SaveChangesAsync();
        var service = new GroupService(context);

        // Act
        var result = await service.GetGroupByIdAsync(group.Id);

        // Assert
        Assert.AreEqual(group.Name, result.Name);
        Assert.AreEqual(group.Description, result.Description);
    }

    [TestMethod]
    public async Task GetGroupByIdAsync_ThrowsKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var context = CreateContext();
        var service = new GroupService(context);

        // Act & Assert
        try
        {
            await service.GetGroupByIdAsync(999);
            Assert.Fail("Expected KeyNotFoundException");
        }
        catch (KeyNotFoundException) { }
    }

    [TestMethod]
    public async Task CreateGroupAsync_SavesAndReturnsGroup()
    {
        // Arrange
        var context = CreateContext();
        var service = new GroupService(context);
        var dto = new CreateGroupDto { Name = "New Group", Description = "A Description" };

        // Act
        var result = await service.CreateGroupAsync(dto);

        // Assert
        Assert.AreEqual("New Group", result.Name);
        Assert.AreEqual("A Description", result.Description);
        Assert.AreEqual(1, context.Groups.Count());
    }

    [TestMethod]
    public async Task UpdateGroupAsync_UpdatesAndReturnsGroup()
    {
        // Arrange
        var context = CreateContext();
        var group = new Data.Model.Group { Name = "Old Name", Description = "Old Description" };
        context.Groups.Add(group);
        await context.SaveChangesAsync();
        var originalVersion = group.Version; // capture before update
        var service = new GroupService(context);
        var dto = new UpdateGroupDto { Version = originalVersion, Name = "New Name", Description = "New Description" };

        // Act
        var result = await service.UpdateGroupAsync(group.Id, dto);

        // Assert
        Assert.AreEqual("New Name", result.Name);
        Assert.AreEqual("New Description", result.Description);
        Assert.AreEqual(originalVersion + 1, result.Version);
    }

    [TestMethod]
    public async Task UpdateGroupAsync_ThrowsKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var context = CreateContext();
        var service = new GroupService(context);
        var dto = new UpdateGroupDto { Version = 0, Name = "New Name", Description = "New Description" };

        // Act & Assert
        try
        {
            await service.UpdateGroupAsync(999, dto);
            Assert.Fail("Expected KeyNotFoundException");
        }
        catch (KeyNotFoundException) { }
    }

    [TestMethod]
    public async Task UpdateGroupAsync_ThrowsInvalidOperationException_WhenVersionOutdated()
    {
        // Arrange
        var context = CreateContext();
        var group = new Data.Model.Group { Name = "Group", Description = "Description" };
        context.Groups.Add(group);
        await context.SaveChangesAsync();
        var service = new GroupService(context);
        var dto = new UpdateGroupDto { Version = 99, Name = "New Name", Description = "New Description" };

        // Act & Assert
        try
        {
            await service.UpdateGroupAsync(group.Id, dto);
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException) { }
    }

    [TestMethod]
    public async Task DeleteGroupAsync_RemovesGroup()
    {
        // Arrange
        var context = CreateContext();
        var group = new Data.Model.Group { Name = "Group to Delete" };
        context.Groups.Add(group);
        await context.SaveChangesAsync();
        var service = new GroupService(context);

        // Act
        await service.DeleteGroupAsync(group.Id);

        // Assert
        Assert.AreEqual(0, context.Groups.Count());
    }

    [TestMethod]
    public async Task DeleteGroupAsync_ThrowsKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var context = CreateContext();
        var service = new GroupService(context);

        // Act & Assert
        try
        {
            await service.DeleteGroupAsync(999);
            Assert.Fail("Expected KeyNotFoundException");
        }
        catch (KeyNotFoundException) { }
    }
}
