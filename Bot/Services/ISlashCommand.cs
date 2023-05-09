using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBot.Bot.Services
{
    public interface ISlashCommand
    {
        SlashCommandBuilder SlashCommandBuilder { get; }
        ulong? Guild { get; } // Setting as null will make the command a global command; otherwise specify GuildId
        Task CommandEvent(SocketSlashCommand command);
    }
}
