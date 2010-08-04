using System.Collections.Generic;

namespace MarkovMusic.BusinessLayer
{
    public interface IPlayList:IEnumerable<IMusicInPlayList>
    {
        void Add(string id);
        IMusicInPlayList this[string id] { get; }
        void UpdateProbability();
        void SetMusicStore(IMusicLibrary musicLibrary);
        bool Contains(string id);
        IMusicInPlayList PlayingMusic { get; set; }
    }
}