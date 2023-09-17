using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace ConsoleApp1.libs.LiftBots.MysteryBots
{
	internal class SoyPorridge : LiftBot
	{
		public SoyPorridge(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }
		public SoyPorridge(LiftBot bot) : base(bot) { }

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
			string botMsg = "Я тут нашла интересный сайт, но у них стоит анти-бот система... Будь дорб помоги мне, а я расскажу где твое следующие задание =)";

			if (userMsg.Text.Contains("start"))
			{
				await botClient.SendTextMessageAsync(chatID, botMsg);
				await botClient.SendPhotoAsync(
				   chatId: chatID,
				   photo: InputFile.FromUri("https://github.com/k1tt3nz/LiftBots/blob/master/libs/LiftBots/MysteryBots/%D0%BA%D0%B0%D0%BF%D1%87%D0%B01.png?raw=true"),
				   parseMode: ParseMode.Html
			   );
			}

			if (userMsg.Text.Contains("w68hp"))
			{
				await botClient.SendPhotoAsync(
				   chatId: chatID,
				   photo: InputFile.FromUri("https://github.com/k1tt3nz/LiftBots/blob/master/libs/LiftBots/MysteryBots/%D0%BA%D0%B0%D0%BF%D1%87%D0%B03.jpg?raw=true"),
				   parseMode: ParseMode.Html
			   );
			}

			if (userMsg.Text.Contains("76447"))
			{
				await botClient.SendPhotoAsync(
				   chatId: chatID,
				   photo: InputFile.FromUri("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCw24AanNXIK54g1SWtbnGed5utKfW_eY_Zg&usqp=CAU"),
				   parseMode: ParseMode.Html
			   );
			}

			if (userMsg.Text.Contains("p5np"))
			{
				await botClient.SendPhotoAsync(
				   chatId: chatID,
				   photo: InputFile.FromUri("https://github.com/k1tt3nz/LiftBots/blob/master/libs/LiftBots/MysteryBots/kapcha-rofl.jpg?raw=true"),
				   parseMode: ParseMode.Html
			   );
				Thread.Sleep(60000);
				await botClient.SendTextMessageAsync(chatID, "Неужиели так сложно");

				Thread.Sleep(15000);
				await botClient.SendTextMessageAsync(chatID, "ДУМАЙ ЧЕЛОВЕК ДУМАЙ");
				await botClient.SendPhotoAsync(
				   chatId: chatID,
				   photo: InputFile.FromUri("https://i.ytimg.com/vi/nrUM70l9qlg/maxresdefault.jpg"),
				   parseMode: ParseMode.Html
				);

				Thread.Sleep(60000);
				await botClient.SendTextMessageAsync(chatID, "Я пошутила, просто, решила посмотреть на сколько вы сильны в математике");
				await botClient.SendTextMessageAsync(chatID, issueTag[random.Next(9)]);
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}

			if (userMsg.Text != string.Empty && userMsg.Text != "/start" && userMsg.Text != "w68hp" && userMsg.Text != "76447" && userMsg.Text != "p5np")
			{
				await botClient.SendTextMessageAsync(chatID, ifWrongAnswer[random.Next(9)]);
			}

			if (userMsg.Text.ToLower().Contains("ответ"))
			{
				await botClient.SendTextMessageAsync(chatID, "СДК");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}
		}
	}
}
