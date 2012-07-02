using System.Collections.Generic;

namespace DbStory
{
    public interface IContentProvider
    {
        IEnumerable<ProgrammablityContent> GetFileContents();
    }
}