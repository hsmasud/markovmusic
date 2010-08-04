using CompositApplication;

namespace MarkovMusic.Clients.Shell
{
    public class MarkovMusicApplicationStarter:ApplicationStarter
    {
        protected override Bootstrapper CreateBootstrapper()
        {
            return new MarkovMusicBootstrapper();
        }
    }
}