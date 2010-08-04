using SQLite.NET;
using System.IO;

namespace MarkovMusic.DataAccessLayer.SQLite
{
    public class DatabaseAdapter : IDatabaseAdapter
    {
        private SQLiteClient _database;

        #region Implementation of IDatabaseAdapter

        public void OpenDatabase()
        {
            var isDatabaseFileExists = File.Exists(Constants.DatabaseName);
            _database=new SQLiteClient(Constants.DatabaseName);
            if(!isDatabaseFileExists)
            {
                CreateTables();
            }
        }

        private void CreateTables()
        {
            _database.Execute(Constants.Sql.CreateMusicTable);
        }

        public void ExecuteNonQuery(string sql)
        {
            _database.Execute(sql);
        }

        public void Close()
        {
            _database.Close();
        }

        public IDatabaseResultSet Execute(string sql)
        {
            return new DatabaseResultSetAdapter(_database.Execute(sql));
        }

        #endregion
    }
}