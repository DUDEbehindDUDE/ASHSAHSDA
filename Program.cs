using Discord;
using Discord.WebSocket;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]

public class Program
{
    //private static readonly log4net.ILog log = log4net.LogManager.GetLogger("sexy");

    private DiscordSocketClient _client;
    public static Task Main(string[] args) => new Program().MainAsync();

	public async Task MainAsync()
	{
        _client = new DiscordSocketClient();

        _client.Log += Log;

        DotNetEnv.Env.Load("./../../../.env");
        var token = Environment.GetEnvironmentVariable("TOKEN");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

	private Task Log(LogMessage msg)
	{
		//log.Info(msg.ToString());
        Console.WriteLine(msg.ToString());
		return Task.CompletedTask;
	}
}