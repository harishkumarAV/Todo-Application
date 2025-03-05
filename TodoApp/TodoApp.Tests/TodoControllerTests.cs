using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.Controllers;
using TodoApp.Models;
using TodoApp.Services;
using Xunit;

namespace TodoApp.Tests
{
    public class TodoControllerTests
    {
        private readonly Mock<ITodoService> _mockService;
        private readonly TodoController _controller;

        public TodoControllerTests()
        {
            _mockService = new Mock<ITodoService>();
            _controller = new TodoController(_mockService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfTodos()
        {
            // Arrange
            var todos = new List<TodoItem> { new TodoItem { Id = 1, Title = "Test Todo" } };
            _mockService.Setup(service => service.GetAllTodosAsync()).ReturnsAsync(todos);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TodoItem>>(viewResult.ViewData.Model);
            Assert.Equal(todos, model);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectToActionResult()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };

            // Act
            var result = await _controller.Create(todo);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewResult_WithTodoItem()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };
            _mockService.Setup(service => service.GetTodoByIdAsync(1)).ReturnsAsync(todo);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TodoItem>(viewResult.ViewData.Model);
            Assert.Equal(todo, model);
        }

        [Fact]
        public async Task Edit_Post_ReturnsRedirectToActionResult()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };

            // Act
            var result = await _controller.Edit(todo);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_Get_ReturnsViewResult_WithTodoItem()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };
            _mockService.Setup(service => service.GetTodoByIdAsync(1)).ReturnsAsync(todo);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TodoItem>(viewResult.ViewData.Model);
            Assert.Equal(todo, model);
        }

        [Fact]
        public async Task DeleteConfirmed_Post_ReturnsRedirectToActionResult()
        {
            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task ToggleStatus_Post_ReturnsRedirectToActionResult()
        {
            // Act
            var result = await _controller.ToggleStatus(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}