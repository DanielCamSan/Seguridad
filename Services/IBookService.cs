using Security.Models;
using Security.Models.DTOS;
namespace Security.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetOne(Guid id);
        Task<Book> CreateBook(CreateBookDto dto);
        Task<Book> UpdateBook(UpdateBookDto dto, Guid id);
        Task DeleteBook(Guid id);
    }
}
