using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using static ConsoleApp1.BotInQuestion;

namespace ConsoleApp1
{
	public class LiftBotConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(LiftBot).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jsonObject = JObject.Load(reader);
			string type = jsonObject["Type"].ToString();
			string name = jsonObject["Name"].ToString();
			string token = jsonObject["Token"].ToString();

			Geolocation geolocation = jsonObject["Coordinates"].ToObject<Geolocation>();
			string path2Text = jsonObject["Path2Text"].ToString();
			if (type == "NoQuestion")
			{
				return new BotNoQuestionCreator().Create(type,name,token,geolocation,path2Text);
			}
			else if (type == "InQuestion")
			{
				return new BotInQuestionCreator().Create(type, name, token, geolocation, path2Text);
			}
			throw new JsonSerializationException("Invalid product type");
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			LiftBot bot = (LiftBot)value;
			JObject jsonObject = new JObject
			{
				{ "Type", bot.Type },
				{ "Name", bot.Name },
				{ "Token", bot.Token },
				{ "Path2Text", bot.Path2Text }
			};
			serializer.Serialize(writer, bot.Coordinates);
			jsonObject.WriteTo(writer);
		}
	}
	public abstract class LiftBot
	{
		public string Type { get; set; }
		public string Name { get; set; }
		public string Token { get; set; }
		public Geolocation Coordinates { get; set; }
		public string Path2Text { get; set; }

		public LiftBot(string type, string name, string token, Geolocation geolocation, string path2Text)
		{
			Type = type;
			Name = name;
			Token = token;
			Coordinates = geolocation;
			Path2Text = path2Text;
		}

		public abstract Task Start();
		public abstract Task Update(ITelegramBotClient botClient, Update update, CancellationToken token);
		public abstract Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token);
		public abstract string ReadBotTextFile();
	}
	internal class BotNoQuestion : LiftBot
	{
		public BotNoQuestion(string type ,string name, string token, Geolocation geolocation, string path2Text) : base(type,name, token, geolocation, path2Text) { }

		public override async Task Start()
		{
			TelegramBotClient Client = new TelegramBotClient(Token);
			Client.StartReceiving(Update, Error);
		}

		public override async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
		{
			var userMsg = update.Message;
			var chatID = update.Message.Chat.Id;
			if (userMsg != null)
			{
				await botClient.SendTextMessageAsync(chatID, ReadBotTextFile());
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude,Coordinates.Longitude);
			}
		} 

		public override string ReadBotTextFile()
		{
			using(StreamReader reader = new StreamReader(Path2Text))
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

	public class BotInQuestion : LiftBot
	{
		public BotInQuestion(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }

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

		public override Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		public abstract class BotCreator
		{
			public abstract LiftBot Create(string type, string name, string token, Geolocation geolocation, string path2Text);
		}

		public class BotNoQuestionCreator : BotCreator
		{
			public override LiftBot Create(string type, string name, string token, Geolocation geolocation, string path2Text)
			{
				return new BotNoQuestion(type, name, token, geolocation, path2Text);
			}
		}

		public class BotInQuestionCreator : BotCreator
		{
			public override LiftBot Create(string type, string name, string token, Geolocation geolocation, string path2Text)
			{
				return new BotInQuestion(type, name, token, geolocation, path2Text);
			}
		}
	}
}