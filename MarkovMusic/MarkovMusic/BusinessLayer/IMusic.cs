namespace MarkovMusic.BusinessLayer
{
    public interface IMusic
    {
        string Id { get; }
        double Priority { get; set; }
        string this[string key] { get; set; }
        bool IsTagMatches(IMusic music, IMusicTag musicTag);
        bool IsTagMatches(IMusicTag musicTag);
    }
}