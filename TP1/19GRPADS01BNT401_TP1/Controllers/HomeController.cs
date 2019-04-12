using System;
using System.Collections.Generic;
using System.Linq;
using _19GRPADS01BNT401_TP1.Models;
using Microsoft.AspNetCore.Mvc;
using TP1.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _19GRPADS01BNT401_TP1.Controllers
{
    public class HomeController : Controller
    {
        private readonly TP1DbContext _db;

        public HomeController(TP1DbContext db)
        {
            _db = db;

            if (_listA.Count == 0)
            {
                _listA = FromBdFriendList().ToDictionary(k => k.Id, v => v);
            }
            if (_listB.Count == 0)
                _listB = FromBdFriendList().ToDictionary(k => k.Id, v => v);
        }

        private static IDictionary<Guid, FriendViewModel> _listA = new Dictionary<Guid, FriendViewModel>();
        private static IDictionary<Guid, FriendViewModel> _listB = new Dictionary<Guid, FriendViewModel>();

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View(_listA.Values);
        }

        public IActionResult ListB()
        {
          return View(_listB.Values);
        }
        [HttpPost]
        public IActionResult SelectListA(Guid id)
        {
            var friend = _listA[id];
            friend.Selected = !friend.Selected;
            _listA[id] = friend;
            return RedirectToAction("Index");

        }


        [HttpPost]
        public IActionResult SelectListB(Guid id)
        {
            var friend = _listB[id];
            friend.Selected = !friend.Selected;
            _listB[id] = friend;
            return RedirectToAction("ListB");

        }

        private ICollection<FriendViewModel> FromBdFriendList()
        {
            return  _db.Friends
                .Select(friend => new FriendViewModel
                {
                    Id = friend.Id,
                    Name = friend.Name,
                    LastName = friend.LastName,
                    BirthDate = friend.BirthDate,
                    Email = friend.Email

                })
                .ToList();
        }
    }
}
