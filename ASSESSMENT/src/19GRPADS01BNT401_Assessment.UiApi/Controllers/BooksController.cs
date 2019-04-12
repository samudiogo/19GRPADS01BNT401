using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _19GRPADS01BNT401_Assessment.UiApi.Models;
using Microsoft.AspNetCore.Mvc;
using _19GRPADS01BNT401_Assessment.UiApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace _19GRPADS01BNT401_Assessment.UiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IBookAuthorService _bookAuthorService;

        public BooksController(IBookService service, IBookAuthorService bookAuthorService)
        {
            _service = service;
            _bookAuthorService = bookAuthorService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IEnumerable<BookModel>> GetBooks()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _service.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromRoute] Guid id, [FromBody] BookCreateEditModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }


            try
            {
                var model = await _service.UpdateAsync(id, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] BookCreateEditModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateAsync(book);
            return CreatedAtAction("GetBook", new { id = book.Id }, result);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.DeleteAsync(id);

            return Ok(result);

        }

        #region BookAuthors
        // GET: api/Books/5
        [HttpGet("{id}/authors")]
        public IActionResult GetBookAuthors([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = _bookAuthorService.GetAuthorListByBook(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // POST: api/Books
        [HttpPost("{id}/authors")]
        public async Task<IActionResult> PostBookAuthors([FromBody] BookCreateEditModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateAsync(book);
            return CreatedAtAction("GetBook", new { id = book.Id }, result);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}/authors")]
        public async Task<IActionResult> DeleteBookAuthors([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.DeleteAsync(id);

            return Ok(result);

        }


        #endregion
    }
}