using Discord;
using Discord.Commands;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Text.RegularExpressions;
using NetBot.Bot.Services;
using Newtonsoft.Json;
using log4net;

namespace NetBot.Bot.Commands
{
    public class GlobalCommands : ModuleBase<SocketCommandContext>
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

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

            var result = await Task.Run(
                () => CSharpScript.RunAsync(code, globals: Context.Client)
            );

            await ReplyAsync($"```cs\n{JsonConvert.SerializeObject(result.ReturnValue)}```");
        }

        private Task Log(LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    log.Fatal(msg.Message, msg.Exception);
                    break;

                case LogSeverity.Error:
                    log.Error(msg.Message, msg.Exception);
                    break;

                case LogSeverity.Warning:
                    log.Warn(msg.Message, msg.Exception);
                    break;

                case LogSeverity.Info:
                    log.Info(msg.Message, msg.Exception);
                    break;

                default:
                    log.Debug(msg.Message, msg.Exception);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}