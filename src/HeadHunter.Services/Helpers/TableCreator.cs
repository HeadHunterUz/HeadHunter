using Dapper;
using System.Data;
using HeadHunter.Domain.Enums;

namespace HeadHunter.Services.Helpers;

public class TableCreator
{
    private readonly IDbConnection _connection;

    public TableCreator(IDbConnection connection)
    {
        _connection = connection;
    }

    public void CreateTable<T>(string tableName)
    {
        var entityType = typeof(T);
        var columns = GetColumns(entityType);

        var columnDefinitions = string.Join(", ", columns);

        var createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (id SERIAL PRIMARY KEY, {columnDefinitions})";

        _connection.Execute(createTableQuery);
    }

    private List<string> GetColumns(Type entityType)
    {
        var columns = new List<string>();

        foreach (var property in entityType.GetProperties())
        {
            // Skip the "id" property
            if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                continue;

            var columnName = property.Name;
            var dataType = GetDefaultDataType(property.PropertyType);

            columns.Add($"{columnName} {dataType}");
        }

        return columns;
    }

    private string GetDefaultDataType(Type propertyType)
    {
        if (propertyType == typeof(int))
        {
            return "INT";
        }
        else if (propertyType == typeof(long))
        {
            return "BIGINT";
        }
        else if (propertyType == typeof(string))
        {
            return "VARCHAR(100)";
        }
        else if (propertyType == typeof(decimal))
        {
            return "NUMERIC";
        }
        else if (propertyType == typeof(double))
        {
            return "DOUBLE PRECISION";
        }
        else if (propertyType == typeof(float))
        {
            return "REAL";
        }
        else if (propertyType == typeof(bool))
        {
            return "BOOLEAN";
        }
        else if (propertyType == typeof(DateTime))
        {
            return "TIMESTAMP";
        }
        else if (propertyType == typeof(Guid))
        {
            return "UUID";
        }
        else if (propertyType == typeof(byte[]))
        {
            return "BYTEA";
        }
        else if (propertyType == typeof(TimeSpan))
        {
            return "TIME";
        }
        else if (propertyType == typeof(ApplyStatus))
        {
            return "INT";
        }
        else if (propertyType.IsEnum)
        {
            return "INT";
        }

        throw new NotSupportedException($"Data type mapping not supported for property type: {propertyType.Name}");
    }
}
