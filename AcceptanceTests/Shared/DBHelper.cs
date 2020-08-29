using System;
using System.Data.SqlClient;
using System.IO;

namespace AcceptanceTests.Shared
{
    public class DBHelper
    {
        public static int ExecuteScriptFile(SqlConnection connection, string relativeFilePath)
        {
            var location = Path.Join(TestConfig.GetTestRootPath(), relativeFilePath);
            var script = File.ReadAllText(location);
            using var command = connection.CreateCommand();
            command.CommandText = script;
            return command.ExecuteNonQuery();
        }

        public static string FormatDate(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss.0000000");
        }
    }
}
