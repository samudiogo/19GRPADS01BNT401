using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using _19GRPADS01BNT401_Assessment.Domain.Entities;
using _19GRPADS01BNT401_Assessment.Infra.Data;
using _19GRPADS01BNT401_Assessment.UiApi.Models;

namespace _19GRPADS01BNT401_Assessment.UiApi.Services
{

    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAllAsync();
        Task<BookModel> GetByIdAsync(Guid id);

        Task<BookCreateEditModel> CreateAsync(BookCreateEditModel model);
        Task<BookCreateEditModel> UpdateAsync(Guid id, BookCreateEditModel model);
        Task<BookModel> DeleteAsync(Guid id);

    }

    public class BookService : IBookService
    {
        private readonly AssessmentDbContext _db;
        private readonly IMapper _mapper;

        public BookService(AssessmentDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookModel>> GetAllAsync()
        {
            var model = await _db.Books.ToListAsync();
            if (model == null) return new List<BookModel>();

            return _mapper.Map<List<BookModel>>(model);
        }

        public async Task<BookModel> GetByIdAsync(Guid id)
        {
            var model = await _db.Books.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (model == null) return null;

            return _mapper.Map<BookModel>(model);
        }

        public async Task<BookCreateEditModel> CreateAsync(BookCreateEditModel model)
        {
            using (var context = _db)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        model.Id = Guid.NewGuid();
                        var book = _mapper.Map<Book>(model);

                        if (book == null)
                            throw new Exception("Bad Request - Error at mapping DTO");
                        
                        _db.Books.Add(book);
                        await _db.SaveChangesAsync();
                        transaction.Commit();
                        return model;

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        public async Task<BookCreateEditModel> UpdateAsync(Guid id, BookCreateEditModel model)
        {

            using (var context = _db)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var book = new Book { Id = id };
                        var bookModel = _mapper.Map<Book>(model);

                        if (bookModel == null)
                            throw new Exception("Bad Request - Error at mapping DTO");

                        //book.BookAuthors = model.AuthorsId.Select(ba => new BookAuthor { BookId = book.Id, AuthorId = ba }).ToList();

                        _db.Books.Attach(book);

                        _db.Entry(book).CurrentValues.SetValues(bookModel);
                        await _db.SaveChangesAsync();

                        var bookAuthors = _db.BookAuthors.Where(ba => ba.BookId == book.Id).Select(p=> p.AuthorId).ToList();

                        foreach (var bookAuthor in bookModel.BookAuthors)
                        {
                            if (!bookAuthors.Contains(bookAuthor.AuthorId))
                            {
                                _db.BookAuthors.Add(bookAuthor);
                                await _db.SaveChangesAsync();
                            }
                        }

                        var toRemove = bookModel.BookAuthors.Select(p => p.AuthorId);
                        foreach (var bookAuthorId in bookAuthors)
                        {
                            if (!toRemove.Contains(bookAuthorId))
                            {
                                _db.BookAuthors.Remove(new BookAuthor {BookId = book.Id, AuthorId = bookAuthorId});
                                await _db.SaveChangesAsync();   
                            }
                        }
                        

                        transaction.Commit();
                        return model;

                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!BooksExists(model.Id))
                        {
                            transaction.Rollback();
                            Console.WriteLine(e);
                            return null;
                        }

                        throw;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }

        }

        public async Task<BookModel> DeleteAsync(Guid id)
        {
            var book = _db.Books.FirstOrDefault(f => f.Id.Equals(id));

            if (book == null) return null;

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return _mapper.Map<BookModel>(book);
        }
        private bool BooksExists(Guid id)
        {
            return _db.Books.Any(e => e.Id == id);
        }
    }
}