using todobackend.Dtos;
using todobackend.Models;

namespace todobackend.Services
{
    public interface ILibraryService
    {

        // Todo Services
        Task<List<Todo>> GetTodosAsync(); // GET All Todos
        Task<Todo> GetTodoAsync(long id); // Get Single Todo
        Task<Todo> AddTodoAsync(TodoInputDto todo); // POST New Todo
        Task<Todo> UpdateTodoAsync(Todo todo); // PUT Todo
        Task<(bool, string)> DeleteTodoAsync(Todo todo); // DELETE Todo



    }
}
