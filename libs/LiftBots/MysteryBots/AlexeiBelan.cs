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
			string botMsg = "Короче," + update.Message.Chat.FirstName + ", я тебя спас и в благородство играть не буду: отгадаешь для меня пару знаменитостей — " +
				"и мы в расчете. Заодно посмотрим, как быстро у тебя башка после дистанционного обучения прояснится. " +
				"А по твоей теме постараюсь разузнать. Хрен его знает, на кой ляд тебе эта Джиджи Томпсон сдался, но я в чужие дела не лезу, хочешь квестики выполнять, твои дела...";

			if (userMsg.Text.Contains("start"))
			{
				await botClient.SendTextMessageAsync(chatID, botMsg);
				await botClient.SendPhotoAsync(
					chatId: chatID,
					photo: InputFile.FromUri("https://ya.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fcdn.nur.kz%2Fimages%2F1120%2Fpogudx2u6fbe1b3pv.jpeg&lr=4&pos=0&rpt=simage&text=%D0%B1%D1%83%D0%B7%D0%BE%D0%B2%D0%B0"),
					parseMode: ParseMode.Html
				);
			}

			if (userMsg.Text != string.Empty)
			{
				Random random = new Random();
				await botClient.SendTextMessageAsync(chatID, ifWrongAnswer[random.Next(9)].ToString());
			}
		}
	}
}
