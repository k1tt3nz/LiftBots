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
			await Console.Out.WriteLineAsync(16 + ") " + botsQ[0].Name + " запущен");

			AlexeiBelan alexeiBelan = new AlexeiBelan(botsQ[1]);
			await alexeiBelan.Start();
			await Console.Out.WriteLineAsync(17 + ") " + botsQ[1].Name + " запущен");

			VladimirGorskov vladimir = new VladimirGorskov(botsQ[2]);
			await vladimir.Start();
			await Console.Out.WriteLineAsync(18 + ") " + botsQ[2].Name + " запущен");

			SheldonGoldberg sheldon = new SheldonGoldberg(botsQ[3]);
			await sheldon.Start();
			await Console.Out.WriteLineAsync(19 + ") " + botsQ[3].Name + " запущен");

			ElonMusk elonMusk = new ElonMusk(botsQ[4]);
			await elonMusk.Start();
			await Console.Out.WriteLineAsync(20 + ") " + botsQ[4].Name + " запущен");

			SoyPorridge soyPorridge = new SoyPorridge(botsQ[5]);
			await soyPorridge.Start();
			await Console.Out.WriteLineAsync(21 + ") " + botsQ[5].Name + " запущен");

			AlbertHughes albertHughes = new AlbertHughes(botsQ[6]);
			await albertHughes.Start();
			await Console.Out.WriteLineAsync(22 + ") " + botsQ[6].Name + " запущен");

			Robotus robotus = new Robotus(botsQ[7]);
			await robotus.Start();
			await Console.Out.WriteLineAsync(23 + ") " + botsQ[7].Name + " запущен");
		}
	}
}