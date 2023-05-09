using Discord;
using Discord.WebSocket;

namespace NetBot.Bot.Services
{
    public class EmbedUserBuilder : EmbedAuthorBuilder
    {
        public EmbedUserBuilder(SocketUser user)
        {
            Name = user.Username;
            IconUrl = user.GetAvatarUrl();
        }
    }
}