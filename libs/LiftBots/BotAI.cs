using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1.libs.LiftBots
{
	internal class BotAI 
	{
		private static string _name = "Cognito Inc. AI";
		private static string _token = "6363452088:AAGP9kk_bAaSCnlrYIA9o5BCUIXbhlOAjvI";

		public async Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		public  string ReadBotTextFile()
		{
			throw new NotImplementedException();
		}

		public async Task Start()
		{
			TelegramBotClient Client = new TelegramBotClient(_token);
			Client.StartReceiving(Update, Error);
		}

		public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
		{
			var userMsg = update.Message;
			var chatID = update.Message.Chat.Id;
			if (userMsg.Text == "/start")
			{
				await botClient.SendTextMessageAsync(chatID, "Я" + " " + _name + "\n*приветственное сообщение и бла-бла-бла*");
				await botClient.SendTextMessageAsync(chatID, "Для дальнейшей работы вам следует авторизоваться.\n Введите вашу команду: Команда - \"Название команды\"");
			}

			if (userMsg.Text.ToLower().Contains("команда - "))
			{
				int hyphenIndex = userMsg.Text.IndexOf("-") + 2;
				string teamName = userMsg.Text.Substring(hyphenIndex);

				Guid guid = Guid.NewGuid();
				string text = $"Вы успешно зарегистрированны в системе.\n Ваш уникальный идентификатор:" + " " + guid;
				await botClient.SendTextMessageAsync(chatID, text);

				Team team = new Team(chatID, teamName);
				text = "Тестовая хуйня:\n" + team.TeamName + "\n" + team.ChatId;
				await botClient.SendTextMessageAsync(chatID, text);
			}
		}
	}
}
