using System.Collections.Generic;

namespace MarkovMusic.BusinessLayer
{
    public interface IMusicLibrary
    {
        void Add(IMusic music);
        IMusic GetMusic(string id);
        IPlayList AllMusicPlayList { get; }
        IEnumerable<IMusicTag> GetTags();
        int GetMusicCount();
        IMusicTag GetTag(string id);
        void Add(IMusicTag musicTag);
        double GetRepeatPriority();
    }
}