using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

public class CommandHandler
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
        _client.MessageReceived += HandleCommandAsync;
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
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