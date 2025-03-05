using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<TodoItem>> GetAllTodosAsync()
        {
            return await _todoRepository.GetAllTodosAsync();
        }

        public async Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            return await _todoRepository.GetTodoByIdAsync(id);
        }

        public async Task AddTodoAsync(TodoItem todo)
        {
            await _todoRepository.AddTodoAsync(todo);
        }

        public async Task UpdateTodoAsync(TodoItem todo)
        {
            await _todoRepository.UpdateTodoAsync(todo);
        }

        public async Task DeleteTodoAsync(int id)
        {
            await _todoRepository.DeleteTodoAsync(id);
        }

        public async Task ToggleStatusAsync(int id)
        {
            await _todoRepository.ToggleStatusAsync(id);
        }
    }
}