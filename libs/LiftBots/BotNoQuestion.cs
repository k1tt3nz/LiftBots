﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1.libs.LiftBots
{
	internal class BotNoQuestion : LiftBot
    {

        public BotNoQuestion(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }

        public override async Task Start()
        {
            TelegramBotClient Client = new TelegramBotClient(Token);
            Client.StartReceiving(Update, Error);
        }

        public override async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var userMsg = update.Message;
            var chatID = update.Message.Chat.Id;

			Random random = new Random();
            string botMsg = issueTag[random.Next(9)];
            if (userMsg.Text.Contains("/start"))
            {
                await botClient.SendTextMessageAsync(chatID, botMsg);
                await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
            }

			if (userMsg.Text != string.Empty && userMsg.Text != "/start")
			{
				await botClient.SendTextMessageAsync(chatID, phrases[random.Next(9)].ToString());
			}
		}

        public override string ReadBotTextFile()
        {
            using (StreamReader reader = new StreamReader(Path2Text))
            {
                string text = reader.ReadToEnd();
                return text;
            }
        }

        public override async Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}