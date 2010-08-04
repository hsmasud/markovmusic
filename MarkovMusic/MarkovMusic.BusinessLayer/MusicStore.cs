using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkovMusic.BusinessLayer;
using MarkovMusic.DataAccessLayer;

namespace MarkovMusic
{
    public class MusicStore:IMusicStore
    {
        private readonly IDatabaseAdapter _adapter;

        public MusicStore(IDatabaseAdapter adapter)
        {
            _adapter = adapter;
        }

        #region Implementation of IMusicStore

        public void Add(IMusic music)
        {
            _adapter.OpenDatabase();
            _adapter.ExecuteNonQuery(string.Format(Constants.Sql.InsertMusic, music.Id, music.Priority));
            _adapter.Close();
        }

        public IMusic GetMusic(string id)
        {
            _adapter.OpenDatabase();
            var result = _adapter.Execute(string.Format(Constants.Sql.GetMusic, id));
            IMusic music=null;
            if(result.Rows.Count==1)
            {
                var row = (ArrayList) result.Rows[0];
                music = new Music((string)row[Constants.ColumnIndex.Music.Id])
                            {
                                Priority = (double) row[Constants.ColumnIndex.Music.Priority]
                            };
            }
            _adapter.Close();
            return music;
        }

        public int GetMusicCount()
        {
            _adapter.OpenDatabase();
            var result = _adapter.Execute(Constants.Sql.GetMusicCount);
            var musicCount = 0;
            if (result.Rows.Count == 1)
            {
                var row = (ArrayList)result.Rows[0];
                musicCount =  (int) row[0];
            }
            _adapter.Close();
            return musicCount;

        }

        public IMusicTag GetTag(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(IMusicTag musicTag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMusicTag> GetTags()
        {
            throw new NotImplementedException();
        }

        public double GetRepeatPriority()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}