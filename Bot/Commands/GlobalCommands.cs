using Discord;
using Discord.Commands;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using log4net;
using NetBot.Bot.Services.Database;

namespace NetBot.Bot.Commands
{
    //[Group("admin")]
    public class GlobalCommands : ModuleBase<SocketCommandContext>
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        private readonly DatabaseConnector psql = new DatabaseConnector(5432, "admin", "localhost", "postgres", "netbot_dev");

        [Command("setglobalprefix")]
        [Summary("Set the bot's global prefix")]
        [Alias("sgp", "sp", "mp", "mgp", "modifyglobalprefix")]
        public async Task SetGlobalPrefixAsync([Remainder][Summary("Prefix to change to")] string prefix)
        {
            if (String.IsNullOrEmpty(prefix))
            {
                await ReplyAsync("No prefix given");
                return;
            }

            if (prefix.Length > 2)
            {
                await ReplyAsync("Prefix should not be longer than 2 characters");
                return;
            }

            await psql.ExecuteCommandAsync($"UPDATE netbot_dev SET prefix = '{prefix}' WHERE guild_id = '{Context.Guild.Id}'");

            await ReplyAsync("Prefix Updated");
        }

        [Command("eval")]
        [Summary("Execute code from a given string")]
        [Alias("e", "execute")]
        public async Task EvalAsync([Remainder][Summary("Code to execute")] string code)
        {
            Thread thread = new Thread(async () =>
            {
                try
                {
                    var rx = "^`+(cs)?|`+$";
                    var formattedCode = Regex.Replace(code, rx, "");
                    var scriptOptions = ScriptOptions.Default
                    .WithReferences(
                        typeof(DatabaseConnector).Assembly
                    )
                    .WithImports(
                        "System",
                        "System.Math",
                        "System.Text.RegularExpressions",
                        "NetBot.Bot.Services.Database"
                    );
                    var scriptGlobals = Context;

                    var result = await Task.Run(
                        () => CSharpScript.RunAsync(formattedCode, scriptOptions, scriptGlobals)
                    );

                    await ReplyAsync($"```cs\n{JsonConvert.SerializeObject(result.ReturnValue)}```");
                }
                catch (Exception e)
                {
                    await ReplyAsync(e.Message);
                }
            });

            thread.Start();

            await Context.Channel.TriggerTypingAsync();
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