using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using log4net;
using Newtonsoft.Json;
using NetBot.Lib.Types.JSON;


namespace NetBot.Bot.Services
{
    public class ComponentHandler
    {
        private DiscordSocketClient _client;
        private static readonly ILog log = LogManager.GetLogger(typeof(ComponentHandler));

        public ComponentHandler(DiscordSocketClient client)
        {
            _client = client;
            _client.SelectMenuExecuted += SelectMenuInteractionHandler;
        }

        public async Task SelectMenuInteractionHandler(SocketMessageComponent component)
        {
            //await component.DeferAsync();

            switch (component.Data.CustomId)
            {
                case "racemenu":
                case "racemenu2":
                    await RaceMenuInteractionHandler(component);
                    break;
            }
        }

        public async Task RaceMenuInteractionHandler(SocketMessageComponent component)
        {
            string? userChoice = component.Data.Values.First();
            int pipeIndex = userChoice.LastIndexOf("|");
            int hashIndex = userChoice.LastIndexOf("#");

            string raceName = "";
            string sourceBook = "";
            int optionNum = 0;

            for (int i = 0; i < userChoice.Length; i++)
            {
                if (i < pipeIndex)
                {
                    raceName += userChoice[i];
                }
                else if (i < hashIndex && i > pipeIndex)
                {
                    sourceBook += userChoice[i];
                }
                else
                {
                    optionNum = (int)Char.GetNumericValue(userChoice.Last());
                }
            }

            string json = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "data", "races.json"));
            Root? races = JsonConvert.DeserializeObject<Root>(json);
            Race? foundRace = races?.race.Find(r => r.name.ToLower() == raceName.ToLower() && r.source.ToLower() == sourceBook.ToLower());
            Subrace? foundSubrace = races?.subrace.Find
            (
                r =>
                {
                    if (r?.name is null || r.name.Length < 1) return false;
                    return (
                        r.name.ToLower() == raceName.ToLower() || r.raceName.ToLower() == raceName.ToLower()
                    ) && r.source.ToLower() == sourceBook.ToLower();
                }
            );

            if (foundRace?.name is null && foundSubrace?.name is null)
            {
                await component.RespondAsync("Sorry, I couldn't find that race. That means something probably went wrong. Tell us about it!");
                return;
            }

            var embed = new EmbedBuilder()
            {
                Title = foundRace?.name ?? foundSubrace?.name,
                Description = foundRace?.entries.First().ToString() ?? foundSubrace?.entries.First().ToString(),
                Color = Color.Blue,
                Author = new EmbedUserBuilder(component.User)
            }.WithCurrentTimestamp();

            await component.UpdateAsync(i =>
            {
                i.Embed = embed.Build();
            });
        }
    }
}
