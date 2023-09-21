using DBFirstLibrary;

namespace library_webapi_ef_tutorial.Services
{
    public interface ILibraryRepository
    {
        IQueryable<Book> ReadBooks();
        Book ReadBooks(int bookId);
        void CreateBook(Book book);
        void UpdateBook(int bookId, Book book_data);
        void DeleteBook(int bookId);
    }
}
