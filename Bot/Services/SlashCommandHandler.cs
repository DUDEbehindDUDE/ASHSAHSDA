using Discord;
using Discord.Net;
using Discord.WebSocket;
using log4net;
using NetBot.Bot.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

        static dynamic GetCommands()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(ISlashCommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        }

        public async Task Global_Slash_Commands()
        {
            foreach (var slashCommand in GetCommands())
            {
                var command = Activator.CreateInstance(slashCommand) as ISlashCommand;
                if (command is null) break;

                await BuildSlashCommand(command);
            }
        }

        private async Task BuildSlashCommand(dynamic command)
        {
            SlashCommandBuilder commandBuilder = command.SlashCommandBuilder;
            if (command.Guild is null)
            {
                try
                {
                    await _client.CreateGlobalApplicationCommandAsync(commandBuilder.Build());
                }
                catch (HttpException exception)
                {
                    log.Error(exception.StackTrace, exception);
                }
            }
            else
            {
                try
                {
                    await _client.GetGuild((ulong)command.Guild).CreateApplicationCommandAsync(commandBuilder.Build());
                }
                catch (HttpException exception)
                {
                    log.Error(exception.StackTrace, exception);
                }
            }
        }

        public async Task SlashCommandEvent(SocketSlashCommand command)
        {
            foreach (var slashCommand in GetCommands())
            {
                var _command = Activator.CreateInstance(slashCommand) as ISlashCommand;
                if (_command is null) break;
                
                SlashCommandBuilder builder = _command.SlashCommandBuilder;
                if (builder.Name == command.CommandName)
                {
                    await _command.CommandEvent(command);
                }
            }
        }
    }
}
