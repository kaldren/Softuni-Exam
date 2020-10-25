using Git.Models;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        IEnumerable<RepositoriesViewModel> GetAll(string userId);

        bool Create(Repository model);

        Repository GetById(string id);
    }
}
