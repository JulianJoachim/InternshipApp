namespace InternshipApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Discord.WebSocket;

    internal class Scrum
    {
        private static readonly Random random = new Random();
        private List<string> doneSpeaker = new List<string>();
        private HashSet<string> maScrum = new HashSet<string>();
        private string currentSpeaker = string.Empty;
        private long startTime;
        private bool scrumFinished;
        private ISocketMessageChannel msgChannel;

        /// <summary>
        /// Initializes a new instance of the <see cref="scrum"/> class.
        /// </summary>
        /// <param name="doneSpeaker"></param>
        /// <param name="maScrum"></param>
        /// <param name="currentSpeaker"></param>
        public Scrum(List<string> doneSpeaker, HashSet<string> maScrum, string currentSpeaker, ISocketMessageChannel msgChannel)
        {
            this.doneSpeaker = doneSpeaker;
            this.maScrum = maScrum;
            this.currentSpeaker = currentSpeaker;
            this.msgChannel = msgChannel;
            this.startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            this.scrumFinished = false;
        }

        public Scrum(HashSet<string> maScrum, ISocketMessageChannel msgChannel)
        {
            this.maScrum = maScrum;
            this.msgChannel = msgChannel;
            this.startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            this.scrumFinished = false;
        }

        public async void nextSpeaker()
        {
            if (this.maScrum.Count == this.doneSpeaker.Count)
            {
                msgChannel.SendMessageAsync($"Scrum beendet. Gesamtdauer: {(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTime) / 1000} Sekunden.\n" +
                    "**Auf einen guten Arbeitstag!**");
                this.scrumFinished = true;

            }
            else
            {
                this.setNextSpeaker();
                await this.msgChannel.SendMessageAsync($"Es ist {this.currentSpeaker} an der Reihe! Du hast 90 Sekunden Zeit.");
                this.doneSpeaker.Add(this.currentSpeaker);
            }




        }

        public async void setNextSpeaker()
        {
            do
            {
                string speaker = this.maScrum.ElementAt(random.Next(this.maScrum.Count));
                if (!this.doneSpeaker.Contains(speaker))
                {
                    this.currentSpeaker = speaker;
                    break;
                }
            } while (true);
        }

        public bool getScrumStatus()
        {
            return this.scrumFinished;
        }

    }
}
