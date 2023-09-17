using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleApp1.libs.LiftBots
{
	public abstract class LiftBot
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public Geolocation Coordinates { get; set; }
        public string Path2Text { get; set; }

		public List<string> ifWrongAnswer = new List<string> {
			"Нет, это совершенно неверно.",
			"Попробуй ещё раз, твой ответ неправильный.",
			"Увы, ты ошибся.",
			"Это не то, что я ожидала услышать.",
			"Правильный ответ звучит иначе.",
			"Ты в далеке от правильного ответа.",
			"Ты не попал в цель, продолжай искать.",
			"Неправильно, но это хорошая попытка.",
			"Этот ответ не соответствует действительности."};

		public List<string> issueTag = new List<string>
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

		public List<string> phrases = new List<string>
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

		public LiftBot(string type, string name, string token, Geolocation geolocation, string path2Text)
        {
            Type = type;
            Name = name;
            Token = token;
            Coordinates = geolocation;
            Path2Text = path2Text;
        }

        public LiftBot(LiftBot bot)
        {
			Type = bot.Type;
			Name = bot.Name;
			Token = bot.Token;
			Coordinates = bot.Coordinates;
			Path2Text = bot.Path2Text;
		}

        public abstract Task Start();
        public abstract Task Update(ITelegramBotClient botClient, Update update, CancellationToken token);
        public abstract Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token);
        public abstract string ReadBotTextFile();
    }
}