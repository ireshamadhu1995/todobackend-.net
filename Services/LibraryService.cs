using todobackend.Data;
using todobackend.Models;
using todobackend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace todobackend.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _db;

        public LibraryService(AppDbContext db)
        {
            _db = db;
        }

        #region Todos

        public async Task<List<Todo>> GetTodosAsync()
        {
            try
            {
                return await _db.Todos.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Todo> GetTodoAsync(long id)
        {
            try
            {
                return await _db.Todos.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Todo> AddTodoAsync(TodoInputDto todo)
        {
            try
            {
                Console.WriteLine("BBBBBBBBBBBBBBB");
                Todo entity = new Todo
                {
                    Title = todo.Title,
                    Description = todo.Description
                };
                await _db.Todos.AddAsync(entity);
                await _db.SaveChangesAsync();

                return (entity); ;
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Todo> UpdateTodoAsync(Todo todo)
        {
            try
            {
                _db.Entry(todo).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return todo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTodoAsync(Todo todo)
        {
            try
            {
                var dbTodo = await _db.Todos.FindAsync(todo.Id);

                if (dbTodo == null)
                {
                    return (false, "Author could not be found");
                }

                _db.Todos.Remove(todo);
                await _db.SaveChangesAsync();

                return (true, "Todo got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Todos
    }
}
