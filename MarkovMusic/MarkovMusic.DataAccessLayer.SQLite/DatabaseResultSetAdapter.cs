using System.Collections;
using SQLite.NET;

namespace MarkovMusic.DataAccessLayer.SQLite
{
    public class DatabaseResultSetAdapter: IDatabaseResultSet
    {
        private readonly SQLiteResultSet _adaptee;

        public DatabaseResultSetAdapter(SQLiteResultSet adaptee)
        {
            _adaptee = adaptee;
        }
        #region Implementation of IDatabaseResultSet

        public ArrayList GetRow(int rowIndex)
        {
            return _adaptee.GetRow(rowIndex);
        }

        public ArrayList GetColumn(int columnIndex)
        {
            return _adaptee.GetColumn(columnIndex);
        }

        public ArrayList GetColumn(string columnName)
        {
            return _adaptee.GetColumn(columnName);
        }

        public string GetField(int rowIndex, int columnIndex)
        {
            return _adaptee.GetField(rowIndex, columnIndex);
        }

        public ArrayList Rows
        {
            get { return _adaptee.Rows; }
        }

        public ArrayList ColumnNames
        {
            get { return _adaptee.ColumnNames; }
        }

        #endregion
    }
}