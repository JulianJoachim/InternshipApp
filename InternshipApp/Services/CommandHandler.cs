// <copyright file="CommandHandler.cs" company="JAEMACOM GmbH">
// Copyright (c) JAEMACOM GmbH. All rights reserved.
// </copyright>

namespace InternshipApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Timers;
    using Discord;
    using Discord.Addons.Hosting;
    using Discord.Commands;
    using Discord.WebSocket;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Class responsible for handing commands and various events.
    /// </summary>
    public class CommandHandler : InitializedService
    {
        private readonly IServiceProvider provider;
        private readonly DiscordSocketClient client;
        private readonly CommandService service;
        private readonly IConfiguration configuration;
        private string currentSpeaker;
        private List<string> doneSpeaker;
        private System.Timers.Timer timer;
        private HashSet<string> maScrum = new HashSet<string>();
        private Scrum scrumMeeting;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IServiceProvider"/> that should be injected.</param>
        /// <param name="client">The <see cref="DiscordSocketClient"/> that should be injected.</param>
        /// <param name="service">The <see cref="CommandService"/> that should be injected.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> that should be injected.</param>
        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration configuration)
        {
            this.provider = provider;
            this.client = client;
            this.service = service;
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public override async Task InitializeAsync(CancellationToken cancellationToken)
        {
            this.client.MessageReceived += this.OnMessageReceived;
            this.service.CommandExecuted += this.OnCommandExecuted;
            this.client.ReactionAdded += this.OnReactionAdded;
            this.client.ReactionRemoved += this.OnReactionRemoved;

            await this.service.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), this.provider);
        }

        private async Task OnReactionRemoved(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel msgChannel, SocketReaction reaction)
        {
            if (!(reaction.User.ToString() == "InternshipBot#9757"))
            {
                this.maScrum.Remove(reaction.Message.ToString());
            }
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel msgChannel, SocketReaction reaction)
        {
            if (!(reaction.User.ToString() == "InternshipBot#9757") && (reaction.Emote.Name == "✅") && Modules.General.reactionMsgsId.Contains(reaction.MessageId))
            {
                this.maScrum.Add(reaction.Message.ToString());
            }

            if (!(reaction.User.ToString() == "InternshipBot#9757") && (reaction.Emote.Name == "➡️") && Modules.General.reactionMsgsId.Contains(reaction.MessageId))
            {
                await msgChannel.SendMessageAsync("Starte Scrum-Meeting.");

                this.scrumMeeting = new Scrum(this.maScrum, msgChannel);
                this.maScrum = new HashSet<string>();

                this.timer = new System.Timers.Timer(90000);
                this.timer.AutoReset = true;
                this.timer.Elapsed += this.OnTimedEvent;
                this.scrumMeeting.nextSpeaker();
                this.timer.Enabled = true;
            }

            if (reaction.User.ToString() == "InternshipBot#9757" && (reaction.Emote.Name == "⏩") && this.timer != null)
            {
                this.timer.Enabled = false;
                this.scrumMeeting.nextSpeaker();
                this.timer.Enabled = true;
            }
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            if (this.scrumMeeting.getScrumStatus() == true)
            {
                this.timer.Enabled = false;
            }
            else
            {
                this.scrumMeeting.nextSpeaker();

            }
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> commandInfo, ICommandContext commandContext, IResult result)
        {
            if (result.IsSuccess)
            {
                return;
            }

            await commandContext.Channel.SendMessageAsync(result.ErrorReason);
        }

        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message))
            {
                return;
            }

            if (message.Source != MessageSource.User)
            {
                return;
            }

            var argPos = 0;
            if (!message.HasStringPrefix(this.configuration["Prefix"], ref argPos) && !message.HasMentionPrefix(this.client.CurrentUser, ref argPos))
            {
                return;
            }

            var context = new SocketCommandContext(this.client, message);
            await this.service.ExecuteAsync(context, argPos, this.provider);
        }
    }
}
