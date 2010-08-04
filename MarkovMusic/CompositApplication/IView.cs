namespace CompositApplication
{
    public interface IView
    {
        IPresenter Presenter { set; }
    }
}