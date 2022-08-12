using System;
using System.Data;

using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace NetBot.Bot.Services.Database
{
    public class Connector
    {
        private readonly int _port;
        private readonly string _password;
        private readonly string _host;
        private readonly string _user;
        private readonly string _database;

        private readonly MySqlConnection _connection;

        Connector(int port, string password, string host, string user, string database)
        {
            _port = port;
            _password = password;
            _host = host;
            _user = user;
            _database = database;

            _connection = new MySqlConnection(String.Join(';', _host, _user, _database, _port, _password));
        }
    }
}