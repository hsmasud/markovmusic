using System;
using MarkovMusic.BusinessLayer;

namespace MarkovMusic
{
    public class MusicTag : IMusicTag
    {
        public string Header{ get; set;}
        public string Id { get; set; }
        public double Priority { get; set; }
    }
}