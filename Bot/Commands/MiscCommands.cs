using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;

namespace NetBot.Bot.Commands
{
    //[Group("public")]
    public class MiscCommands : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        [Summary("Echoes a given phrase")]
        public async Task EchoAsync([Remainder][Summary("Phrase to echo")] string echo) => await ReplyAsync(echo);

        [Command("userinfo")]
        [Summary("Retrieves information about a given user")]
        [Alias("ui", "info", "i")]
        public async Task UserInfo([Summary("User to get info on")] SocketUser? user = null)
        {
            user = user ?? Context.Message.Author;
            var guildUser = Context.Guild.GetUser(user.Id);
            var roles = await Task.Run(
                () => guildUser.Roles.Select(
                    (role) => $"<@&{role.Id}>"
                ).ToArray()
            );
            var roles_joined = String.Join(", ", roles);

            var embed = new EmbedBuilder()
            .WithTitle("__User Info__")
            .WithDescription($"User info for {guildUser.Mention}")
            .AddField("Username", $"{user.Username}#{user.Discriminator}", true)
            .AddField("User ID", user.Id)
            .AddField("Created At", user.CreatedAt)
            .AddField("Joined At", guildUser.JoinedAt)
            .AddField("Roles", roles_joined, true)
            .AddField("Bot", user.IsBot, true)
            .AddField("Webhook", user.IsWebhook, true)
            .AddField("Badges", user.PublicFlags, true)
            .WithThumbnailUrl(user.GetAvatarUrl());

            await ReplyAsync(embed: embed.Build());
        }
    }
}