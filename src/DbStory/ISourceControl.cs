namespace DbStory
{
    public interface ISourceControl
    {
        void AddModifiedFilesToVersioning(string commitMessage);
    }
}