using DbStory.Provider;
using DbStory.Storage;
using DbStory.Versioning;

namespace DbStoryRunner
{
    public class ReadDataFromProcedures
    {
        public static void Main(string[] args)
        {
            const string connectionString = @"Server=(Localdb)\dbstory;Integrated Security=true;Initial Catalog=DbStory";
            const string repositoryPath = @"C:\spoc\";

            var runner = new DbStory.DbStoryRunner(new SqlServerContentProvider(connectionString),
                                                    new DiskStorage(repositoryPath), 
                                                    new GitVersioning(repositoryPath));
            
            runner.Execute();
        }
    }
}