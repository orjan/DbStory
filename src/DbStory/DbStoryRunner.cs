namespace DbStory
{
    public class DbStoryRunner
    {
        private readonly IContentProvider contentProvider;
        private readonly IContentStorage contentStorage;
        private readonly ISourceControl versioning;

        public DbStoryRunner(IContentProvider contentProvider, IContentStorage contentStorage, ISourceControl versioning)
        {
            this.contentProvider = contentProvider;
            this.contentStorage = contentStorage;
            this.versioning = versioning;
        }

        public void Execute()
        {
            var fileContents = contentProvider.GetFileContents();
            contentStorage.Store(fileContents);

            if (versioning != null)
            {
                versioning.AddModifiedFilesToVersioning("Changes from the production server");
            }
        }
    }
}