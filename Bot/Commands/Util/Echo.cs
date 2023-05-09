using Discord;
using Discord.WebSocket;
using NetBot.Bot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBot.Bot.Commands.Util
{
    public class Echo : ISlashCommand
    {
        public SlashCommandBuilder SlashCommandBuilder => new SlashCommandBuilder()
                .WithName("echo")
                .WithDescription("Repeats a given phrase")
                .AddOption("text", ApplicationCommandOptionType.String, "The text you want to be echoed", isRequired: true, isAutocomplete: true);

        public ulong? Guild => null;

        public async Task CommandEvent(SocketSlashCommand command)
        {
            await command.RespondAsync(command.Data.Options.First().Value.ToString());
        }
    }
}
