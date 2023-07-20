using Dapper;
using Npgsql;

namespace UID.Persistance;

public class Repository
{
    private const string USER = "postgres";
    private const string PASSWORD = "postgres";
    private const string HOST = "localhost"; //"192.168.0.185";
    private const string PORT = "5432";
    private const string DATABASE = "uuid-test";

    private const int BulkSize = 1000;

    private string ConnectionString => $"User ID={USER};Password={PASSWORD};Host={HOST};Port={PORT};Database={DATABASE};";

    public Repository()
    {
        SqlMapper.AddTypeHandler(new GuidUlidHandler());
    }

    public async Task InsertBulk(IList<Ulid> data)
    {
        var bulkCount = data.Count() / BulkSize;
        if (data.Count() % BulkSize > 0) bulkCount++;

        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            var sql = "INSERT INTO test_ulid (id) VALUES (@id)";

            for (int i = 0; i < bulkCount; i++)
            {
                var customers = new List<object>();

                for (var j = i * BulkSize; j < (i + 1) * BulkSize; j++)
                {
                    customers.Add(new { id = data[j] });
                }

                await connection.ExecuteAsync(sql, customers);

                var processedCount = (i + 1) * BulkSize;
                Console.WriteLine($"Processed {processedCount} items, which is {processedCount * 100 / data.Count()}% of total.");
            }
        }
    }

    public async Task InsertBulk(IList<Guid> data)
    {
        var bulkCount = data.Count() / BulkSize;
        if (data.Count() % BulkSize > 0) bulkCount++;

        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            var sql = "INSERT INTO test_guid (id) VALUES (@id)";

            for (int i = 0; i < bulkCount; i++)
            {
                var customers = new List<object>();

                for (var j = i * BulkSize; j < (i + 1) * BulkSize; j++)
                {
                    customers.Add(new { id = data[j] });
                }

                await connection.ExecuteAsync(sql, customers);

                var processedCount = (i + 1) * BulkSize;
                Console.WriteLine($"Processed {processedCount} items, which is {processedCount * 100 / data.Count()}% of total.");
            }
        }
    }
}