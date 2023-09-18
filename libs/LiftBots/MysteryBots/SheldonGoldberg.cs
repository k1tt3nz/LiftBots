using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ConsoleApp1.libs.LiftBots.MysteryBots
{
	internal class SheldonGoldberg : LiftBot
	{
		public SheldonGoldberg(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }
		public SheldonGoldberg(LiftBot bot) : base(bot) { }

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
			string botMsg = "Назовите самого популярного Рептилоида,\r\nЧьё имя в сети мир создаёт:\r\nОн социальные сети ведёт,\r\nИ в вас все ваши данные знает.";
			if (userMsg.Text.Contains("start"))
			{
				await botClient.SendTextMessageAsync(chatID, botMsg);
			}

			if (userMsg.Text.ToLower().Contains("цукенберг") || userMsg.Text.ToLower().Contains("цукерберг"))
			{
				await botClient.SendTextMessageAsync(chatID, "А вы способнее чем я предпологала.");
				await botClient.SendTextMessageAsync(chatID, issueTag[random.Next(9)]);
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}

			if (userMsg.Text != string.Empty && userMsg.Text != "/start" && !userMsg.Text.ToLower().Contains("цукерберг"))
			{
				await botClient.SendTextMessageAsync(chatID, ifWrongAnswer[random.Next(9)].ToString());
			}

			if (userMsg.Text.ToLower().Contains("ответ"))
			{
				await botClient.SendTextMessageAsync(chatID, "\r Под переходом в ГУК");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}
		}
	}
}
