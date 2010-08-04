using CompositApplication;

namespace MarkovMusic.Clients.MainModule.MusicGrid
{
    /// <summary>
    /// Interaction logic for MusicGridView.xaml
    /// </summary>
    public partial class MusicGridView : IMusicGridView
    {
        public MusicGridView()
        {
            InitializeComponent();
        }

        public IPresenter Presenter
        {
            set { DataContext = value; }
        }
    }
}
