using System.Collections.Generic;

namespace MarkovMusic.BusinessLayer
{
    public interface IMusicStore
    {
        void Add(IMusic music);
        IMusic GetMusic(string id);
        int GetMusicCount();
        IMusicTag GetTag(string id);
        void Add(IMusicTag musicTag);
        IEnumerable<IMusicTag> GetTags();
        double GetRepeatPriority();
    }
}