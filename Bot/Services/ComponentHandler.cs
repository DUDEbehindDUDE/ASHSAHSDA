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
using static NetBot.Bot.Services.DatabaseHandler;


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
            _client.ButtonExecuted += TermsInteractionHandler;
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

        public async Task TermsInteractionHandler(SocketMessageComponent component)
        {
            bool? acceptTerms = component.Data.CustomId switch
            {
                "accept" => true,
                "deny" => false,
                _ => null
            };
            log.Debug(acceptTerms);
            log.Debug(component.Data.CustomId);
            if (acceptTerms is null) return;

            var response = await UpdateDNDTerms(component.User.Id, (bool)acceptTerms);
            bool? previous = response.previous;
            bool? result = response.result;

            switch (result)
            {
                case null:
                    await component.RespondAsync("There was a problem contacting the database. Try again later.", ephemeral: true);
                    return;
                case bool b when b == previous:
                    await component.RespondAsync("Your response hasn't changed since last time.", ephemeral: true);
                    return;
                case true:
                    await component.RespondAsync("Thank you for agreeing to the terms. You can now execute any DND related command.", ephemeral: true);
                    return;
                case false:
                    await component.RespondAsync("Response updated. You will have to agree to the terms again to use any DND related commands.", ephemeral: true);
                    return;
            }
        }
    }
}
