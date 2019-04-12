using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using _19GRPADS01BNT401_TP3Web.Models;

namespace _19GRPADS01BNT401_TP3Web.Services
{
    public interface IFriendWebService
    {
        Task<FriendViewModel> GetAsync(Guid id);
        Task<FriendViewModel> PostAsync(FriendViewModel model);
        Task<FriendViewModel> PutAsync(FriendViewModel model);
        Task<FriendViewModel> DeleteAsync(Guid id);
        Task<IEnumerable<FriendViewModel>> GetAllAsync();

        Task<HttpStatusCode> Login();
    }
}