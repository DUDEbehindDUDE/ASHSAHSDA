using Discord;
using Discord.Commands;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Text.RegularExpressions;
using NetBot.Bot.Services;
using Newtonsoft.Json;

namespace NetBot.Bot.Commands
{
    //[Group("admin")]
    public class GlobalCommands : ModuleBase<SocketCommandContext>
    {
        [Command("setglobalprefix")]
        [Summary("Set the bot's global prefix")]
        [Alias("sgp", "sp", "mp", "mgp", "modifyglobalprefix")]
        public async Task SetGlobalPrefixAsync([Remainder][Summary("Prefix to change to")] char prefix)
        {
            await ReplyAsync("ok");

        }

        [Command("eval")]
        [Summary("Execute code from a given string")]
        [Alias("e", "execute")]
        public async Task EvalAsync([Remainder][Summary("Code to execute")] string code)
        {
            var rx = "^[`]+(cs)?|[`]+$";
            var formattedCode = Regex.Replace(code, rx, "");
            var scriptOptions = ScriptOptions.Default.WithImports(
                "System",
                "System.Text.RegularExpressions",
                "System.Math"
            );
            var scriptGlobals = Context;

            var result = await Task.Run(
                () => CSharpScript.RunAsync(formattedCode, scriptOptions, scriptGlobals)
            );

            await ReplyAsync($"```cs\n{JsonConvert.SerializeObject(result.ReturnValue)}```");
        }

        private Task Log(LogMessage msg)
        {
            if (msg.Exception != null)
            {
                Console.WriteLine(msg.Exception.Message);
            }

            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}