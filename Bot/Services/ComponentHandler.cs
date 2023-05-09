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
            await component.DeferAsync();

            switch (component.Data.CustomId)
            {
                case "racemenu":
                    await RaceMenuInteractionHandler(component);
                    break;
            }
        }

        public async Task RaceMenuInteractionHandler(SocketMessageComponent component)
        {
            string? userChoice = component.Data.Values.First();
            int pipeIndex = userChoice.IndexOf("|");
            int dashIndex = userChoice.LastIndexOf("-");

            string raceName = "";
            string sourceBook = "";
            int optionNum = 0;

            for (int i = 0; i < userChoice.Length; i++)
            {
                if (i < pipeIndex)
                {
                    raceName += userChoice[i];
                }
                else if (i < dashIndex && i > pipeIndex)
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

            log.Debug($"userChoice VALUE: {userChoice}");
            log.Debug($"raceName VALUE: {raceName}");
            log.Debug($"sourceBook VALUE: {sourceBook}");
            log.Debug($"optionNum VALUE: {optionNum}");

            if (foundRace?.name is null)
            {
                await component.Channel.SendMessageAsync("Sorry, something went wrong. Try again later.");
            }

            var embed = new EmbedBuilder()
            {
                Title = foundRace?.name,
                Description = foundRace?.entries.First().ToString(),
                Color = Color.Blue,
                Author = new EmbedUserBuilder(component.User)
            }.WithCurrentTimestamp();

            await component.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
