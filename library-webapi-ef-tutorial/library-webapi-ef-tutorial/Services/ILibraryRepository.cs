using DBFirstLibrary;

namespace library_webapi_ef_tutorial.Services
{
    public interface ILibraryRepository
    {
        IQueryable<Book> GetAllBooks();
        Book? GetAllBooks(int bookId);

        Task<ICollection<Book>> GetAllBooksAsync();
        Task<Book?> GetAllBooksAsync(int? bookId);

        void CreateBook(Book book);
        Task CreateBookAsync(Book bookEntity);
        void UpdateBook(int bookId, Book book_data);
        void DeleteBook(int bookId);
        void Save();
        Task<int> SaveAsync(CancellationToken ct);

    }
}
