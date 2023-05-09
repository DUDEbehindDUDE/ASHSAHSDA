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
        private readonly CommandService _commands = new CommandService();
        private DiscordSocketClient? _client;
        private ComponentHandler? componentHandler;
        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += new SlashCommandHandler(_client).RegisterSlashCommands;
            componentHandler = new ComponentHandler(_client);

            DotNetEnv.Env.TraversePath().Load(".env");

            string token = DotNetEnv.Env.GetString("TOKEN");
            if (String.IsNullOrEmpty(token))
            {
                log.Fatal("Discord token not provided! Create a file named '.env' and add 'TOKEN=<your bot's token>' to it.");
                return;
            }

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        protected Task Log(LogMessage msg)
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