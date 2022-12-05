namespace Generator.Server.Models.Shema;

public class PrimaryKey
{
    public string TableName { get; set; }
    public string PrimaryKeyName { get; set; }
    public string FieldName { get; set; }
    public bool IsAutoIncrement { get; set; }
}

