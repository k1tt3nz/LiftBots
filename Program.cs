using ConsoleApp1.libs.LiftBots;
using ConsoleApp1.libs.LiftBots.MysteryBots;
using ConsoleApp1.libs.LiftBots.MysteryBots.GigiThompsonBOT;
using Newtonsoft.Json;
using static ConsoleApp1.libs.LiftBots.BotInQuestion;

namespace ConsoleApp1
{
    internal class Program
	{

		static void Main(string[] args)
		{

			StartBots();
			//test();

            Console.ReadKey();

        }

		static void A()
		{
		Console.ReadKey();
		}

		static void test()
		{
			var coordinates = new Geolocation(50.58048505266357, 36.596409887320334);
			var token = "1014588195:AAEsv_pDfDzMV6oxuwXwsZU-KfvWw_LZyHk";
			var path = "F:\\Programming\\VS repos\\ConsoleApp1\\txt\\text.txt";
			LiftBot bot = new BotNoQuestionCreator().Create("NoQuestion", "Бот1", token, coordinates, path);
			LiftBot bot2 = new BotInQuestionCreator().Create("NoQuestion", "Бот1", "5524438661:AAEwwSezD9evLXJ9EN8pNkP9U_9pr4amOVU", coordinates, path);
			//bot.Start();
			LiftBot[] bots = { bot, bot2 };
			var jsonStr = JsonConvert.SerializeObject(bots);
            Console.WriteLine(jsonStr);
            Console.ReadLine();
		}

		static async void StartBots()
		{
			string pathJson = "F:\\Programming\\VS repos\\ConsoleApp1\\bots.json";

			string text;
			using (StreamReader reader = new StreamReader(pathJson))
			{
				text = reader.ReadToEnd();
			}


			var settings = new JsonSerializerSettings();
			settings.Converters.Add(new LiftBotConverter());

			LiftBot[] bots = JsonConvert.DeserializeObject<LiftBot[]>(text,settings);
			Console.ForegroundColor = ConsoleColor.Green;
			for (int i = 0; i < bots.Length; i++)
			{
				await bots[i].Start();
                await Console.Out.WriteLineAsync(i+1 + ") " + bots[i].Name + " запущен");
            }

			pathJson = "F:\\Programming\\VS repos\\ConsoleApp1\\libs\\LiftBots\\MysteryBots\\InQuestion.json";
			using (StreamReader reader = new StreamReader(pathJson))
			{
				text = reader.ReadToEnd();
			}

			LiftBot[] botsQ = JsonConvert.DeserializeObject<LiftBot[]>(text, settings);
			GigiThompsonBOT gigiThompsonBOT = new GigiThompsonBOT(botsQ[0]);
			await gigiThompsonBOT.Start();
			await Console.Out.WriteLineAsync(") " + botsQ[0].Name + " запущен");

			AlexeiBelan alexeiBelan = new AlexeiBelan(botsQ[1]);
			await alexeiBelan.Start();
			await Console.Out.WriteLineAsync(") " + botsQ[1].Name + " запущен");
		}
	}
}