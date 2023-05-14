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
        private readonly DiscordSocketClient _client;
        private static readonly ILog log = LogManager.GetLogger(typeof(ComponentHandler));

        public ComponentHandler(DiscordSocketClient client)
        {
            _client = client;
            _client.SelectMenuExecuted += SelectMenuInteractionHandler;
            _client.ButtonExecuted += TermsInteractionHandler;
        }

        public static async Task SelectMenuInteractionHandler(SocketMessageComponent component)
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

        public static async Task RaceMenuInteractionHandler(SocketMessageComponent component)
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
