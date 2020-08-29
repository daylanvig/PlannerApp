using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace AcceptanceTests.Shared
{
    public class UserDBConnection : IDisposable
    {
        private readonly SqlConnection connection;
        public UserDBConnection()
        {
            var config = TestConfig.GetConfig();
            connection = new SqlConnection(config.GetConnectionString("UserContext"));
            connection.Open();
        }

        public T ExecuteScalar<T>(string script)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = script;
            return (T)cmd.ExecuteScalar();
        }

        public void PopulateDB()
        {
            DBHelper.ExecuteScriptFile(connection, Path.Join("Accounts", "Data", "populatedb.sql"));
            var itemDate = DateTime.Now.AddHours(2);
            var startTimeString = DBHelper.FormatDate(itemDate);
            var endTimeString = DBHelper.FormatDate(itemDate.AddHours(1));
            var testItemScript = $"INSERT[dbo].[PlannerItem]([ID], [TenantID], [Description], [PlannedActionDate], [PlannedEndTime], [CompletionDate], [CategoryID]) VALUES(1, N'cfccba65-bd81-4fac-93e3-bae0ebaba3df', N'Program', CAST(N'{startTimeString}' AS DateTime2), CAST(N'{endTimeString}' AS DateTime2), NULL, NULL);";
            using var command = connection.CreateCommand();
            command.CommandText = testItemScript;
            command.ExecuteNonQuery();
        }

        public void CleanupDB()
        {
            DBHelper.ExecuteScriptFile(connection, Path.Join("Accounts", "Data", "cleanupdb.sql"));
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
