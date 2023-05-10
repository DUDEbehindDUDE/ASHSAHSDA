using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Interactions;
using Discord.API;
using NetBot.Bot.Services;
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

        public async Task CommandEvent(SocketSlashCommand slashCommand)
        {
            string json = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "data", "races.json"));
            Root? races = JsonConvert.DeserializeObject<Root>(json);
            List<Subrace>? subraces = races?.subrace;


            string arg = (string)slashCommand.Data.Options.First().Value;
            Race? race = races?.race.Find(r => r.name.ToLower().Contains(arg.ToLower()));

            // there should probably be a more robust check for this in the future, plenty of valid races return no results as it is
            IEnumerable<Race>? relevantRaces = races?.race.Where(r => r.name.ToLower().Contains(arg.ToLower()));
            IEnumerable<Subrace>? relevantSubraces = subraces?.Where(r => r.raceName.ToLower().Contains(arg.ToLower()));

            if (relevantRaces is null || !relevantRaces.Any(r => r.name.ToLower().Contains(arg.ToLower())))
            {
                await slashCommand.RespondAsync($"No races of the name `{arg}` were found; maybe you spelled it wrong?");
                return;
            }

            EmbedBuilder embed = new EmbedBuilder()
            {
                Title = "Relevant race entries",
                Description = "Since there are likely multiple instances of the same race across separate publications, provided here is a list of all the ones I've found",
                Color = Color.Green,
                Author = new EmbedUserBuilder(slashCommand.User)
            }.WithCurrentTimestamp();

            SelectMenuBuilder menuBuilder = new SelectMenuBuilder()
                .WithPlaceholder("Select a source for the race")
                .WithCustomId("racemenu");

            EmbedBuilder? embed2 = default;
            SelectMenuBuilder? menuBuilder2 = default;


            int acc = 1;

            if (relevantSubraces is not null)
            {
                foreach (Subrace subrace in relevantSubraces)
                {
                    string name = subrace.name ?? subrace.raceName;

                    if (acc >= 25)
                    {
                        embed2 = new EmbedBuilder()
                        {
                            Title = "Relevant race entries",
                            Color = Color.Green,
                            Author = new EmbedUserBuilder(slashCommand.User)
                        }.WithCurrentTimestamp();

                        menuBuilder2 = new SelectMenuBuilder()
                            .WithPlaceholder("Select a source for the race")
                            .WithCustomId("racemenu2");

                        embed2.AddField(name, $"**{subrace.source}** (p. {subrace.page})", true);
                        menuBuilder2.AddOption($"{acc}. {name} ({subrace.source})", $"{name}|{subrace.source.ToLower()}#{acc}", $"The {name} race as published in the {subrace.source}");

                        acc++;
                        break;
                    };

                    embed.AddField(name, $"**{subrace.source}** (p. {subrace.page})", true);
                    menuBuilder.AddOption($"{acc}. {name} ({subrace.source})", $"{name}|{subrace.source.ToLower()}#{acc}", $"The {name} race as published in the {subrace.source}");
                    acc++;
                }
            }

            foreach (Race r in relevantRaces)
            {
                if (acc >= 25)
                {
                    embed2?.AddField(r?.name, $"**{r?.source}** (p. {r?.page})", true);
                    menuBuilder2?.AddOption($"{acc}. {r?.name} ({r?.source})", $"{r?.name}|{r?.source.ToLower()}#{acc}", $"The {r?.name} race as published in the {r?.source}");
                    acc++;
                    break;
                };

                embed.AddField(r?.name, $"**{r?.source}** (p. {r?.page})", true);
                menuBuilder.AddOption($"{acc}. {r?.name} ({r?.source})", $"{r?.name}|{r?.source.ToLower()}#{acc}", $"The {r?.name} race as published in the {r?.source}");
                acc++;
            }

            ComponentBuilder component = new ComponentBuilder()
                .WithSelectMenu(menuBuilder);

            bool isDefault = EqualityComparer<SelectMenuBuilder>.Default.Equals(menuBuilder2);
            bool isNull = menuBuilder2 is null;

            if (!isDefault && !isNull)
            {
                component.WithSelectMenu(menuBuilder2);
            }

            await slashCommand.RespondAsync(
                embeds: (embed2 is null || embed2.Color != Color.Green) ? new Embed[] { embed.Build() } : new Embed[] { embed.Build(), embed2.Build() },
                components: component.Build(),
                ephemeral: true
            );

        }
    }
}