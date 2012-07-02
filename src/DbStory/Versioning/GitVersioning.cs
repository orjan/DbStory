using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace DbStory.Versioning
{
    public class GitVersioning : ISourceControl
    {
        private readonly string repositoryPath;

        public GitVersioning(string repositoryPath)
        {
            this.repositoryPath = repositoryPath;
        }

        private static IList<string> FilesToCommit(Repository repository)
        {
            var repositoryStatus = repository.Index.RetrieveStatus();
            return repositoryStatus.Untracked.Concat(repositoryStatus.Modified).Concat(repositoryStatus.Missing).ToList();
        }

        public void AddModifiedFilesToVersioning(string commitMessage)
        {
            using (var repository = new Repository(repositoryPath))
            {
                var filesToAddToCommit = FilesToCommit(repository);

                if (!filesToAddToCommit.Any())
                {
                    return;
                }

                repository.Index.Stage(filesToAddToCommit);
                repository.Commit(commitMessage);
            }
        }
    }
}