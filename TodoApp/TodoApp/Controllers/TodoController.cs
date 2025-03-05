using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;
using System.Threading.Tasks;

namespace TodoApp.Controllers
{
    public class TodoController : Controller, ITodoController
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return View(todos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItem todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddTodoAsync(todo);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoItem todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.UpdateTodoAsync(todo);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _todoService.DeleteTodoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _todoService.ToggleStatusAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}