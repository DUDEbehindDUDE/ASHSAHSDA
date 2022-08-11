// will fix later
#pragma warning disable

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using log4net;

namespace NetBot
{
    public class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        private readonly CommandService _commands = new CommandService();

        private DiscordSocketClient _client;
        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            DotNetEnv.Env.Load(".env");
            string token = Environment.GetEnvironmentVariable("TOKEN");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            var commandHandler = new CommandHandler(_client, _commands, '?');
            await commandHandler.InstallCommandsAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            log.Info(msg.ToString());
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}