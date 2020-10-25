using Git.Constants;
using Git.Models;
using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;
        private readonly IRepositoriesService repoService;

        public CommitsController(ICommitsService commitsService, IRepositoriesService repoService)
        {
            this.commitsService = commitsService;
            this.repoService = repoService;
        }

        [HttpGet]
        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.LoginPage);
            }

            Repository repo = this.repoService.GetById(id);

            var viewModel = new CommitViewModel
            {
                Id = repo.Id,
                Name = repo.Name
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CommitInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.LoginPage);
            }

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length < 5)
            {
                return this.Error(ErrorConst.InvalidDescriptionLength);
            }

            var commit = new Commit
            {
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = this.GetUserId(),
                RepositoryId = model.Id,
            };

            bool success = this.commitsService.Create(commit);

            if (!success)
            {
                return this.Error(ErrorConst.UnableToCreateCommit);
            }

            return this.Redirect(RedirectConst.AllRepositories);
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.LoginPage);
            }

            string userId = this.GetUserId();

            var commits = this.commitsService.GetAll(userId);

            return this.View(commits);
        }

        [HttpGet]
        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.LoginPage);
            }

            var userId = this.GetUserId();

            bool result = this.commitsService.Delete(id, userId);

            if (!result)
            {
                return this.Error(ErrorConst.UnableToDeleteCommit);
            }

            return this.Redirect(nameof(All));
        }
    }
}
