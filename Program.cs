using Discord;
using Discord.WebSocket;
using Discord.Commands;
using log4net;
using NetBot.Bot.Services;
using System.Diagnostics;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace NetBot
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        private DiscordSocketClient? _client;
        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += new SlashCommandHandler(_client).RegisterSlashCommands;

            DotNetEnv.Env.TraversePath().Load(".env");

            string token = DotNetEnv.Env.GetString("TOKEN");
            if (String.IsNullOrEmpty(token))
            {
                log.Fatal("Discord token not provided! Create a file named '.env' and add 'TOKEN=<your bot's token>' to it.");
                return;
            }
            if (String.IsNullOrEmpty(DotNetEnv.Env.GetString("CONNECTION_STRING")))
            {
                log.Warn("No database connection string provided! There will likely be errors running commands that involve connecting to a database. To change this, add CONNECTION_STRING=<your database's connection string> to your .env file.");
            }

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        protected static Task Log(LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    log.Fatal(msg.Message, msg.Exception);
                    break;

                case LogSeverity.Error:
                    log.Error(msg.Message, msg.Exception);
                    break;

                case LogSeverity.Warning:
                    log.Warn(msg.Message, msg.Exception);
                    break;

                case LogSeverity.Info:
                    log.Info(msg.Message, msg.Exception);
                    break;

                default:
                    log.Debug(msg.Message, msg.Exception);
                    break;
            }
            return Task.CompletedTask;
        }
    }
}