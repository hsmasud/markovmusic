namespace MarkovMusic.BusinessLayer
{
    public interface IMusicTag
    {
        string Header { get; set; }
        string Id { get; set; }
        double Priority { get; set; }
    }
}