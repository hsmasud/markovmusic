using System;
using NUnit.Framework;
using MarkovMusic;
using Tests.MarkovMusic.Mocks;
using Tests.MarkovMusic.Mocks.BusinessLayer;

namespace Tests.MarkovMusic.TestFixtures.BusinessLayer
{
    [TestFixture]
    public class PlayListTest
    {
        public const double Zero = 0.00001;
        private MusicLibrary _musicLibrary;
        private MockMusicStore _store;

        [Test]
        public void NoSelection4MusicHasPriority1()
        {
            var musicA = new Music("A") { Priority = 1 };
            var musicB = new Music("B") { Priority = 1 };
            var musicC = new Music("C") { Priority = 1 };
            var musicD = new Music("D") { Priority = 1 };

            _store.SetupGetMusic(musicA);
            _store.SetupGetMusic(musicB);
            _store.SetupGetMusic(musicC);
            _store.SetupGetMusic(musicD);

            _musicLibrary.Add(musicA);
            _musicLibrary.Add(musicB);
            _musicLibrary.Add(musicC);
            _musicLibrary.Add(musicD);
            var playList = new PlayList(_musicLibrary) {"A","B","C","D"};
            playList.UpdateProbability();
            const double unitProbability = 1d / 4d;
            Assert.AreEqual(unitProbability, playList["A"].Probability);
            Assert.AreEqual(unitProbability, playList["B"].Probability);
            Assert.AreEqual(unitProbability, playList["C"].Probability);
            Assert.AreEqual(unitProbability, playList["D"].Probability);
            Assert.AreEqual(unitProbability * 1, playList["A"].CumulativeProbability);
            Assert.AreEqual(unitProbability * 2, playList["B"].CumulativeProbability);
            Assert.AreEqual(unitProbability * 3, playList["C"].CumulativeProbability);
            Assert.AreEqual(unitProbability * 4, playList["D"].CumulativeProbability);
        }
        [Test]
        public void NoSelection4MusicHasPriority1OneHas2()
        {
            var musicA = new Music("A") { Priority = 1 };
            var musicB = new Music("B") { Priority = 1 };
            var musicC = new Music("C") { Priority = 1 };
            var musicD = new Music("D") { Priority = 1 };
            var musicE = new Music("E") { Priority = 2 };

            _store.SetupGetMusic(musicA);
            _store.SetupGetMusic(musicB);
            _store.SetupGetMusic(musicC);
            _store.SetupGetMusic(musicD);
            _store.SetupGetMusic(musicE);

            _musicLibrary.Add(musicA);
            _musicLibrary.Add(musicB);
            _musicLibrary.Add(musicC);
            _musicLibrary.Add(musicD);
            _musicLibrary.Add(musicE);

            var playList = new PlayList(_musicLibrary) {"A","B","C","D","E"};
            playList.UpdateProbability();
            const double unitProbability = 1d / 6d;
            Assert.AreEqual(unitProbability, playList["A"].Probability);
            Assert.AreEqual(unitProbability, playList["B"].Probability);
            Assert.AreEqual(unitProbability, playList["C"].Probability);
            Assert.AreEqual(unitProbability, playList["D"].Probability);
            Assert.AreEqual(2*unitProbability, playList["E"].Probability);
        }
        [SetUp]
        public void SetUp()
        {
            _store = new MockMusicStore();
            _musicLibrary = new MusicLibrary(_store.Object);
        }
        [Test]
        public void NoSelection4Music2Classic()
        {
            var musicA = new Music("A") { Priority = 1 };
            musicA[Constants.Tags.Genre] = "Classic";
            var musicB = new Music("B") { Priority = 2 };
            var musicC = new Music("C") { Priority = 3 };
            musicC[Constants.Tags.Genre] = "Classic";
            var musicD = new Music("D") { Priority = 4 };

            _store.SetupGetMusic(musicA);
            _store.SetupGetMusic(musicB);
            _store.SetupGetMusic(musicC);
            _store.SetupGetMusic(musicD);

            _musicLibrary.Add(musicA);
            _musicLibrary.Add(musicB);
            _musicLibrary.Add(musicC);
            _musicLibrary.Add(musicD);
            var playList = new PlayList(_musicLibrary) { "A", "B", "C", "D" };
            playList.UpdateProbability();
            const double totalPriority = 10d;
            const double unitProbability = 1d / totalPriority;
            _store.VerifyAll();
            AreEqual(unitProbability * musicA.Priority, playList["A"].Probability);
            AreEqual(unitProbability * musicB.Priority, playList["B"].Probability);
            AreEqual(unitProbability * musicC.Priority, playList["C"].Probability);
            AreEqual(unitProbability * musicD.Priority, playList["D"].Probability);

            AreEqual(unitProbability * 1, playList["A"].CumulativeProbability);
            AreEqual(unitProbability * 3, playList["B"].CumulativeProbability);
            AreEqual(unitProbability * 6, playList["C"].CumulativeProbability);
            AreEqual(unitProbability * 10, playList["D"].CumulativeProbability);
        }
        [Test]
        public void TwoClassic2RockClassicIsPlaying()
        {
            _store.SetupGetTags(new[]
                                    {
                                        new MusicTag {Id = Constants.Tags.Genre, Priority = 2},
                                        new MusicTag {Id = Constants.Tags.Album, Priority = 3}
                                    });
            _store.Setup(s => s.GetRepeatPriority()).Returns(1);
            var musicA = new Music("A") { Priority = 1 };
            musicA[Constants.Tags.Genre] = "Classic";
            var musicB = new Music("B") { Priority = 2 };
            musicB[Constants.Tags.Genre] = "Rock";
            var musicC = new Music("C") { Priority = 3 };
            musicC[Constants.Tags.Genre] = "Classic";
            var musicD = new Music("D") { Priority = 4 };
            musicD[Constants.Tags.Genre] = "Rock";

            _store.SetupGetMusic(musicA);
            _store.SetupGetMusic(musicB);
            _store.SetupGetMusic(musicC);
            _store.SetupGetMusic(musicD);

            _musicLibrary.Add(musicA);
            _musicLibrary.Add(musicB);
            _musicLibrary.Add(musicC);
            _musicLibrary.Add(musicD);
            var playList = new PlayList(_musicLibrary) { "A", "B", "C", "D" };
            playList.SetPlaying("A");
            playList.UpdateProbability();
            const double totalPriority = 18d;
            const double unitProbability = 1d / totalPriority;
            _store.VerifyAll();
            AreEqual(unitProbability * (musicA.Priority+4d), playList["A"].Probability);
            AreEqual(unitProbability * musicB.Priority, playList["B"].Probability);
            AreEqual(unitProbability * (musicC.Priority+4d), playList["C"].Probability);
            AreEqual(unitProbability * musicD.Priority, playList["D"].Probability);

            AreEqual(unitProbability * 5, playList["A"].CumulativeProbability);
            AreEqual(unitProbability * 7, playList["B"].CumulativeProbability);
            AreEqual(unitProbability * 14, playList["C"].CumulativeProbability);
            AreEqual(unitProbability * 18, playList["D"].CumulativeProbability);
        }

