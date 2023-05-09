using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Interactions;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NetBot.Bot.Commands;

namespace NetBot.Bot.Services
{
    public class SlashCommandHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SlashCommandHandler));
        private readonly DiscordSocketClient _client;

        public SlashCommandHandler(DiscordSocketClient client)
        {
            _client = client;
            //_commands = commands;
            //_service = service;

            _client.SlashCommandExecuted += e_SlashCommandExecuted;
        }

        public async Task Global_Slash_Commands()
        {
            var echoCommand = new SlashCommandBuilder()
                .WithName("echo")
                .WithDescription("Repeats a given phrase")
                .AddOption("text", ApplicationCommandOptionType.String, "The text you want to be echoed THIS WAS GENERATED MANUALLY", true);
            var userInfo = new SlashCommandBuilder()
                .WithName("userinfo")
                .WithDescription("Displays information on a given user, or yourself if none is given THIS WAS GENERATED MANUALLY")
                .AddOption("user", ApplicationCommandOptionType.User, "The user whose information you want to display");
            var raceInfo = new SlashCommandBuilder()
                .WithName("raceinfo")
                .WithDescription("Get info on a D&D race")
                .AddOption("race", ApplicationCommandOptionType.String, "The race to fetch information on THIS WAS GENERATED MANUALLY", true);

            log.Debug($"Client: {_client}");
            try
            {
                await _client.CreateGlobalApplicationCommandAsync(echoCommand.Build());
                await _client.CreateGlobalApplicationCommandAsync(userInfo.Build());
                //await _client.GetGuild(1012834935469506590).CreateApplicationCommandAsync(raceInfo.Build());
            }
            catch (HttpException exception)
            {
                log.Error(exception.StackTrace, exception);
            }
        }

        public async Task e_SlashCommandExecuted(SocketSlashCommand command)
        {
            string name = command.Data.Name;
            var raceCommands = typeof(Races).GetMethods();

            await command.RespondAsync($"`raceCommands is null: {raceCommands is null}`");

            if (!(raceCommands is null))
            {
                foreach (var rc in raceCommands)
                {
                    if (rc.Name.ToLower() == command.Data.Name)
                    {
                        rc.Invoke(null, new object?[] { command });
                        log.Debug($"Command Executed: {command.Data.Name}");
                        break;
                    }
                }
            }
        }
    }
}
