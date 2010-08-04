namespace MarkovMusic.BusinessLayer
{
    public interface IMusicInPlayList:IMusic
    {
        double Probability { get; set; }
        double CumulativeProbability { get; }
        void UpdateProbability(double totalPriority, double averageTagsPriority, ref double cumulativeProbability, double probabilityMagnifyer);
    }
}