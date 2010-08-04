using System;
using System.Collections;
using MarkovMusic.DataAccessLayer;

namespace Tests.MarkovMusic.Mocks.DataAccessLayer
{
    public class MockDatabaseResultSet : IDatabaseResultSet
    {
        #region Implementation of IDatabaseResultSet

        public ArrayList GetRow(int rowIndex)
        {
            throw new NotImplementedException();
        }

        public ArrayList GetColumn(int columnIndex)
        {
            throw new NotImplementedException();
        }

        public ArrayList GetColumn(string columnName)
        {
            throw new NotImplementedException();
        }

        public string GetField(int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public ArrayList Rows { get; set; }

        public ArrayList ColumnNames
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}