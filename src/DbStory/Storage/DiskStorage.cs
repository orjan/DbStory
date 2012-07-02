using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DbStory.Storage
{
    public class DiskStorage : IContentStorage
    {
        private readonly string rootDirectory;

        public DiskStorage(string rootDirectory)
        {
            this.rootDirectory = rootDirectory;
        }

        public void Store(IEnumerable<ProgrammablityContent> contents)
        {
            var directories = contents.Select(x => rootDirectory + x.RelativeDirectoryName).Distinct();
            Parallel.ForEach(directories, RecreateDirectory);
            
            Parallel.ForEach(contents, StoreFile);
        }

        private static void RecreateDirectory(string c)
        {
            if (Directory.Exists(c))
            {
                Directory.Delete(c, true);
            }

            Directory.CreateDirectory(c);
        }

        private void StoreFile(ProgrammablityContent c)
        {
            File.WriteAllText(GetFileName(c), c.Content);
        }

        private string GetFileName(ProgrammablityContent content)
        {
            return string.Format("{0}\\{1}\\{2}", rootDirectory, content.RelativeDirectoryName, content.FileName);
        }
    }
}