using System;
using System.Data;

using Npgsql;
using NpgsqlTypes;

namespace NetBot.Bot.Services.Database
{
    public class DatabaseConnector
    {
        private readonly int _port;
        private readonly string _password;
        private readonly string _host;
        private readonly string _user;
        private readonly string _database;

        public NpgsqlConnection _connection { get; private set; }

        public DatabaseConnector(int port, string password, string host, string user, string database)
        {
            _port = port;
            _password = password;
            _host = host;
            _user = user;
            _database = database;

            _connection = new NpgsqlConnection($"Host={_host};Username={user};Password={_password};Database={_database}");
            _connection.Open();
        }

        public async Task<Dictionary<string, object>> ExecuteCommandAsync(string sql)
        {
            await using var cmd = new NpgsqlCommand(sql, _connection);
            await using var reader = await cmd.ExecuteReaderAsync();

            var dict = new Dictionary<string, object>();

            while (await reader.ReadAsync())
            {
                dict.Add(reader.GetName(0), reader.GetData(0));
            }

            return dict;
        }
    }
}