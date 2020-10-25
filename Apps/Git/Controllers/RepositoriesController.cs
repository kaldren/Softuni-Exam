using Git.Constants;
using Git.Models;
using Git.Services;
using Git.ViewModels.Repositories;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repoService;

        public RepositoriesController(IRepositoriesService repoService)
        {
            this.repoService = repoService;
        }

        public HttpResponse All()
        {
            var currentUser = this.GetUserId();

            var repos = this.repoService.GetAll(currentUser);
            return this.View(repos);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.LoginPage);
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(RepositoryInputModel model)
        {
            if (model.Name.Length < 3 || model.Name.Length > 10)
            {
                return this.Error(ErrorConst.InvalidRepoName);
            }

            var repository = new Repository
            {
                Name = model.Name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = model.RepositoryType.ToLower() == "public" ? true : false,
                OwnerId = this.GetUserId(),
            };

            bool result = this.repoService.Create(repository);

            if (!result)
            {
                return this.Redirect(nameof(Create));
            }

            return this.Redirect(nameof(All));
        }
    }
}
