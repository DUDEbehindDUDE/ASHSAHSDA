using Discord.WebSocket;
using log4net;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBot.Bot.Services
{
    public class DatabaseHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseHandler));

        string connectionString = DotNetEnv.Env.GetString("CONNECTION_STRING");

        public async Task<bool> CheckDNDTos(SocketSlashCommand command)
        {
            log.Debug(connectionString);
            ulong userID = command.User.Id;
            await CheckCreateUser(userID);

            string sql = "SELECT COUNT agreeToDND FROM users WHERE id = :userID";
            using (OracleConnection connection = new OracleConnection(connectionString))
            using (OracleCommand getTos =  new OracleCommand(sql, connection))
            {
                await connection.OpenAsync();

                getTos.Parameters.Add("userID", userID);
                int? tos = (int?)await getTos.ExecuteScalarAsync();

                if (tos.Equals(0)) return false;
                else return true;
            }
        }

        private async Task CheckCreateUser(ulong id)
        {
            string sql = "SELECT COUNT(*) FROM users WHERE id = :userID";

            using (OracleConnection connection = new OracleConnection(connectionString))
            using (OracleCommand command = new OracleCommand(sql, connection))
            {
                log.Debug("CheckCreateUser.openasync");
                await connection.OpenAsync();
                log.Debug("it's opened");

                command.Parameters.Add(new OracleParameter("userID", id));
                var result = await command.ExecuteScalarAsync();

                if (result is null)
                {
                    OracleCommand createUser = new OracleCommand("INSERT INTO users (id) VALUES (:userID)", connection);
                    createUser.Parameters.Add("userID", id);
                    await createUser.ExecuteNonQueryAsync();
                    log.Debug($"Added {id} into database");
                }
            }
        }
    }
}
