namespace MarkovMusic.DataAccessLayer
{
    public interface IDatabaseAdapter
    {
        void OpenDatabase();
        void ExecuteNonQuery(string sql);
        void Close();
        IDatabaseResultSet Execute(string sql);
    }
}