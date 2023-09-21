using DBFirstLibrary;

namespace library_webapi_ef_tutorial.Services
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly libraryef1Context _dbContext;

        public LibraryRepository(libraryef1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public void DeleteBook(int bookId)
        {
            _dbContext.Books.Remove(ReadBooks(bookId)); 
            _dbContext.SaveChanges();
        }

        public IQueryable<Book> ReadBooks()
        {
            return _dbContext.Books;
        }

        public Book ReadBooks(int bookId)
        {
            return _dbContext.Books.SingleOrDefault(book => book.BookId == bookId);
        }

        public void UpdateBook(int bookId, Book book_data)
        {
            var book = ReadBooks(bookId);

            book.Title = book_data.Title;
            book.Author = book_data.Author;
            book.PublishedDate = book_data.PublishedDate;
            _dbContext.SaveChanges();
        }
    }
}
