using Discord.Commands;

namespace NetBot.Bot.Commands
{
    public class ServerCommands : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        [Summary("Echoes a given phrase")]
        public async Task EchoAsync([Remainder][Summary("Phrase to echo")] string echo) => await ReplyAsync(echo);
    }
}