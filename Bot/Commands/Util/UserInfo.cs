using Discord;
using Discord.WebSocket;
using NetBot.Bot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHSAHSDA.Bot.Commands.Util
{
    public class UserInfo : ISlashCommand
    {
        public SlashCommandBuilder SlashCommandBuilder => new SlashCommandBuilder()
            .WithName("userinfo")
            .WithDescription("Displays information on a given user, or yourself if none is given")
            .AddOption("user", ApplicationCommandOptionType.User, "The user whose information you want to display");

        public ulong? Guild => null;

        public async Task CommandEvent(SocketSlashCommand command)
        {
            if (command.Data.Options.First() is null)
            {
                await command.RespondAsync($"You're {command.User}!");
            }
            else
            {
                await command.RespondAsync($"They're {command.Data.Options.First().Value.ToString()}");
            }
        }
    }
}
