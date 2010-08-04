using MarkovMusic;
using MarkovMusic.BusinessLayer;
using Moq;

namespace Tests.MarkovMusic.Mocks.BusinessLayer
{
    public class MockMusicStore:Mock<IMusicStore>
    {
        public void VerifyAdd(Music music)
        {
            Verify(s => s.Add(music), Times.Once());
        }
        public void SetupGetMusic(Music music)
        {
            Setup(s => s.GetMusic(music.Id)).Returns(music);
        }
        public IMusicTag SetupGetTag(string id, double priority)
        {
            var tag = new MusicTag
                          {
                              Id = id,
                              Header = id,
                              Priority = priority
                          };
            Setup(s => s.GetTag(id)).Returns(tag);
            return tag;
        }

        public void SetupGetTags(MusicTag[] tags)
        {
            Setup(s => s.GetTags()).Returns(tags);
        }
    }
}