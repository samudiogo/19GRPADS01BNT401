using AutoMapper;
using _19GRPADS01BNT401_Assessment.Domain.Entities;
using _19GRPADS01BNT401_Assessment.UiApi.Models;

namespace _19GRPADS01BNT401_Assessment.UiApi.AutoMapperProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorModel, Author>().ForMember(p => p.BookAuthors, opt => opt.Ignore());
        }
    }
}