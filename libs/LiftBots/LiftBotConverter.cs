using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static ConsoleApp1.libs.LiftBots.BotInQuestion;

namespace ConsoleApp1.libs.LiftBots
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
                return new BotNoQuestionCreator().Create(type, name, token, geolocation, path2Text);
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
}