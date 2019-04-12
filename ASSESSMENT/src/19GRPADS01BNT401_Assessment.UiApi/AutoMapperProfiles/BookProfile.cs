using System.Linq;
using AutoMapper;
using _19GRPADS01BNT401_Assessment.Domain.Entities;
using _19GRPADS01BNT401_Assessment.UiApi.Models;

namespace _19GRPADS01BNT401_Assessment.UiApi.AutoMapperProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookModel, Book>().ForMember(p => p.BookAuthors, opt => opt.Ignore());
            CreateMap<BookCreateEditModel, Book>()
                .ForMember(dst => dst.BookAuthors, opt => opt.MapFrom(src => src.AuthorsId.Select(item => new BookAuthor
                {
                    AuthorId = item,
                    BookId = src.Id
                })));

            CreateMap<Book, BookCreateEditModel>().ForMember(dst => dst.AuthorsId, opt => opt.MapFrom(src => src.BookAuthors.Select(item => item.AuthorId)));

        }
    }
}