using todobackend.Dtos;
using todobackend.Models;
using todobackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace todobackend.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public TodoController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var authors = await _libraryService.GetTodosAsync();

            if (authors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
            }

            return StatusCode(StatusCodes.Status200OK, authors);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetTodo(long id)
        {
            Todo todo = await _libraryService.GetTodoAsync(id);

            if (todo == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Todo found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] TodoInputDto todo)
        {
            var dbTodo = await _libraryService.AddTodoAsync(todo);

            if (dbTodo == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{todo.Title} could not be added.");
            }

            return CreatedAtAction("GetTodo", new { id = dbTodo.Id }, dbTodo);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateTodo(long id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            Todo dbTodo = await _libraryService.UpdateTodoAsync(todo);

            if (dbTodo == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{todo.Title} could not be updated");
            }

            return StatusCode(StatusCodes.Status200OK, todo);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteTodo(long id)
        {
            var todo = await _libraryService.GetTodoAsync(id);
            (bool status, string message) = await _libraryService.DeleteTodoAsync(todo);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, todo);
        }
    }
}
