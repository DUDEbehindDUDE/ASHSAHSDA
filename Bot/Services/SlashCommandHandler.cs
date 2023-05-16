using Discord;
using Discord.Net;
using Discord.WebSocket;
using log4net;
using System.Data;
using System.Reflection;
using static NetBot.Bot.Services.DatabaseHandler;

namespace NetBot.Bot.Services
{
    public class SlashCommandHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SlashCommandHandler));
        private readonly DiscordSocketClient _client;

        public SlashCommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _client.SlashCommandExecuted += SlashCommandEvent;
            _client.Ready += RegisterSlashCommands;
        }

        static readonly List<string> dndCommands = new()
        {   // I know this isn't an ideal way to implement this; i'll fix it in a future PR when we switch the command handler to use attributes 
            "raceinfo"
        };

        static dynamic GetCommands()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(ISlashCommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        }

        public async Task RegisterSlashCommands()
        {
            foreach (var slashCommand in GetCommands())
            {
                ISlashCommand? command = Activator.CreateInstance(slashCommand) as ISlashCommand;
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

        public static async Task SlashCommandEvent(SocketSlashCommand command)
        {
            foreach (var slashCommand in GetCommands())
            {
                ISlashCommand? _command = Activator.CreateInstance(slashCommand) as ISlashCommand;
                if (_command is null) break;

                SlashCommandBuilder builder = _command.SlashCommandBuilder;
                if (!builder.Name.Equals(command.CommandName)) 
                    continue;

                if (dndCommands.Contains(command.CommandName))
                {
                    bool tos = await CheckDNDTos(command);
                    if (tos == false) return;
                }
                await _command.CommandEvent(command);
            }
        }
    }
}
