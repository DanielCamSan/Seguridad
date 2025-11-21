using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        public BookService(IBookRepository repo)
        {
            _repo = repo;
        }
        public async Task<Book> CreateBook(CreateBookDto dto)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                AuthorName = dto.AuthorName,
            };
            await _repo.Add(book);
            return book;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Book> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }
        public async Task<Book> UpdateBook(UpdateBookDto dto, Guid id)
        {
            Book? book = await GetOne(id);
            if (book == null) throw new Exception("Book doesnt exist.");

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.AuthorName = dto.AuthorName;

            await _repo.Update(book);
            return book;
        }
        public async Task DeleteBook(Guid id)
        {
            Book? book = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (book == null) return;
            await _repo.Delete(book);
        }
    }
}