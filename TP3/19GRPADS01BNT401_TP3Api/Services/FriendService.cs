using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _19GRPADS01BNT401_TP3.Infra.Data;
using _19GRPADS01BNT401_TP3Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using _19GRPADS01BNT401_TP3.Domain.Entities;

namespace _19GRPADS01BNT401_TP3Api.Services
{
    
    public interface IFriendService
    {
        Task<IEnumerable<FriendModel>> GetAllAsync();
        Task<FriendModel> GetByIdAsync(Guid id);

        Task<FriendModel> CreateAsync(FriendModel model);
        Task<FriendModel> UpdateAsync(Guid id, FriendModel model);
        Task<FriendModel> DeleteAsync(Guid id);

    }

    public class FriendService : IFriendService
    {
        private readonly TP3DbContext _db;
        private readonly IMapper _mapper;

        public FriendService(TP3DbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<FriendModel>> GetAllAsync() => _mapper.Map<List<FriendModel>>((await _db.Friends.ToListAsync()));

        public async Task<FriendModel> GetByIdAsync(Guid id)
        {
            var model = await _db.Friends.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (model == null) return null;

            return _mapper.Map<FriendModel>(model);
        }

        public async Task<FriendModel> CreateAsync(FriendModel model)
        {
            model.Id = Guid.NewGuid();
            var friend = _mapper.Map<Friend>(model);

            if (friend == null)
                throw new Exception("Bad Request - Error at mapping DTO");
            _db.Friends.Add(friend);
            await _db.SaveChangesAsync();

            return model;

        }

        public async Task<FriendModel> UpdateAsync(Guid id, FriendModel model)
        {
            try
            {
                var friend = new Friend { Id = id };

                _db.Friends.Attach(friend);

                _db.Entry(friend).CurrentValues.SetValues(model);

                await _db.SaveChangesAsync();

                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(model.Id))
                {
                    return null;
                }

                throw;
            }


           
        }

        public async Task<FriendModel> DeleteAsync(Guid id)
        {
            var friend = _db.Friends.FirstOrDefault(f => f.Id.Equals(id));

            if (friend == null) return null;

            _db.Friends.Remove(friend);
            await _db.SaveChangesAsync();
            return _mapper.Map<FriendModel>(friend);

        }
        private bool FriendExists(Guid id)
        {
            return _db.Friends.Any(e => e.Id == id);
        }
    }
}
