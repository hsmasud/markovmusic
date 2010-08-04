using System;
using System.Collections.Generic;
using MarkovMusic.BusinessLayer;

namespace MarkovMusic
{
    public class MusicLibrary : IMusicLibrary
    {
        private readonly IMusicStore _store;

        public MusicLibrary(IMusicStore store)
        {
            _store = store;
            AllMusicPlayList=new PlayList(this);
        }

        public IPlayList AllMusicPlayList { get; private set; }
        public IEnumerable<IMusicTag> GetTags()
        {
            return _store.GetTags();
        }

        public void Add(IMusic music)
        {
            _store.Add(music);
            AllMusicPlayList.Add(music.Id);
        }
        public IMusic GetMusic(string id)
        {
            return _store.GetMusic(id);
        }

        public int GetMusicCount()
        {
            return _store.GetMusicCount();
        }

        public IMusicTag GetTag(string id)
        {
            return _store.GetTag(id);
        }

        public void Add(IMusicTag musicTag)
        {
            _store.Add(musicTag);
        }

        public double GetRepeatPriority()
        {
            return _store.GetRepeatPriority();
        }
    }
}