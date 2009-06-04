using System.Data.Common;

namespace eSword9Converter
{
    public interface IColumn
    {
        DbType Type { get; set; }
        int Length { get; set; }
        string Name { get; set; }
        bool NotNull { get; set; }
        string PropertyName { get; set; }
        columnType colType { get; }
        string Hash { get; }
    }

    public interface IIndex
    {
        string Name { get; set; }
    }

    public interface ITable
    {
        void SaveToDatabase(DbProviderFactory Factory, string connectionString);
        string SQLCreateStatement();
        void Load(DbProviderFactory Factory, string connectionString);
        string TableName { get; set; }
        IDatabase DB { get; set; }
    }

    public interface IDatabase
    {
        string SourceDB { get; set; }
        string DestDB { get; set; }
        string FileName { get; set; }
        void Stop();
        bool Running { get; set; }
        bool Skip { get; set; }
    }
}
