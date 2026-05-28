using LearningPortal.Api.DTOs;
using LearningPortal.Api.Services;
using LearningPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearningPortal.Api.Tests
{
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
        public async Task GetAllAsync_ReturnsAllGroups()
        {
            //Arrange
            var context = CreateContext(); //fresh empty database
            //insert some groups into the fake database
            context.Groups.AddRange(
                new Data.Model.Group { Name = "Group 1", Description = "First group" },
                new Data.Model.Group { Name = "Group 2", Description = "Second group" }
                );
            await context.SaveChangesAsync(); //saves them to fake database
            var service = new GroupService(context); //creates service with fake database

            //Act
            var result = await service.GetAllGroupsAsync(); //calls method being tested

            //Assert
            Assert.AreEqual(2, result.Count()); //asserts that 2 groups are returned

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => service.GetGroupByIdAsync(999)); //asserts that requesting non-existent group throws exception
        }


        [TestMethod]
        public async Task GetGroupByIdAsync_ReturnsGroup()
        {
            var context = CreateContext();
            var group = new Data.Model.Group { Name = "Group 1", Description = "First group" };
            context.Groups.Add(group);
            await context.SaveChangesAsync();
            var service = new GroupService(context);

            var result = await service.GetGroupByIdAsync(group.Id);
            Assert.AreEqual(group.Name, result.Name);
            Assert.AreEqual(group.Description, result.Description);
        }

        [TestMethod]
        public async Task GetGroupByIdAsync_ThrowsKeyNotFoundException_WhenNotFound()
        {
            var context = CreateContext();
            var service = new GroupService(context);

            var result = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => service.GetGroupByIdAsync(999));
        }

        [TestMethod]
        public async Task CreateGroupAsync_SavesAndReturnsGroup()
        {
            var context = CreateContext();
            var service = new GroupService(context);
            var dto = new CreateGroupDTO("New Group", "A Description");

            var result = await service.CreateGroupAsync(dto);

            Assert.AreEqual("New Group", result.Name);
            Assert.AreEqual("A Description", result.Description);
            Assert.AreEqual(1, context.Groups.Count());
        }

        [TestMethod]
        public async Task UpdateGroupAsync_UpdateAndReturnsGroup()
        {
            var context = CreateContext();
            var group = new Data.Model.Group { Name = "Old Name", Description = "Old Description" };
            context.Groups.Add(group);
            await context.SaveChangesAsync();

            var service = new GroupService(context);
            var dto = new UpdateGroupDTO(group.Version, "New Name", "New Description");

            var result = await service.UpdateGroupAsync(group.Id, dto);

            Assert.AreEqual("New Name", result.Name);
            Assert.AreEqual("New Description", result.Description);
        }

        [TestMethod]
        public async Task UpdateGroupAsync_ThrowsKeyNotFoundException_WhenNotFound()
        {
            var context = CreateContext();
            var service = new GroupService(context);

            var dto = new UpdateGroupDTO(1, "New Name", "New Description");
            var result = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => service.UpdateGroupAsync(999, dto));
        }

        [TestMethod]
        public async Task DeleteGroupAsync_RemovesGroup()
        {
            var context = CreateContext();
            var group = new Data.Model.Group { Name = "Group to Delete"};
            context.Groups.Add(group);
            await context.SaveChangesAsync();
            var service = new GroupService(context);

            var result = await service.DeleteGroupAsync(group.Id);

            Assert.IsTrue(result);
            Assert.AreEqual(0, context.Groups.Count());
        }

        [TestMethod]
        public async Task DeleteGroupAsync_ReturnsFalse_WhenNotFound()
        {
            var context = CreateContext();
            var service = new GroupService(context);
            var result = await service.DeleteGroupAsync(999);

            Assert.AreEqual(false, result);
        }
    }
}
