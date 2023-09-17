namespace ConsoleApp1.libs.LiftBots
{
	public partial class BotInQuestion
	{
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