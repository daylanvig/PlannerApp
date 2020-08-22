using System.Configuration;
using System.Data.SqlClient;

namespace AcceptanceTests.Shared
{
    public class Database
    {
        public static SqlConnection CreateUserDBConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString);
        }

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["PlannerContext"].ConnectionString);
        }
    }
}
