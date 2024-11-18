using System.Data;
using Npgsql;

namespace CrudProfisaComDapper.Connections
{
    public class PostgreSqlConnectionFactory
    {
        private readonly string _connectionString;

        public PostgreSqlConnectionFactory(IConfiguration configuration)
        {
            // Carrega configurações do appsettings.json ou variáveis de ambiente
            string databaseHost = Environment.GetEnvironmentVariable("DB_HOST") ?? configuration["DatabaseSettings:DB_HOST"];
            string databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? configuration["DatabaseSettings:DATABASE_NAME"];
            string databaseUser = Environment.GetEnvironmentVariable("DATABASE_USER") ?? configuration["DatabaseSettings:DATABASE_USER"];
            string databasePass = Environment.GetEnvironmentVariable("DATABASE_PASS") ?? configuration["DatabaseSettings:DATABASE_PASS"];

            _connectionString = $"Host={databaseHost};Database={databaseName};Username={databaseUser};Password={databasePass};";
        }

        /// <summary>
        /// Obtém uma conexão aberta com o banco de dados.
        /// </summary>
        /// <returns>Uma conexão aberta do tipo IDbConnection.</returns>
        public IDbConnection GetConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
