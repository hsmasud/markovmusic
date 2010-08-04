using System;
using System.Collections.Generic;
using MarkovMusic.BusinessLayer;

namespace MarkovMusic
{
    public class Music : IMusic
    {
        private readonly Dictionary<string, string> _tags;
        public Music()
            : this(Guid.NewGuid().ToString())
        {
        }
        public Music(string id)
        {
            Id = id;
            Priority = 1;
            _tags = new Dictionary<string, string>();
        }
        public string Id { get; private set; }
        
        public double Priority { get; set; }

        public string this[string key]
        {
            get
            {
                if (_tags.ContainsKey(key))
                    return _tags[key];
                return string.Empty;
            }
            set
            {
                if (_tags.ContainsKey(key))
                    _tags.Remove(key);
                _tags.Add(key,value);
            }
        }
        public bool IsTagMatches(IMusic music, IMusicTag tag)
        {
            return this[tag.Id] != string.Empty && music[tag.Id] != string.Empty && this[tag.Id] == music[tag.Id];
        }

        public bool IsTagMatches(IMusicTag musicTag)
        {
            return this[musicTag.Id] != string.Empty;
        }
    }
}