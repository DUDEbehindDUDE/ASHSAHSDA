using Discord.Commands;
using NetBot.Bot.Services.Database;
namespace NetBot.Bot.Commands
{
    //[Group("moderation")]
    public class ServerCommands : ModuleBase<SocketCommandContext>
    {
        [Command("modifymemberpermissions")]
        [Summary("Modify a specific server members bot permissions")]
        [Alias("mmp", "permissionsconfig", "memberpermissionsconfig", "mpc")]
        public async Task ModifyMemberPermissionsAsync()
        {

        }

        [Command("modifyserverprefix")]
        [Summary("Change the command prefix for this server")]
        [Alias("msp")]
        public async Task ModifyServerPrefix()
        {

        }
    }
}