        [Test]
        public void TwoClassic2RockClassicIsPlayingMusicHasAnalbum()
        {
            _store.SetupGetTags(new[]
                                    {
                                        new MusicTag {Id = Constants.Tags.Genre, Priority = 2},
                                        new MusicTag {Id = Constants.Tags.Album, Priority = 3}
                                    });
            _store.Setup(s => s.GetRepeatPriority()).Returns(1);
            var musicA = new Music("A") { Priority = 1 };
            musicA[Constants.Tags.Genre] = "Classic";
            musicA[Constants.Tags.Album] = "Bangladesh";
            var musicB = new Music("B") { Priority = 2 };
            musicB[Constants.Tags.Genre] = "Rock";
            var musicC = new Music("C") { Priority = 3 };
            musicC[Constants.Tags.Genre] = "Classic";
            musicC[Constants.Tags.Album] = "Bangladesh";
            var musicD = new Music("D") { Priority = 4 };
            musicD[Constants.Tags.Genre] = "Rock";

            _store.SetupGetMusic(musicA);
            _store.SetupGetMusic(musicB);
            _store.SetupGetMusic(musicC);
            _store.SetupGetMusic(musicD);

            _musicLibrary.Add(musicA);
            _musicLibrary.Add(musicB);
            _musicLibrary.Add(musicC);
            _musicLibrary.Add(musicD);
            var playList = new PlayList(_musicLibrary) { "A", "B", "C", "D" };
            playList.SetPlaying("A");
            playList.UpdateProbability();
            const double totalPriority = 30d;
            const double unitProbability = 1d / totalPriority;
            _store.VerifyAll();
            AreEqual(unitProbability * (musicA.Priority + 10d), playList["A"].Probability);
            AreEqual(unitProbability * musicB.Priority, playList["B"].Probability);
            AreEqual(unitProbability * (musicC.Priority + 10d), playList["C"].Probability);
            AreEqual(unitProbability * musicD.Priority, playList["D"].Probability);

            AreEqual(unitProbability * 11, playList["A"].CumulativeProbability);
            AreEqual(unitProbability * 13, playList["B"].CumulativeProbability);
            AreEqual(unitProbability * 26, playList["C"].CumulativeProbability);
            AreEqual(unitProbability * 30, playList["D"].CumulativeProbability);
        }
        [Test]
        public void TwoClassic2RockClassicIsPlayingRepeatPriorityZero()
        {
            _store.SetupGetTags(new[]
                                    {
                                        new MusicTag {Id = Constants.Tags.Genre, Priority = 2},
                                        new MusicTag {Id = Constants.Tags.Album, Priority = 3}
                                    });
            _store.Setup(s => s.GetRepeatPriority()).Returns(0);
            var musicA = new Music("A") { Priority = 1 };
            musicA[Constants.Tags.Genre] = "Classic";
            var musicB = new Music("B") { Priority = 2 };
            musicB[Constants.Tags.Genre] = "Rock";
            var musicC = new Music("C") { Priority = 3 };
            musicC[Constants.Tags.Genre] = "Classic";
            var musicD = new Music("D") { Priority = 4 };
            musicD[Constants.Tags.Genre] = "Rock";

            _store.SetupGetMusic(musicA);
            _store.SetupGetMusic(musicB);
            _store.SetupGetMusic(musicC);
            _store.SetupGetMusic(musicD);

            _musicLibrary.Add(musicA);
            _musicLibrary.Add(musicB);
            _musicLibrary.Add(musicC);
            _musicLibrary.Add(musicD);
            var playList = new PlayList(_musicLibrary) { "A", "B", "C", "D" };
            playList.SetPlaying("A");
            playList.UpdateProbability();
            const double totalPriority = 13d;
            const double unitProbability = 1d / totalPriority;
            _store.VerifyAll();
            AreEqual(0, playList["A"].Probability);
            AreEqual(unitProbability * musicB.Priority, playList["B"].Probability);
            AreEqual(unitProbability * (musicC.Priority + 4d), playList["C"].Probability);
            AreEqual(unitProbability * musicD.Priority, playList["D"].Probability);

            AreEqual(0, playList["A"].CumulativeProbability);
            AreEqual(unitProbability * 2, playList["B"].CumulativeProbability);
            AreEqual(unitProbability * 9, playList["C"].CumulativeProbability);
            AreEqual(unitProbability * 13, playList["D"].CumulativeProbability);
        }

        

        private static void AreEqual(double expected, double actual)
        {
            Assert.IsTrue(Math.Abs(expected-actual)<Zero,string.Format("Expected {0} Actual {1}",expected,actual));
        }
    }
}