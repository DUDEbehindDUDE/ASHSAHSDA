using Discord;
using Discord.WebSocket;
using NetBot;
using log4net;
using NetBot.Bot.Services;

namespace NetBot.Bot.Services
{
    public class CommandHandler
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(CommandHandler));

        private readonly DiscordSocketClient _client;

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task SlashCommandHandler()
        {

        }
    }
}