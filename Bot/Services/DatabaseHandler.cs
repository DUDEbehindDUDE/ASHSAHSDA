using Discord;
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
        private static readonly string connectionString = DotNetEnv.Env.GetString("CONNECTION_STRING") + "Min Pool Size=5;";

        public static async Task<bool> CheckDNDTos(SocketSlashCommand command)
        {
            ulong userID = command.User.Id;

            string sql = "SELECT agreeToDND FROM users WHERE id = :userID";
            using (OracleConnection connection = new OracleConnection(connectionString))
            using (OracleCommand getTos =  new OracleCommand(sql, connection))
            {
                try
                {
                    await CheckCreateUser(userID);

                    await connection.OpenAsync();

                    getTos.Parameters.Add("userID", (long)userID);
                    short? tos = (short?)await getTos.ExecuteScalarAsync(); // When reading from the database, it's a short?; when writing it's an int. Very confusing.

                    if (tos == 0)
                    {
                        Embed embed = new EmbedBuilder()
                            .WithTitle("Hold up!")
                            .WithDescription("Before you can use any commands related to DND, you must aknowledge that you already own the rulebooks, and otherwise any content that goes with it. If you agree to this, run the command </agreetoterms:1106074501122367538>.")
                            .WithColor(Color.Red)
                            .Build();
                        await command.RespondAsync(embed: embed, ephemeral: true);
                        return false;
                    }
                    else return true;
                } 
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    return true;
                }
            }
        }

        public static async Task CheckCreateUser(ulong id)
        {
            string sql = "SELECT * FROM users WHERE id = :userID";

            using (OracleConnection connection = new(connectionString))
            using (OracleCommand command = new(sql, connection))
            {
                await connection.OpenAsync();
                command.Parameters.Add(new OracleParameter("userID", (long)id));
                var result = await command.ExecuteScalarAsync();
                if (result is null)
                {
                    OracleCommand createUser = new("INSERT INTO users (id) VALUES (:userID)", connection);
                    createUser.Parameters.Add("userID", (long)id);
                    await createUser.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<(bool? previous, bool? result)> UpdateDNDTerms(ulong discordId, bool value)
        {
            int agreeToTerms = value ? 1 : 0;
            short? pdbValue;
            short? dbValue;

            string sql = "UPDATE users SET AGREETODND = :agree WHERE id = :id";

            try
            {
                await CheckCreateUser(discordId);

                using (OracleConnection connection = new(connectionString))
                using (OracleCommand command = new(sql, connection))
                using (OracleCommand checkValue = new("SELECT agreeToDND FROM users WHERE id = :id", connection))
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(new OracleParameter("agree", agreeToTerms));
                    command.Parameters.Add(new OracleParameter("id", (long)discordId));
                    checkValue.Parameters.Add("id", (long)discordId);


                    pdbValue = (short?)await checkValue.ExecuteScalarAsync();
                    await command.ExecuteScalarAsync();
                    dbValue = (short?)await checkValue.ExecuteScalarAsync();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return (null, null);
            }

            bool previous = (pdbValue == 0) ? false : true;
            bool result = (dbValue == 0) ? false : true;
            return (previous, result);
        }
    }
}
