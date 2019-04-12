using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using _19GRPADS01BNT401_Assessment.UiWeb.APiFactory;
using _19GRPADS01BNT401_Assessment.UiWeb.Models;
using _19GRPADS01BNT401_Assessment.UiWeb.Utils;

namespace _19GRPADS01BNT401_Assessment.UiWeb.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        public AuthorsController(IOptions<MySettingsModel> appSettings) =>
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await ApiClientFactory.Instance.GetAuthors());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AuthorViewModel = await ApiClientFactory.Instance.GetAuthorById(id.Value);

            if (AuthorViewModel == null)
            {
                return NotFound();
            }

            return View(AuthorViewModel);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Email,BirthDate")] AuthorViewModel AuthorViewModel)
        {
            if (ModelState.IsValid)
            {
                AuthorViewModel.Id = Guid.NewGuid();

                await ApiClientFactory.Instance.CreateAuthor(AuthorViewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(AuthorViewModel);
        }



        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AuthorViewModel = await ApiClientFactory.Instance.GetAuthorById(id.Value);
            if (AuthorViewModel == null)
            {
                return NotFound();
            }

            return View(AuthorViewModel);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,LastName,Email,BirthDate")] AuthorViewModel AuthorViewModel)
        {
            if (id != AuthorViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await ApiClientFactory.Instance.UpdateAuthor(AuthorViewModel);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }



        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AuthorViewModel = await ApiClientFactory.Instance.GetAuthorById(id.Value);

            if (AuthorViewModel == null)
            {
                return NotFound();
            }

            return View(AuthorViewModel);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await ApiClientFactory.Instance.DeleteAuthor(id);
            return RedirectToAction(nameof(Index));
        }

    }

}