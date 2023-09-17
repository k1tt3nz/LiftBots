using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ConsoleApp1.libs.LiftBots.MysteryBots.GigiThompsonBOT
{
	internal class Robotus : LiftBot
	{
		public Robotus(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }
		public Robotus(LiftBot bot) : base(bot) { }

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
			Random random = new Random();
			string botMsg = "Есть начальная точка с которой команда начинает идти к этапу ориентируясь по компасу (в любом телефоне он есть) " +
				"\r\nНаправление и кол-во метров и снова пока не дойдут (Ща пойду координаты строить)";

			if (userMsg.Text.Contains("start"))
			{
				await botClient.SendTextMessageAsync(chatID, botMsg);
			}

			if (userMsg.Text != string.Empty && userMsg.Text != "/start")
			{
				await botClient.SendTextMessageAsync(chatID, phrases[random.Next(9)]);
			}

			if (userMsg.Text.ToLower().Contains("ответ"))
			{
				await botClient.SendTextMessageAsync(chatID, "Они придут сюда без метки по заданию выше");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}
		}
	}
}
