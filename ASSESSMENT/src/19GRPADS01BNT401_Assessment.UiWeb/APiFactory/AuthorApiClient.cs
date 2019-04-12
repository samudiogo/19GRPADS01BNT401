using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _19GRPADS01BNT401_Assessment.UiWeb.Models;

namespace _19GRPADS01BNT401_Assessment.UiWeb.APiFactory
{
    public partial class ApiClient
    {

        public async Task<AuthorViewModel> GetAuthorById(Guid id) =>
            await GetAsync<AuthorViewModel>(CreateRequestUri($"Authors/{id:N}"));
        public async Task<IEnumerable<AuthorViewModel>> GetAuthors()
        {
            var req = CreateRequestUri("Authors");
            var Authors = await GetAsync<IEnumerable<AuthorViewModel>>(req);
            return Authors;

        }
        public async Task<Message<AuthorViewModel>> CreateAuthor(AuthorViewModel model)
        {
            model.Id = Guid.NewGuid();
            var requestUrl = CreateRequestUri("Authors");
            return await PostAsync(requestUrl, model);
        }

        public async Task<Message<AuthorViewModel>> UpdateAuthor(AuthorViewModel model)
        {
            var requestUrl = CreateRequestUri($"Authors/{model.Id}");

            return await PutAsync(requestUrl, model);
        }

        public async Task DeleteAuthor(Guid id)
        {
            var requestUrl = CreateRequestUri($"Authors/{id}");

            await DeleteAsync(requestUrl);

        }

    }
}