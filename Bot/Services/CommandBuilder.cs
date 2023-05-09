using Discord;
using Discord.WebSocket;
using NetBot;
using log4net;
using NetBot.Bot.Services;
using NetBot.Bot.Commands;
using NetBot.Lib.Attributes;

namespace NetBot.Bot.Services
{
    public class CommandBuilder
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(CommandBuilder));

        private readonly DiscordSocketClient _client;

        public CommandBuilder(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task RegisterCommands()
        {
            var raceCommands = typeof(Races).GetMethods().Where(c => c.CustomAttributes.Any(a => a.AttributeType.Name == "Description"));

            foreach (var cmd in raceCommands)
            {
                var attrs = System.Attribute.GetCustomAttributes(cmd);
                Console.WriteLine(cmd.Name);
                var slashCommand = new SlashCommandBuilder();

                foreach (System.Attribute attr in attrs)
                {
                    if (attr is DescriptionAttribute d)
                    {
                        slashCommand.WithDescription(d.GetDescription());
                    }
                    else if (attr is OptionsAttribute o)
                    {
                        slashCommand.AddOption(o.GetOptionName(), o.OptionType, o.GetDescription(), o.isRequired);
                    }
                }

                await _client.GetGuild(1012834935469506590).CreateApplicationCommandAsync(slashCommand.Build());
            }
        }
    }
}