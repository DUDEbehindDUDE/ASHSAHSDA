using Discord;
using Discord.Net;
using Discord.WebSocket;
using NetBot.Lib.Attributes;
using NetBot.Lib.Types.JSON;
using Newtonsoft.Json;

namespace NetBot.Bot.Commands
{
    public class Races
    {
        [Description("Get information on a given D&D race THIS IS ALSO A VERY UNIQUE DESCRIPTION")]
        [Options("race", "The race to get information on", ApplicationCommandOptionType.String, true)]
        public static async void RaceInfo(SocketSlashCommand slashCommand)
        {
            string json = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "data", "races.json"));
            Root? races = JsonConvert.DeserializeObject<Root>(json);

            string arg = (string)slashCommand.Data.Options.First().Value;
            Race? race = races?.race.Find(r => r.name.ToLower() == arg.ToLower());

            await slashCommand.Channel.SendMessageAsync($"{race?.name ?? "?"} **Source:** {race?.source}p{race?.page}");
        }

        [Description("This is a test command")]
        [Options("test", "hehe", ApplicationCommandOptionType.String)]
        public static async void TestCommand(SocketSlashCommand slashCommand)
        {
            await slashCommand.Channel.SendMessageAsync($"The argument provided was: {slashCommand.Data.Options.First().Value}");
        }
    }
}