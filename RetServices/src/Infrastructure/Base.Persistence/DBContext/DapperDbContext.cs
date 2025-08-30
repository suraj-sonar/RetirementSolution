using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;


namespace Base.Persistence.DBContext
{
    //add dapper context code here
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public DapperDbContext(IConfiguration Configuration)
        {
            _configuration = Configuration;
            string ConnectionString = _configuration.GetConnectionString("RSConnectionString");

            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new ArgumentException("Connection string 'RSConnectionString' is not configured.");
            }
            //Create a new  with the retrived connection string
            _connection = new SqlConnection(ConnectionString);
        }

        public IDbConnection DbConnection { get { return _connection; } }
    }
}
