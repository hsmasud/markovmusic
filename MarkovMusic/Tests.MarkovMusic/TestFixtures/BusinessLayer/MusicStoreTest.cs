using System.Collections;
using MarkovMusic;
using MarkovMusic.DataAccessLayer;
using Moq;
using NUnit.Framework;
using Tests.MarkovMusic.Mocks;
using Tests.MarkovMusic.Mocks.DataAccessLayer;

namespace Tests.MarkovMusic.TestFixtures.BusinessLayer
{
    [TestFixture]
    public class MusicStoreTest
    {
        private Mock<IDatabaseAdapter> _adapter;
        private MusicStore _store;

        [SetUp]
        public void Setup()
        {
            _adapter = new Mock<IDatabaseAdapter>();
            _store = new MusicStore(_adapter.Object);

        }
        [Test]
        public void AddMusic()
        {
            var music = new Music("A"){Priority = 2};
            _store.Add(music);
            _adapter.Verify(o => o.OpenDatabase(), Times.Once());
            _adapter.Verify(o=>o.ExecuteNonQuery(string.Format(Constants.Sql.InsertMusic,music.Id,music.Priority)),Times.Once());
            _adapter.Verify(o=>o.Close(),Times.Once());
        }
        [Test]
        public void GetMusic()
        {
            var databaseResultSet = new MockDatabaseResultSet
                                        {
                                            Rows = new ArrayList
                                                       {
                                                           new ArrayList {"A",2d}
                                                       }
                                        };
            _adapter.Setup(s => s.Execute(string.Format(Constants.Sql.GetMusic, "A"))).Returns(databaseResultSet);
            var music=_store.GetMusic("A");
            _adapter.VerifyAll();
            _adapter.Verify(o => o.OpenDatabase(), Times.Once());
            _adapter.Verify(o => o.Close(), Times.Once());
            Assert.AreEqual("A",music.Id);
            Assert.AreEqual(2, music.Priority);
        }

        [Test]
        public void GetMusicCount()
        {
            var databaseResultSet = new MockDatabaseResultSet
                                        {
                                            Rows  = new ArrayList
                                                        {
                                                            new ArrayList{2}
                                                        }
                                        };
            _adapter.Setup(s => s.Execute(Constants.Sql.GetMusicCount)).Returns(databaseResultSet);
            Assert.AreEqual(2,_store.GetMusicCount());
            _adapter.VerifyAll();
            _adapter.Verify(o => o.OpenDatabase(), Times.Once());
            _adapter.Verify(o => o.Close(), Times.Once());
        }
    }
}
