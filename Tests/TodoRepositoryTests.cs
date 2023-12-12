using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Data;
using WebApplication1.Models.TodoApi.Models;
using WebApplication1.Repositories;

namespace Tests
{
    public class TodoRepositoryTests
    {
        // This method configures the in-memory database for testing
        private AppDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryAppDb")
                .Options;
            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureDeleted();
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }

        // Test for GetAllAsync
        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            // Arrange
            using var context = GetDatabaseContext();
            context.TodoItems.AddRange(
                new TodoItem { Title = "Test 1", Description = "Description for Test 1" }, // Ensure Description is set
                new TodoItem { Title = "Test 2", Description = "Description for Test 2" }  // Ensure Description is set
            );
            context.SaveChanges();
            var repository = new Repository<TodoItem>(context);

            // Act
            var todos = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, todos.Count());
        }

        // Test for GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_ReturnsEntity_WhenIdExists()
        {
            // Arrange
            using var context = GetDatabaseContext();
            var testTodo = new TodoItem { Title = "Test GetByIdAsync", Description = "Description of GetByIdAsync" }; // Ensure Description is set
            context.TodoItems.Add(testTodo);
            await context.SaveChangesAsync(); // Make sure changes are saved to the context
            var repository = new Repository<TodoItem>(context);

            // Act
            var todo = await repository.GetByIdAsync(testTodo.Id);

            // Assert
            Assert.NotNull(todo);
            Assert.Equal(testTodo.Id, todo.Id);
            Assert.Equal("Test GetByIdAsync", todo.Title);
            Assert.Equal("Description of GetByIdAsync", todo.Description);
        }

        [Fact]
        public async Task CreateAsync_AddsEntityToDatabase()
        {
            // Arrange
            using var context = GetDatabaseContext();
            var repository = new Repository<TodoItem>(context);
            var todoItem = new TodoItem { Title = "Test Create", Description = "Description" };

            // Act
            await repository.CreateAsync(todoItem);

            // Assert
            var itemInDb = await context.TodoItems.FirstOrDefaultAsync(t => t.Title == "Test Create");
            Assert.NotNull(itemInDb);
            Assert.Equal("Description", itemInDb.Description);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntityInDatabase()
        {
            // Arrange
            using var context = GetDatabaseContext();
            var testTodo = new TodoItem { Title = "Test Update", Description = "Initial description" }; // Ensure Description is set
            context.TodoItems.Add(testTodo);
            await context.SaveChangesAsync();
            var repository = new Repository<TodoItem>(context);

            testTodo.Title = "Updated Title";
            testTodo.Description = "Updated description"; // Ensure Description is updated if required

            // Act
            await repository.UpdateAsync(testTodo);
            await context.Entry(testTodo).ReloadAsync(); // Reload the entity from the database

            // Assert
            var updatedTodoItem = await context.TodoItems.FindAsync(testTodo.Id);
            Assert.NotNull(updatedTodoItem);
            Assert.Equal("Updated Title", updatedTodoItem.Title);
            Assert.Equal("Updated description", updatedTodoItem.Description); // Ensure the assertion includes Description if it's required
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntityFromDatabase()
        {
            // Arrange
            using var context = GetDatabaseContext();
            var testTodo = new TodoItem { Title = "Test Delete", Description = "Initial description" }; // Ensure Description is set
            context.TodoItems.Add(testTodo);
            await context.SaveChangesAsync();
            var repository = new Repository<TodoItem>(context);

            // Act
            await repository.DeleteAsync(testTodo.Id);

            // Assert
            var itemInDb = await context.TodoItems.FindAsync(testTodo.Id);
            Assert.Null(itemInDb);
        }
    }
}
