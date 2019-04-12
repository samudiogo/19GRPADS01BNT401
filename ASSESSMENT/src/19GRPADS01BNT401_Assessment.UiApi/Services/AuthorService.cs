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
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorModel>> GetAllAsync();
        Task<AuthorModel> GetByIdAsync(Guid id);

        Task<AuthorModel> CreateAsync(AuthorModel model);
        Task<AuthorModel> UpdateAsync(Guid id, AuthorModel model);
        Task<AuthorModel> DeleteAsync(Guid id);

    }
    public class AuthorService: IAuthorService
    {
        private readonly AssessmentDbContext _db;
        private readonly IMapper _mapper;

        public AuthorService(AssessmentDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorModel>> GetAllAsync()
        {
           return  _mapper.Map<List<AuthorModel>>((await _db.Authors.ToListAsync()));
        }

        public async Task<AuthorModel> GetByIdAsync(Guid id)
        {
            var model = await _db.Authors.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (model == null) return null;

            return _mapper.Map<AuthorModel>(model);
        }

        public async Task<AuthorModel> CreateAsync(AuthorModel model)
        {
            model.Id = Guid.NewGuid();
            var friend = _mapper.Map<Author>(model);

            if (friend == null)
                throw new Exception("Bad Request - Error at mapping DTO");
            _db.Authors.Add(friend);
            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<AuthorModel> UpdateAsync(Guid id, AuthorModel model)
        {
            try
            {
                var friend = new Author { Id = id };

                _db.Authors.Attach(friend);

                _db.Entry(friend).CurrentValues.SetValues(model);

                await _db.SaveChangesAsync();

                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(model.Id))
                {
                    return null;
                }

                throw;
            }
        }

        public async Task<AuthorModel> DeleteAsync(Guid id)
        {
            var friend = _db.Authors.FirstOrDefault(f => f.Id.Equals(id));

            if (friend == null) return null;

            _db.Authors.Remove(friend);
            await _db.SaveChangesAsync();
            return _mapper.Map<AuthorModel>(friend);
        }

        private bool AuthorExists(Guid id)
        {
            return _db.Authors.Any(e => e.Id == id);
        }
    }
}