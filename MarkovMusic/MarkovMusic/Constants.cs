using System;

namespace MarkovMusic
{
    public class Constants
    {
        public const string DatabaseName="markovmusic.db";

        public class Sql
        {
            public const string GetMusicCount="select count(*) from music";
            public const string GetMusic="select * from music where id='{0}'";
            public const string CreateMusicTable = "create table music(id varchar(20), priority int);";
            public const string InsertMusic="insert into music values('{0}',{1})";
        }
        public class ColumnIndex
        {
            public class Music
            {
               public const int Id = 0;
               public const int Priority = 1;           
            }
        }

        public class Tags
        {
            public const string Genre="Genre";
            public const string Title = "Title";
            public const string FileName = "FileName";
            public const string Length = "Length";
            public const string Artist = "Artist";
            public const string Album = "Album";
        }
    }
}