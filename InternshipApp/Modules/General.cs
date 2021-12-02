// <copyright file="General.cs" company="JAEMACOM GmbH">
// Copyright (c) JAEMACOM GmbH. All rights reserved.
// </copyright>

namespace InternshipApp.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    /// <summary>
    /// Defines the commands that are available in the bot.
    /// </summary>
    public class General : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// A Command that will respond with pong.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("ping")]
        [Alias("p")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task PingAsync()
        {
            await this.Context.Channel.TriggerTypingAsync();
            await this.Context.Channel.SendMessageAsync("Pong!");
        }

        /// <summary>
        /// Returns Information on given User or if without argument for the user calling the command.
        /// </summary>
        /// <param name="socketGuildUser">An optional user to get the information from.</param>
        /// <returns> A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("info")]
        public async Task InfoAsync(SocketGuildUser socketGuildUser = null)
        {
            if (socketGuildUser == null)
            {
                socketGuildUser = this.Context.User as SocketGuildUser;
            }

            await this.ReplyAsync($"ID: {socketGuildUser.Id}\n" +
                $"Name: {socketGuildUser.Username}#{socketGuildUser.Discriminator}\n" +
                $"Created at: {socketGuildUser.CreatedAt}");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsBB")]
        public async Task DetailsBBAsync()
        {
            await this.ReplyAsync("Name: Bogdana Bondarenko\n" +
                "Position: \n" +
                "Im Unternehmen seit: ");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsDM")]
        public async Task DetailsDMAsync()
        {
            await this.ReplyAsync("Name: David Maciejewski\n" +
                "Position: Backend-Entwicklung\n" +
                "Im Unternehmen seit: 5 Jahren");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsAC")]
        public async Task DetailsACAsync()
        {
            await this.ReplyAsync("Name: Ana Caballero\n" +
                "Position: \n" +
                "Im Unternehmen seit: ");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsJH")]
        public async Task DetailsJHAsync()
        {
            await this.ReplyAsync("Name: Jens-Christian Hübner\n" +
                "Position: Leiter Softwareabteilung\n" +
                "Im Unternehmen seit: Urgestein");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsMW")]
        public async Task DetailsMWAsync()
        {
            await this.ReplyAsync("Name: Maximilian Wache\n" +
                "Position: Praktikant\n" +
                "Im Unternehmen seit: 4 Monaten ");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsAH")]
        public async Task DetailsAHAsync()
        {
            await this.ReplyAsync("Name: Andreas Heidrich\n" +
                "Position: freier Mitarbeiter\n" +
                "Im Unternehmen seit: 22.11.2021");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsSH")]
        public async Task DetailsSHAsync()
        {
            await this.ReplyAsync("Name: Sina Heidrich\n" +
                "Position: Werksstudentin\n" +
                "Im Unternehmen seit: ");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsDW")]
        public async Task DetailsDWAsync()
        {
            await this.ReplyAsync("Name: David Wasilewko\n" +
                "Position: \n" +
                "Im Unternehmen seit: ");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsJJ")]
        public async Task DetailsJJAsync()
        {
            await this.ReplyAsync("Name: Julian Joachim\n" +
                "Position: Praktikant\n" +
                "Im Unternehmen seit: 01.12.2021");
        }

        /// <summary>
        /// Eine Methode die den Bot  Informationen über den Mitarbeiter ausgeben lässt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("detailsJR")]
        public async Task DetailsJRAsync()
        {
            await this.ReplyAsync("Name: Janet Richter\n" +
                "Position: Softwaretesterin\n" +
                "Im Unternehmen seit: Mai 2021");
        }

        [Command("startScrum")]
        [Alias("ss", "scrum")]
        [RequireBotPermission(GuildPermission.AddReactions)]

        public async Task StartScrum()
        {
            var maList = new List<string>()
                    {
                        "Bogdana Bondarenko",
                        "David Maciejewski",
                        "Ana Caballero",
                        "Jens-Christian Hübner",
                        "Maximilian Wache",
                        "Andreas Heidrich",
                        "Sina Heidrich",
                        "David Wasilewko",
                        "Julian Joachim",
                        "Janet Richter"
                    };

            await this.ReplyAsync("Scrum-Meeting wird gestartet...\n" +
                "Bitte wählen Sie die Teilnehmer durch Reaktion auf dessen Namen aus!");

            for(int i = 0; i < maList.Count; i++)
            {
                var sentLoop = await this.ReplyAsync($"{maList[i]}");
                await sentLoop.AddReactionAsync(new Emoji("✅"));
            }

            var sent = await this.ReplyAsync("Reagiere auf den Haken unter dieser Nachricht um das Scrum Meeting zu starten.");
            await sent.AddReactionAsync(new Emoji("➡️"));
        }

        [Command("nm")]
        [Alias("next", "nächstes mitglied")]
        public async Task NaechstesMitglied()
        {
            var sent = await this.ReplyAsync("Skippe zum Nächsten Teilnehmer...");
            await sent.AddReactionAsync(new Emoji("⏩"));
        }

    }
}
