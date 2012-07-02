using System.Collections.Generic;

namespace DbStory
{
    public interface IContentStorage
    {
        void Store(IEnumerable<ProgrammablityContent> contents);
    }
}