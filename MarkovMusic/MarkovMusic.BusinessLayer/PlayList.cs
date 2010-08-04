using System;
using System.Collections.ObjectModel;
using System.Linq;
using MarkovMusic.BusinessLayer;

namespace MarkovMusic
{
    public class PlayList: ObservableCollection<IMusicInPlayList>, IPlayList
    {
        private IMusicLibrary _musicLibrary;
        public PlayList()
        {
        }
        public PlayList(IMusicLibrary musicLibrary)
            :this()
        {
            SetMusicStore(musicLibrary);
        }

        public void SetMusicStore(IMusicLibrary musicLibrary)
        {
            _musicLibrary = musicLibrary;
        }

        public bool Contains(string id)
        {
            return this.Any(c => c.Id == id);
        }


        public void Add(string id)
        {
            Add(new MusicInPlayList(_musicLibrary.GetMusic(id)));
        }
        public IMusicInPlayList this[string id]
        {
            get
            {
                return this.Single(m => m.Id == id);
            }
        }
        public void UpdateProbability()
        {
            var totalPriority = this.Sum(p => GetTotalPriority(p));
            double cumulativeProbability = 0;
            foreach (var music in this)
            {
                var probabilityMagnifyer = 1d;
                if (IsPlaying(music))
                    probabilityMagnifyer = _musicLibrary.GetRepeatPriority();
                music.UpdateProbability(totalPriority, GetAverageTagsPriority(music), ref cumulativeProbability,probabilityMagnifyer);
            }
        }

        private double GetTotalPriority(IMusic music)
        {
            var totalPriority = music.Priority;
            if (PlayingMusic != null)
            {
                totalPriority += GetAverageTagsPriority(music);
                if (IsPlaying(music))
                    totalPriority *= _musicLibrary.GetRepeatPriority();
            }
            return totalPriority;
        }

        private bool IsPlaying(IMusic music)
        {
            return PlayingMusic != null && PlayingMusic.Equals(music);
        }

        public IMusicInPlayList PlayingMusic { get; set; }

        private double GetAverageTagsPriority(IMusic music)
        {
            var totalPriority = 0d;
            foreach (var tag in _musicLibrary.GetTags())
            {
                if(PlayingMusic.IsTagMatches(music,tag))
                {
                    var matchedMusic = this.Where(m => music.IsTagMatches(m, tag));
                    if (matchedMusic.Count() > 0)
                        totalPriority += matchedMusic.Average(m => m.Priority) * tag.Priority;      
                }
            }
            return totalPriority;
        }

        public void SetPlaying(string id)
        {
            PlayingMusic = GetMusic(id);
        }

        private IMusicInPlayList GetMusic(string id)
        {
            return this.SingleOrDefault(m => m.Id == id);
        }
    }
}