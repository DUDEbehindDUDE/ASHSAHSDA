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
            Race? race = races?.race.Find(r => r.name.ToLower().Contains(arg.ToLower()));

            // there should probably be a more robust check for this in the future, plenty of valid races return no results as it is
            IEnumerable<Race>? relevantRaces = races?.race.Where(r => r.name.ToLower().Contains(arg.ToLower()));

            if (relevantRaces is null || !relevantRaces.Any(r => r.name.ToLower().Contains(arg.ToLower())))
            {
                await slashCommand.RespondAsync($"No races of the name `{arg}` were found; maybe you spelled it wrong?");
                return;
            }

            var embed = new EmbedBuilder()
            {
                Title = "Relevant race entries",
                Description = "Since there are likely multiple instances of the same race across separate publications, provided here is a list of all the ones I've found",
                Color = Color.Green,
                Author = new EmbedUserBuilder(slashCommand.User)
            }.WithCurrentTimestamp();

            foreach (Race race in relevantRaces)
            {
                embed.AddField(race?.name, $"**{race?.source}**p{race?.page}", true);
            }

            await slashCommand.RespondAsync(embed: embed.Build());
        }
    }
}