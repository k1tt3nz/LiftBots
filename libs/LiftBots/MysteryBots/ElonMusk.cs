using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ConsoleApp1.libs.LiftBots.MysteryBots
{
	internal class ElonMusk : LiftBot
	{
		public ElonMusk(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }
		public ElonMusk(LiftBot bot) : base(bot) { }

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
			string botMsg = "Я - загадка, позвольте мне развлечься, О человеке с бизнесом в голове. " +
				"Эта фигура - не обычное зрелище. В его имени скрыт небесный намек, " +
				"О красных, пыльных равнинах и миссиях, которые сверкают. " +
				"Гений с видениями, устремленными в небо, " +
				"Он мечтает путешествовать там, где летают звезды. " +
				"Смелые авантюры и прорывы, В электромобилях и ракетах. " +
				"Его Tesla - это чудо, бесшумное на улице, " +
				"А SpaceX устремляется в бесконечные дали. " +
				"Так скажи мне, мой друг, можешь ли ты сделать вывод? " +
				"Ответ на загадку, не имея оправданий? " +
				"Ищи возле БИЗНЕС ИНКУБАТОРА, где бизнес ведется, " +
				"И ты найдешь ЕГО, человека который заново создает заголовки.";

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
				await botClient.SendTextMessageAsync(chatID, "Они придут сюда без метки по заданию выше\r Лавочка возле бизнес инкубатора");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}
		}
	}
}
