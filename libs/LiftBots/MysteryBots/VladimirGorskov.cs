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
	internal class VladimirGorskov : LiftBot
	{
		public VladimirGorskov(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }
		public VladimirGorskov(LiftBot bot) : base(bot) { }

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
			string botMsg = "Кажется или эти дома похожи на буквы, но я не могу понять что они означают помогите решить мне данную проблему, людишки";
			if (userMsg.Text.Contains("start"))
			{
				await botClient.SendTextMessageAsync(chatID, botMsg);

				//Буква Т
				await botClient.SendPhotoAsync(
					chatId: chatID,
					photo: InputFile.FromUri("https://github.com/k1tt3nz/LiftBots/blob/master/libs/LiftBots/MysteryBots/%D0%B1%D1%83%D0%BA%D0%B2%D0%B0_%D0%A2.jpg?raw=true"),
					parseMode: ParseMode.Html
				);

				//Буква Л
				await botClient.SendPhotoAsync(
					chatId: chatID,
					photo: InputFile.FromUri("https://github.com/k1tt3nz/LiftBots/blob/master/libs/LiftBots/MysteryBots/%D0%B1%D1%83%D0%BA%D0%B2%D0%B0_%D0%9B.jpg?raw=true"),
					parseMode: ParseMode.Html
				);

				await botClient.SendTextMessageAsync(chatID, "*ERROR: данные поврежденны.");

				//Буква Ф
				await botClient.SendPhotoAsync(
					chatId: chatID,
					photo: InputFile.FromUri("https://github.com/k1tt3nz/LiftBots/blob/master/libs/LiftBots/MysteryBots/%D0%B1%D1%83%D0%BA%D0%B2%D0%B0_%D0%A4.jpg?raw=true"),
					parseMode: ParseMode.Html
				);
			}

			if (userMsg.Text.ToLower().Contains("лифт"))
			{
				await botClient.SendTextMessageAsync(chatID, "А вы способнее чем я предпологала.");
				await botClient.SendTextMessageAsync(chatID, issueTag[random.Next(9)]);
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}

			if (userMsg.Text != string.Empty && userMsg.Text != "/start" && userMsg.Text != "лифт")
			{
				await botClient.SendTextMessageAsync(chatID, ifWrongAnswer[random.Next(9)].ToString());
			}
		}
	}
}
