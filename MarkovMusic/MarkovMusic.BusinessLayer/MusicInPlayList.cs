using System;
using MarkovMusic.BusinessLayer;

namespace MarkovMusic
{
    public class MusicInPlayList : IMusicInPlayList
    {
        private readonly IMusic _music;

        public MusicInPlayList(IMusic music)
        {
            _music = music;
        }

        #region Implementation of IMusic

        public string Id
        {
            get { return _music.Id; }
        }

        public double Priority
        {
            get { return _music.Priority;}
            set { _music.Priority = value; }
        }

        public string this[string key]
        {
            get { return _music[key]; }
            set { _music[key] = value; }
        }

        public bool IsTagMatches(IMusic music, IMusicTag musicTag)
        {
            return _music.IsTagMatches(music, musicTag);
        }

        public bool IsTagMatches(IMusicTag musicTag)
        {
            return _music.IsTagMatches(musicTag);
        }

        #endregion

        #region Implementation of IMusicInPlayList

        public double Probability { get; set; }

        public double CumulativeProbability { get; private set; }

        public void UpdateProbability(double totalPriority, double averageTagsPriority, ref double cumulativeProbability, double probabilityMagnifyer)
        {
            if (totalPriority <= 0) return;
            Probability = (Priority + averageTagsPriority) / totalPriority;
            Probability *= probabilityMagnifyer;
            CumulativeProbability = cumulativeProbability += Probability;
        }

        #endregion
    }
}