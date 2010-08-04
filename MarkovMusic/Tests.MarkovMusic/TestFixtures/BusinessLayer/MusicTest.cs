using NUnit.Framework;
using MarkovMusic;

namespace Tests.MarkovMusic.TestFixtures.BusinessLayer
{
    [TestFixture]
    public class MusicTest
    {
        [Test]
        public void MusicReturnsEmptyStringIfNotEdited()
        {
            var music = new Music();
            Assert.AreEqual("",music[Constants.Tags.Genre]);
        }
        [Test]
        public void MusicReturnsActualValueIfEdited()
        {
            var music = new Music();
            music[Constants.Tags.Genre] = "Classic";
            Assert.AreEqual("Classic", music[Constants.Tags.Genre]);
            
        }
    }
}