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
			string botMsg = "Йо-хо-хо и ящик микросхем!\n" +
				"Сейчас вы отправитесь в путешествие по-нашему исследовательскому кампусу.";

			string questStr = "Ты отыщи огромный куб посередь площади,\r\n" +
				"На кубе том в углу ты метку (XATEM 2015) рассмотри,\r\n" +
				"И от угла ступай ты прямо до угла (190град) УК4,\r\n" +
				"А от угла ступай по красной ты дорого, \r\n" +
				"Где на конце найдешь тропу (северо-запад),\r\n" +
				"По ней иди ты, огибая всякий дом,\r\n" +
				"Но не сходя с пути.\r\n" +
				"В конце пути – увидишь белый мрамор, \r\n" +
				"К нему придя, взгляни в окно из камня\r\n" +
				"Там буду я.\r\n";

			if (userMsg.Text.Contains("start"))
			{
				await botClient.SendTextMessageAsync(chatID, botMsg);
				await botClient.SendTextMessageAsync(chatID, questStr);

				Thread.Sleep(5000);

				await botClient.SendTextMessageAsync(chatID, "Ах да, никаких подсказок не ждите и даже не надейтесь на чью-либо помощь. " +
					"У вас есть только ВЫ! А я пока пойду посмотрю новую часть Пиратов Карибского моря. УДАЧИ)))");

				Thread.Sleep(5000);
				await botClient.SendTextMessageAsync(chatID, "Надесь, вы уже додумалсь и смогл найти этап, иначе вот вам метка...");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}
		}
	}
}