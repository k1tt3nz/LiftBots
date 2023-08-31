using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1.libs.LiftBots
{
	public class BotInQuestion : LiftBot
    {
        public BotInQuestion(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }

        public override Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override string ReadBotTextFile()
        {
            using (StreamReader reader = new StreamReader(Path2Text))
            {
                string text = reader.ReadToEnd();
                return text;
            }
        }

        public override async Task Start()
        {
            TelegramBotClient Client = new TelegramBotClient(Token);
            Client.StartReceiving(Update, Error);
        }

        public override Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public abstract class BotCreator
        {
            public abstract LiftBot Create(string type, string name, string token, Geolocation geolocation, string path2Text);
        }

        public class BotNoQuestionCreator : BotCreator
        {
            public override LiftBot Create(string type, string name, string token, Geolocation geolocation, string path2Text)
            {
                return new BotNoQuestion(type, name, token, geolocation, path2Text);
            }
        }

        public class BotInQuestionCreator : BotCreator
        {
            public override LiftBot Create(string type, string name, string token, Geolocation geolocation, string path2Text)
            {
                return new BotInQuestion(type, name, token, geolocation, path2Text);
            }
        }
    }
}