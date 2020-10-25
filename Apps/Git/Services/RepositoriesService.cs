using Git.Data;
using Git.Models;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool Create(Repository model)
        {
            this.db.Repositories.Add(model);

            return this.db.SaveChanges() > 0;
        }

        public IEnumerable<RepositoriesViewModel> GetAll(string userId)
        {
            var repos = this.db.Repositories
                .Where(x => x.IsPublic == true || x.IsPublic != true && x.OwnerId == userId)
                .Select(x => new RepositoriesViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Owner.Username,
                CreatedOn = x.CreatedOn,
                CommitsCount = x.Commits.Count()
            }).ToList();

            return repos;
        }

        public Repository GetById(string id)
        {
            return this.db.Repositories.SingleOrDefault(x => x.Id == id);
        }
    }
}
