using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using _19GRPADS01BNT401_Assessment.UiWeb.APiFactory;
using _19GRPADS01BNT401_Assessment.UiWeb.Models;
using _19GRPADS01BNT401_Assessment.UiWeb.Utils;

namespace _19GRPADS01BNT401_Assessment.UiWeb.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {

        public BooksController(IOptions<MySettingsModel> appSettings) =>
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await ApiClientFactory.Instance.GetBooks());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookViewModel = await ApiClientFactory.Instance.GetBookWithAuthorsByBookId(id.Value);

            if (bookViewModel == null)
            {
                return NotFound();
            }

            return View(bookViewModel);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var authors = (await ApiClientFactory.Instance.GetAuthors()).Select(author => new { AuthorId = author.Id, AuthorFullName = $"{author.Name} {author.LastName}" });
            ViewBag.Authors = new MultiSelectList(authors, "AuthorId", "AuthorFullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Isbn,Year, authorId")] BookViewModel bookViewModel, IEnumerable<Guid> authorId)
        {
            if(!authorId.Any())
            {
                ModelState.AddModelError("authorId", "you should choice at least one author.");
            }

            if (ModelState.IsValid)
            {
                bookViewModel.Id = Guid.NewGuid();
                bookViewModel.AuthorsId = authorId;
                var bookResponse = await ApiClientFactory.Instance.CreateBook(bookViewModel);
                if (!bookResponse.IsSuccess)
                {
                    ModelState.AddModelError(nameof(bookViewModel.Id), bookResponse.ReturnMessage);
                    return View(bookViewModel);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(bookViewModel);
        }



        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookViewModel = await ApiClientFactory.Instance.GetBookWithAuthorsByBookId(id.Value);
            if (bookViewModel == null)
            {
                return NotFound();
            }

            var authors = (await ApiClientFactory.Instance.GetAuthors()).Select(author => new { AuthorId = author.Id, AuthorFullName = $"{author.Name} {author.LastName}" }).ToList();

            ViewBag.Authors = new MultiSelectList(authors, "AuthorId", "AuthorFullName", bookViewModel.Authors.Select(ba => ba.Id).ToList());

            var model = new BookViewModel
            {
                Id = bookViewModel.BookId,
                Title = bookViewModel.Title,
                Isbn = bookViewModel.Isbn,
                Year = bookViewModel.Year
            };
            return View(model);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Isbn,Year, AuthorId")] BookViewModel bookViewModel, IEnumerable<Guid> authorId)
        {
            if (id != bookViewModel.Id)
            {
                return NotFound();
            }

            if (!authorId.Any())
            {
                ModelState.AddModelError("authorId", "you should choice at least one author.");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    bookViewModel.AuthorsId = authorId;
                    await ApiClientFactory.Instance.UpdateBook(bookViewModel);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            var authors = (await ApiClientFactory.Instance.GetAuthors()).Select(author => new { AuthorId = author.Id, AuthorFullName = $"{author.Name} {author.LastName}" });
            ViewBag.Authors = new MultiSelectList(authors, "AuthorId", "AuthorFullName", bookViewModel.AuthorsId.Select(ba => ba));

            return RedirectToAction(nameof(Index));
        }



        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookViewModel = await ApiClientFactory.Instance.GetBookById(id.Value);

            if (bookViewModel == null)
            {
                return NotFound();
            }

            return View(bookViewModel);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await ApiClientFactory.Instance.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

    }

}