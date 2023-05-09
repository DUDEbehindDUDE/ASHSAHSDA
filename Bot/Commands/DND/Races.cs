using Discord;
using Discord.Net;
using Discord.WebSocket;
using NetBot.Bot.Services;
using NetBot.Lib.Attributes;
using NetBot.Lib.Types.JSON;
using Newtonsoft.Json;

namespace NetBot.Bot.Commands.DND
{
    public class RaceInfo : ISlashCommand
    {
        public SlashCommandBuilder SlashCommandBuilder => new SlashCommandBuilder()
            .WithName("raceinfo")
            .WithDescription("Get information on a given D&D race")
            .AddOption("race", ApplicationCommandOptionType.String, "The race to get information on", isRequired: true);

        public ulong? Guild => null;

        public async Task CommandEvent(SocketSlashCommand command)
        {
            string json = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "data", "races.json"));
            Root? races = JsonConvert.DeserializeObject<Root>(json);

            string arg = (string)command.Data.Options.First().Value;
            Race? race = races?.race.Find(r => r.name.ToLower() == arg.ToLower());

            await command.Channel.SendMessageAsync($"{race?.name ?? "?"} **Source:** {race?.source}p{race?.page}");
        }
    }
}