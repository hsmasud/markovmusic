using CompositApplication;

namespace MarkovMusic.Clients.MainModule.MusicGrid
{
    public class MusicGridPresenter:APresenter, IMusicGridPresenter
    {
        public MusicGridPresenter(IMusicGridView view) : base(view)
        {
        }
    }
}
