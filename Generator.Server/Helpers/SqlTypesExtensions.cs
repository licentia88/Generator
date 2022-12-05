using System.Data;

namespace Generator.Server.Helpers;

public static class SqlTypesExtensions
{

    public static Type SqlToType(this string pSqlType)
    {
        switch (pSqlType)
        {
            case "bigint":
            case "real":
                return typeof(long);
            case "numeric":
                return typeof(decimal);
            case "bit":
                return typeof(bool);

            case "smallint":
                return typeof(short);

            case "decimal":
            case "smallmoney":
            case "money":
                return typeof(decimal);

            case "int":
                return typeof(int);

            case "tinyint":
                return typeof(byte);

            case "float":
                return typeof(float);

            case "date":
            case "datetime2":
            case "smalldatetime":
            case "datetime":
            case "time":
                return typeof(DateTime);

            case "datetimeoffset":
                return typeof(DateTimeOffset);

            case "char":
            case "varchar":
            case "text":
            case "nchar":
            case "nvarchar":
            case "ntext":
                return typeof(string);


            case "binary":
            case "varbinary":
            case "image":
                return typeof(byte[]);

            case "uniqueidentifier":
                return typeof(Guid);

            default:
                return typeof(string);

        }

    }

    public static DbType ToDbType(this Type pType)
    {
        switch (pType.Name.ToLower())
        {
            case "byte":
                return DbType.Byte;
            case "sbyte":
                return DbType.SByte;
            case "short":
            case "int16":
                return DbType.Int16;
            case "uint16":
                return DbType.UInt16;
            case "int32":
                return DbType.Int32;
            case "uint32":
                return DbType.UInt32;
            case "int64":
                return DbType.Int64;
            case "uint64":
                return DbType.UInt64;
            case "single":
                return DbType.Single;
            case "double":
                return DbType.Double;
            case "decimal":
                return DbType.Decimal;
            case "bool":
            case "boolean":
                return DbType.Boolean;
            case "string":
                return DbType.String;
            case "char":
                return DbType.StringFixedLength;
            case "Guid":
                return DbType.Guid;
            case "DateTime":
                return DbType.DateTime;
            case "DateTimeOffset":
                return DbType.DateTimeOffset;
            case "byte[]":
                return DbType.Binary;
            case "byte?":
                return DbType.Byte;
            case "sbyte?":
                return DbType.SByte;
            case "short?":
                return DbType.Int16;
            case "ushort?":
                return DbType.UInt16;
            case "int?":
                return DbType.Int32;
            case "uint?":
                return DbType.UInt32;
            case "long?":
                return DbType.Int64;
            case "ulong?":
                return DbType.UInt64;
            case "float?":
                return DbType.Single;
            case "double?":
                return DbType.Double;
            case "decimal?":
                return DbType.Decimal;
            case "bool?":
                return DbType.Boolean;
            case "char?":
                return DbType.StringFixedLength;
            case "Guid?":
                return DbType.Guid;
            case "DateTime?":
                return DbType.DateTime;
            case "DateTimeOffset?":
                return DbType.DateTimeOffset;
            default:
                return DbType.String;
        }

    }

    public static DbType SqlToDbType(this string pSqlType)
    {
        return pSqlType.SqlToType().ToDbType();
    }

    public static object GetDefault(this Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }
        return null;
    }

}