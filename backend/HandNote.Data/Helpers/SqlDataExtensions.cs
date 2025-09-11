using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Helpers
{
    public static class SqlDataReaderExtensions
    {
        public static T? GetValueOrDefault<T>(this SqlDataReader reader, string columnName)
        {
            try
            {
                var ordinal = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(ordinal))
                    return default(T);

                return (T)reader.GetValue(ordinal);
            }
            catch
            {
                return default(T);
            }
        }

        public static T GetValue<T>(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return (T)reader.GetValue(ordinal);
        }

    }
    public static class SqlCommandExtensions
    {
        public static void AddParameter(this SqlCommand command, string parameterName, object? value)
        {
            command.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
        }
    }
}