using System.Data.Common;

namespace e_Sword9Converter
{
    public interface IColumn
    {
        DbType Type { get; set; }
        int Length { get; set; }
        string Name { get; set; }
        bool NotNull { get; set; }
        string PropertyName { get; set; }
        columnType colType { get; }
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
        IParent Parent { get; set; }
    }

    public interface IParent
    {
        string GetPassword(string path);
        void UpdateStatus();
        void SetMaxValue(int value);
    }
}
