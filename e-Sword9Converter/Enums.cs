namespace e_Sword9Converter
{
    public enum DbType
    {
        NULL = 0,
        AUTONUMBER,
        INT,
        REAL,
        TEXT,
        BLOB,
        NVARCHAR,
        BOOL
    }
    public enum columnType
    {
        Access = -1,
        Both = 0,
        Sql = 1
    }
    public enum tableType
    {
        Access = -1,
        Both = 0,
        Sql = 1
    }
}
