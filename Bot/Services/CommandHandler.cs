using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using NetBot;

public class CommandHandler : NetBot.Program
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly char _prefix;

    public CommandHandler(DiscordSocketClient client, CommandService commands, char prefix)
    {
        _commands = commands;
        _client = client;
        _prefix = prefix;
    }

    public async Task InstallCommandsAsync()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        _client.MessageReceived += HandleCommandAsync;
        _commands.CommandExecuted += OnCommandExecutedAsync;
    }

    public async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
    {
        if (!String.IsNullOrEmpty(result?.ErrorReason))
        {
            await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        var author = context.Message.Author;

        var commandName = command.IsSpecified ? command.Value.Name : "Unknown Command";
        await Log(new LogMessage(LogSeverity.Info, "CommandExecution",
            $"{commandName} was executed at {DateTime.UtcNow}. Executed by {author.Username}#{author.Discriminator}({context.Message.Author.Id})"
        ));
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;

        if (message == null)
            return;

        int argPos = 0;

        if (!(message.HasCharPrefix(_prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
            return;

        var context = new SocketCommandContext(_client, message);

        await _commands.ExecuteAsync(
            context: context,
            argPos: argPos,
            services: null);
    }
}