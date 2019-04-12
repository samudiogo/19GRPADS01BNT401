using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _19GRPADS01BNT401_Assessment.Domain.Entities;
using _19GRPADS01BNT401_Assessment.Infra.Data;
using _19GRPADS01BNT401_Assessment.UiApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _19GRPADS01BNT401_Assessment.UiApi.Services
{

   
    public interface IBookAuthorService
    {
        Task<IEnumerable<BookAuthorModel>> GetAllAsync();

        Task<BookAuthorModel> GetByIdsAsync(Guid bookId, Guid authorId);

        //getBooksByAuthor
        AuthorBooksListModel GetBookListByAuthor(Guid authorId);
        BookAuthorsListModel GetAuthorListByBook(Guid bookId);

        Task<BookAuthorModel> CreateAsync(BookAuthorAddEditModel model);
       

        Task<BookAuthorModel> DeleteAsync(Guid bookId, Guid authorId);
    }

    public class BookAuthorService:IBookAuthorService
    {
        private readonly AssessmentDbContext _db;
        private readonly IMapper _mapper;

        public BookAuthorService(AssessmentDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookAuthorModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<BookAuthorModel>>(await _db.BookAuthors.ToListAsync());
        }

       public  BookAuthorsListModel GetAuthorListByBook(Guid bookId)
        {

            var book = _db.Books.
                Include(o => o.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefault(b => b.Id.Equals(bookId));

            var authors = book.BookAuthors.Select(p => p.Author).Distinct();

            return new BookAuthorsListModel
            {
                BookId = book.Id,
                Title = book.Title,
                Isbn = book.Isbn,
                Year = book.Year,
                Authors = _mapper.Map<HashSet<AuthorModel>>(authors)
            };


        }

        public AuthorBooksListModel GetBookListByAuthor(Guid authorId)
        {

            var author = _db.Authors.Include(p => p.BookAuthors).FirstOrDefault(p => p.Id == authorId);

          
            var books = author.BookAuthors.Select(p => p.Book).Distinct();

            return new AuthorBooksListModel
            {
                AuthorId = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Email = author.Email,
                Books = _mapper.Map<HashSet<BookModel>>(books)
            };
        }


        public async Task<BookAuthorModel> GetByIdsAsync(Guid bookId, Guid authorId)
        {
            var model = await _db.BookAuthors
                .FirstOrDefaultAsync(p => p.BookId.Equals(bookId) && p.AuthorId.Equals(authorId));

            if (model == null) return null;

            return _mapper.Map<BookAuthorModel>(model);
        }

        public async Task<BookAuthorModel> CreateAsync(BookAuthorAddEditModel model)
        {
           
            if (! await BookAuthorAddEditModelExistsAsync(model.BookId,model.AuthorId))
                throw new Exception($"this {model.BookId} & {model.AuthorId} doesn't exists");

            var bookAuthor = _mapper.Map<BookAuthor>(model);

            _db.BookAuthors.Add(bookAuthor);

            await _db.SaveChangesAsync();

            return _mapper.Map<BookAuthorModel>(bookAuthor);
                
        }

     
        public async Task<BookAuthorModel> DeleteAsync(Guid bookId, Guid authorId)
        {
            var bookAuthor = _db.BookAuthors.FirstOrDefault(p => p.BookId.Equals(bookId) && p.AuthorId.Equals(authorId));

            if (bookAuthor == null) return null;

            _db.BookAuthors.Remove(bookAuthor);
            await _db.SaveChangesAsync();

            return _mapper.Map<BookAuthorModel>(bookAuthor);
        }

        private async Task<bool> BookAuthorAddEditModelExistsAsync(Guid bookId, Guid authorId)
        {
            return await _db.Books.AnyAsync(e => e.Id == bookId) &&
                 await  _db.Authors.AnyAsync(e => e.Id == authorId);

        }
    }
}
