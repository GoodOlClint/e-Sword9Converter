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
        BOOL,
        DATETIME
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
    public enum updateStatus
    {
        Loading,
        Converting,
        Saving,
        Finishing
    }
}
