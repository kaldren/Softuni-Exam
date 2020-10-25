using Git.Data;
using Git.Models;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool Create(Commit model)
        {
            this.db.Commits.Add(model);

            return this.db.SaveChanges() > 0;
        }

        public IEnumerable<CommitsViewModel> GetAll(string userId)
        {
            return this.db.Commits.Where(x => x.CreatorId == userId).Select(x => new CommitsViewModel
            {
                Repository = x.Repository.Name,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                Id = x.Id
            }).ToList();
        }

        public bool Delete(string commitId, string userId)
        {
            var commit = this.db.Commits.FirstOrDefault(x => x.Id == commitId && x.CreatorId == userId);

            if (commit == null)
            {
                return false;
            }

            this.db.Remove(commit);
            return this.db.SaveChanges() > 0;
        }
    }
}
