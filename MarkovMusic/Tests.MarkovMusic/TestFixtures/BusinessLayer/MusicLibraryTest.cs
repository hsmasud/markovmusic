using Moq;
using NUnit.Framework;
using MarkovMusic;
using Tests.MarkovMusic.Mocks.BusinessLayer;

namespace Tests.MarkovMusic.TestFixtures.BusinessLayer
{
    [TestFixture]
    public class MusicLibraryTest
    {
        private MusicLibrary _musicLibrary;
        private MockMusicStore _store;

        [Test]
        public void AddMusic()
        {
            var music = new Music("A");
            _musicLibrary.Add(music);
            _store.VerifyAdd(music);
        }

        [Test]
        public void MusicStoreHasAllMusicPlayList()
        {
            Assert.IsNotNull(_musicLibrary.AllMusicPlayList);
        }
        [SetUp]
        public void SetUp()
        {
            _store = new MockMusicStore();
            _musicLibrary = new MusicLibrary(_store.Object);
        }

        [Test]
        public void AddingMusicAddsInAllMusicPlayList()
        {
            var music = new Music("A");
            _store.SetupGetMusic(music);
            _musicLibrary.Add(music);
            Assert.IsTrue(_musicLibrary.AllMusicPlayList.Contains("A"));
        }

        [Test]
        public void AddTag()
        {
            var tag = new MusicTag
                          {
                              Id = Constants.Tags.Genre,
                              Header = Constants.Tags.Genre,
                              Priority = 5
                          };
            _musicLibrary.Add(tag);
            _store.Verify(s=>s.Add(tag),Times.Once());
        }
        [Test]
        public void GetTag()
        {
            var tag = _store.SetupGetTag(Constants.Tags.Genre, 5);
            Assert.AreEqual(tag,_musicLibrary.GetTag(Constants.Tags.Genre));
            _store.VerifyAll();
        }
        [Test]
        public void RepeatPriority()
        {
            _store.Setup(s => s.GetRepeatPriority()).Returns(1);
            Assert.AreEqual(1,_musicLibrary.GetRepeatPriority());
        }
        
    }
}