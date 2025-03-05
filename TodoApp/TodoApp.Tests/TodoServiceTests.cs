using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Services;
using Xunit;

namespace TodoApp.Tests
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _todoService = new TodoService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllTodosAsync_ReturnsAllTodos()
        {
            // Arrange
            var todos = new List<TodoItem> { new TodoItem { Id = 1, Title = "Test Todo" } };
            _mockRepo.Setup(repo => repo.GetAllTodosAsync()).ReturnsAsync(todos);

            // Act
            var result = await _todoService.GetAllTodosAsync();

            // Assert
            Assert.Equal(todos, result);
        }

        [Fact]
        public async Task GetTodoByIdAsync_ReturnsTodoItem()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };
            _mockRepo.Setup(repo => repo.GetTodoByIdAsync(1)).ReturnsAsync(todo);

            // Act
            var result = await _todoService.GetTodoByIdAsync(1);

            // Assert
            Assert.Equal(todo, result);
        }

        [Fact]
        public async Task AddTodoAsync_AddsTodo()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };

            // Act
            await _todoService.AddTodoAsync(todo);

            // Assert
            _mockRepo.Verify(repo => repo.AddTodoAsync(todo), Times.Once);
        }

        [Fact]
        public async Task UpdateTodoAsync_UpdatesTodo()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };

            // Act
            await _todoService.UpdateTodoAsync(todo);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateTodoAsync(todo), Times.Once);
        }

        [Fact]
        public async Task DeleteTodoAsync_DeletesTodo()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };
            _mockRepo.Setup(repo => repo.GetTodoByIdAsync(1)).ReturnsAsync(todo);

            // Act
            await _todoService.DeleteTodoAsync(1);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteTodoAsync(1), Times.Once);
        }

        [Fact]
        public async Task ToggleStatusAsync_TogglesStatus()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo", IsCompleted = false };
            _mockRepo.Setup(repo => repo.GetTodoByIdAsync(1)).ReturnsAsync(todo);
            _mockRepo.Setup(repo => repo.ToggleStatusAsync(1)).Callback(() => todo.IsCompleted = !todo.IsCompleted);

            // Act
            await _todoService.ToggleStatusAsync(1);

            // Assert
            Assert.True(todo.IsCompleted);
            _mockRepo.Verify(repo => repo.ToggleStatusAsync(1), Times.Once);
        }
    }
}