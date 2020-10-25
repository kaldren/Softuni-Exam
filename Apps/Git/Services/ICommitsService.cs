using Git.Models;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        bool Create(Commit model);
        IEnumerable<CommitsViewModel> GetAll(string userId);
        bool Delete(string commitId, string userId);
    }
}
