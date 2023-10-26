using DBFirstLibrary;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace library_webapi_ef_tutorial.Services
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly libraryef1Context _dbContext;

        public LibraryRepository(libraryef1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Book> GetAllBooks()  // IQueryable vs IENumerable: https://www.youtube.com/watch?v=fvzuMVPlc6Y
        {
            // return _dbContext.Books;  // does lazy loading: Authors data is not requested, only Books data

            return _dbContext.Books.Include(b => b.Author);  // does eager loading: Authors data is  requested per JOIN with Books data. this is not necessary when using Dto classes with specific props of Author! EF Core will handle the rest
        }

        public Book? GetAllBooks(int bookId)
        {
            return _dbContext.Books.Include(b => b.Author).SingleOrDefault(book => book.BookId == bookId);
        }

        public virtual async Task<ICollection<Book>> GetAllBooksAsync()
        {
            try
            {
                //return await _context.Books.ToListAsync();
                return await _dbContext.Set<Book>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual async Task<Book?> GetAllBooksAsync(int? bookId)
        {
            return await _dbContext.Books.FindAsync(bookId);
        }

        public void CreateBook(Book book)
        {
            _dbContext.Books.Add(book);
            //_dbContext.SaveChanges();
            Save();
        }

        public virtual async Task CreateBookAsync(Book bookEntity)
        {
            if (bookEntity == null)
            {
                throw new ArgumentNullException($"{nameof(CreateBookAsync)} entity must not be null");
            }
            try
            {
                if (bookEntity != null)
                {
                    await _dbContext.Books.AddAsync(bookEntity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(bookEntity)} could not be saved: {ex.Message}");
            }
        }

        public async void UpdateBook(int bookId, Book bookData)
        {
            // Advantages of state tracking with dbcontext instance and SaveChanges() vs manual states manipulation: https://learn.microsoft.com/en-us/ef/core/change-tracking/#tracking-from-queries

            //ShowEntityState(_dbContext);
            //_dbContext.Entry(bookData).State = EntityState.Modified;
            //ShowEntityState(_dbContext);

            var book = GetAllBooks(bookId);

            book.Title = String.IsNullOrEmpty(bookData.Title) ? book.Title : bookData.Title;
            book.Author = bookData.Author is null ? book.Author : bookData.Author;
            book.PublishedDate = bookData.PublishedDate is null ? book.PublishedDate : bookData.PublishedDate;

            var cts = new CancellationTokenSource();
            cts.CancelAfter(4000);
            CancellationToken ct = cts.Token;

            //_dbContext.SaveChanges();
            // Save();
            await SaveAsync(ct);
        }

        public void DeleteBook(int bookId)
        {
            _dbContext.Books.Remove(GetAllBooks(bookId));
            //_dbContext.SaveChanges();
            Save();
        }

        public virtual void Save()
        {
            _dbContext.SaveChanges();
        }

        public virtual async Task<int> SaveAsync(CancellationToken ct)
        {
            int records = 0;
            IDbContextTransaction tx = null;
            //await Task.Delay(5000);
            if (ct.IsCancellationRequested)
            {
                ct.ThrowIfCancellationRequested();
            }

            try
            {
                using (tx = await _dbContext.Database.BeginTransactionAsync())
                {
                    records = await _dbContext.SaveChangesAsync();
                    tx.Commit();
                    return records;
                }
            }
            catch (DbUpdateConcurrencyException ex)  // area for resolving a concurrency conflict (thrown only on update and delete, never on add): https://learn.microsoft.com/en-us/ef/core/saving/concurrency?tabs=data-annotations#resolving-concurrency-conflicts
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Book)
                    {
                        var originalValues = entry.OriginalValues;  // originally retrieved before any edits 
                        var proposedValues = entry.CurrentValues;  // application was attempting to write
                        var databaseValues = entry.GetDatabaseValues();  // currently stored in the database

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            proposedValues[property] = databaseValue;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);

                        await SaveAsync(ct);  // TODO: Is recursion here ok?
                    }
                    else
                    {
                        throw new NotSupportedException("Unable to save changes. The book details was updated by another user. " + entry.Metadata.Name);
                    }
                }
                throw ex;
            }
            catch (DbUpdateException ex)
            {
                SqlException s = ex.InnerException as SqlException;
                var errorMessage = $"{ex.Message}" + " {ex?.InnerException.Message}" + " rolling back…";
                tx.Rollback();
            }
            return records;
        }

        public static void ShowEntityState(libraryef1Context context)
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries())
            {
                //Discards are local variables which you can assign but cannot read from. i.e. they are “write-only” local variables.
                _ = ($"type: {entry.Entity.GetType().Name}, state: {entry.State}," + $" {entry.Entity}");
            }
        }
    }
}
