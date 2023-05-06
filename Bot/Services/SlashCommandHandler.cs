using Discord;
using Discord.Net;
using Discord.WebSocket;
using log4net;
using NetBot.Bot.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBot.Bot.Services
{
    public class SlashCommandHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SlashCommandHandler));
        private DiscordSocketClient _client;

        public SlashCommandHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task Global_Slash_Commands()
        {
            var echoCommand = new SlashCommandBuilder()
                .WithName("echo")
                .WithDescription("Repeats a given phrase")
                .AddOption("text", ApplicationCommandOptionType.String, "The text you want to be echoed", true);
            var userInfo = new SlashCommandBuilder()
                .WithName("userinfo")
                .WithDescription("Displays information on a given user, or yourself if none is given")
                .AddOption("user", ApplicationCommandOptionType.User, "The user whose information you want to display");
            log.Debug($"Client: {_client}");
            try
            {
                await _client.CreateGlobalApplicationCommandAsync(echoCommand.Build());
                await _client.CreateGlobalApplicationCommandAsync(userInfo.Build());
            }
            catch (HttpException exception)
            {
                log.Error(exception.StackTrace, exception);
            }
        }
    }
}
