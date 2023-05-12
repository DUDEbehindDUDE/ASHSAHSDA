using Discord;
using Discord.WebSocket;
using NetBot.Bot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBot.Bot.Commands.DND
{
    public class AgreeToTerms : ISlashCommand
    {
        public SlashCommandBuilder SlashCommandBuilder => new SlashCommandBuilder()
            .WithName("agreetoterms")
            .WithDescription("List the terms associated with using DND related commands, and choose to either accept or deny them");

        public ulong? Guild => null;

        public async Task CommandEvent(SocketSlashCommand command)
        {
            EmbedBuilder embed = new EmbedBuilder()
                .WithTitle("Terms")
                .WithDescription("Before you can use any commands related to DND, you must aknowledge that you already own or otherwise have access to the rulebooks, and any other content that may go along with them. To accept this, simply hit `I accept` below. If you hit `I do not accept`, and you previously accepted these terms, you will be unable to use any commands related to DND unless you accept these terms again.")
                .WithColor(Color.Gold);

            ComponentBuilder buttons = new ComponentBuilder()
                .WithButton(label: "I accept", customId: "accept", style: ButtonStyle.Success)
                .WithButton(label: "I do not accept", customId: "deny", style: ButtonStyle.Danger);

            await command.RespondAsync(embed: embed.Build(), components: buttons.Build(), ephemeral: true);
        }
    }
}
