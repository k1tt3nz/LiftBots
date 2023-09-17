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
	internal class AlexeiBelan : LiftBot
	{
		private List<string> ifWrongAnswer = new List<string> {
			"Нет, это совершенно неверно.",
			"Попробуй ещё раз, твой ответ неправильный.",
			"Увы, ты ошибся.",
			"Это не то, что я ожидала услышать.",
			"Правильный ответ звучит иначе.",
			"Ты в далеке от правильного ответа.",
			"Ты не попал в цель, продолжай искать.",
			"Неправильно, но это хорошая попытка.",
			"Этот ответ не соответствует действительности."};

		public AlexeiBelan(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }
		public AlexeiBelan(LiftBot bot) : base(bot) { }

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
			string botMsg = "Давай думай)";
			if (userMsg.Text.Contains("start"))
			{
				await botClient.SendTextMessageAsync(chatID, botMsg);
				await botClient.SendPhotoAsync(
					chatId: chatID,
					photo: InputFile.FromUri("https://github.com/k1tt3nz/LiftBots/blob/master/libs/LiftBots/MysteryBots/dRZ19Q-T2QA.jpg?raw=true"),
					parseMode: ParseMode.Html
				);
			}

			if (userMsg.Text.ToLower().Contains("хакер"))
			{
				await botClient.SendTextMessageAsync(chatID, "\"Хакеры - современные цифровые археологи, раскрывающие тайны виртуальных миров и " +
					"исследующие края интернета, которые остаются скрытыми от большинства.");
				await botClient.SendTextMessageAsync(chatID, "А вот и твоя меточка, давай шуруй человек АХАХВ...ой...");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}

			if (userMsg.Text != string.Empty && userMsg.Text != "/start" && userMsg.Text != "хакер")
			{
				Random random = new Random();
				await botClient.SendTextMessageAsync(chatID, ifWrongAnswer[random.Next(9)].ToString());
			}

			if (userMsg.Text.ToLower().Contains("ответ"))
			{
				await botClient.SendTextMessageAsync(chatID, "СДК");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}
		}
	}
}
