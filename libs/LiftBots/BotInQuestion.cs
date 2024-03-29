﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1.libs.LiftBots
{
	public partial class BotInQuestion : LiftBot
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

        public override async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
			var userMsg = update.Message;
			var chatID = update.Message.Chat.Id;
			string botMsg = ReadBotTextFile();
			if (userMsg.Text != "start")
			{
				await botClient.SendTextMessageAsync(chatID, "Бот с вопросом");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}
		}
    }
}