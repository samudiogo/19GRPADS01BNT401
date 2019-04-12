using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using _19GRPADS01BNT401_Assessment.UiWeb.Models;

namespace _19GRPADS01BNT401_Assessment.UiWeb.APiFactory
{
    public partial class ApiClient
    {

        public async Task<BookViewModel> GetBookById(Guid id) => await GetAsync<BookViewModel>(CreateRequestUri($"books/{id:N}"));

        public async Task<BookAuthorsListModel> GetBookWithAuthorsByBookId(Guid id)
        {
            return await GetAsync<BookAuthorsListModel>(CreateRequestUri($"books/{id:N}/authors"));
        }

        public async Task<IEnumerable<BookViewModel>> GetBooks()
        {
            var req = CreateRequestUri("books");
            var books = await GetAsync<IEnumerable<BookViewModel>>(req);
            return books;

        }
        public async Task<Message<BookViewModel>> CreateBook(BookViewModel model)
        {
            model.Id = Guid.NewGuid();
            var requestUrl = CreateRequestUri("books");
            return await PostAsync(requestUrl, model);
        }

        public async Task<Message<BookViewModel>> UpdateBook(BookViewModel model)
        {
            var requestUrl = CreateRequestUri($"books/{model.Id}");

            return await PutAsync(requestUrl, model);
        }

        public async Task DeleteBook(Guid id)
        {
            var requestUrl = CreateRequestUri($"books/{id}");

            await DeleteAsync(requestUrl);

        }

    }
}