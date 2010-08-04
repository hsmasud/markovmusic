using System.Collections;

namespace MarkovMusic.DataAccessLayer
{
    public interface IDatabaseResultSet
    {
        ArrayList GetRow(int rowIndex);
        ArrayList GetColumn(int columnIndex);
        ArrayList GetColumn(string columnName);
        string GetField(int rowIndex, int columnIndex);
        ArrayList Rows { get; }
        ArrayList ColumnNames { get; }
    }
}