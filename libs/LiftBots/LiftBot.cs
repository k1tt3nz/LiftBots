using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1.libs.LiftBots
{
	public abstract class LiftBot
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public Geolocation Coordinates { get; set; }
        public string Path2Text { get; set; }

        public LiftBot(string type, string name, string token, Geolocation geolocation, string path2Text)
        {
            Type = type;
            Name = name;
            Token = token;
            Coordinates = geolocation;
            Path2Text = path2Text;
        }

        public abstract Task Start();
        public abstract Task Update(ITelegramBotClient botClient, Update update, CancellationToken token);
        public abstract Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token);
        public abstract string ReadBotTextFile();
    }
}