using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1.libs.LiftBots
{
	internal class BotNoQuestion : LiftBot
    {
		private List<string> phrases = new List<string>
        {
			"Ты что, случайно потерял свои дела? Почему общаешься со мной?",
	        "Зачем ты отвлекаешься на болтовню? Лучше займись своими делами.",
	        "Не мог бы ты заняться чем-то полезным? Мое время дорого.",
	        "Ты действительно думаешь, что я готов слушать бессмысленные разговоры? Иди займись чем-то другим.",
	        "Моя жизнь и так достаточно заполнена, чтобы тратить ее на бесполезные разговоры.",
	        "Если у тебя нет ничего важного сказать, то лучше уйди и не мешай.",
	        "Время - это деньги, и я не готов тратить его на пустые разговоры.",
	        "Пожалуйста, оставь меня в покое, если у тебя нет важных вопросов.",
	        "Время летит, и я не хочу его терять на бессмысленные разговоры.",
	        "Я не против общения, но только если оно имеет какой-то смысл. У нас же есть дела.",
		};

        private List<string> issueTag = new List<string> 
        {
			"Ваше задание: дойти до метки и вернуться обратно. Почувствуйте себя агентом 007, но без машины и бонда.",
			"Как скажете 'Абракадабра', так и дойдете до метки. Ну или хотя бы попробуйте.",
            "Сейчас вас ждет метка, и она такая, будто бы ее придумал котенок на клавиатуре.",
			"Вам нужно дойти до метки, как льву нужно дойти до своего обеда. Быстро и без разговоров!",
			"Ваша следующая метка - как спрятанная кладовка в доме без карты. Но я дам вам карту!",
			"Задание: идти к метке, не улетая на Луну. По крайней мере, не сегодня.",
			"Метка - ваш надежный спутник в мире заданий. Не заблудитесь, даже если захотите.",
			"Эта метка несет ответственность за вашу успешную навигацию. Пожалуйста, соблюдайте ее указания.",
			"Задание требует перемещения к метке. Помните, точное выполнение инструкций важно для успешного завершения миссии.",
			"Согласно вашей миссии, следуйте к метке, которая находится по координатам:."
		};

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