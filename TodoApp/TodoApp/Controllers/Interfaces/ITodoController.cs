using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public interface ITodoController
    {
        Task<IActionResult> Index();
        IActionResult Create();
        Task<IActionResult> Create(TodoItem todo);
        Task<IActionResult> Edit(int id);
        Task<IActionResult> Edit(TodoItem todo);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> DeleteConfirmed(int id);
        Task<IActionResult> ToggleStatus(int id);
    }
}