using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _19GRPADS01BNT401_TP3Web.Models;
using _19GRPADS01BNT401_TP3Web.Services;

namespace _19GRPADS01BNT401_TP3Web.Controllers
{
    public class FriendsController : Controller
    {


        private readonly IFriendWebService _service;

        public FriendsController( IFriendWebService service)
        {
            _service = service;
            _service.Login();
        }

        // GET: Friends
        public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _service.GetAsync(id.Value);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Email,BirthDate")] FriendViewModel friend)
        {
            if (ModelState.IsValid)
            {
                await _service.PostAsync(friend);
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _service.GetAsync(id.Value);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,LastName,Email,BirthDate")] FriendViewModel friend)
        {
            if (id != friend.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(friend);


            var model = await _service.PutAsync(friend);

            if (model == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _service.GetAsync(id.Value);

            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
