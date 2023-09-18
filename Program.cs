using ConsoleApp1.libs.LiftBots;
using ConsoleApp1.libs.LiftBots.MysteryBots;
using ConsoleApp1.libs.LiftBots.MysteryBots.GigiThompsonBOT;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1
{
    internal class Program
	{
		private static readonly string BotJsonPath = "F:\\Programming\\VS repos\\ConsoleApp1\\bots.json";
		private static readonly string MysteryBotsJsonPath = "F:\\Programming\\VS repos\\ConsoleApp1\\libs\\LiftBots\\MysteryBots\\InQuestion.json";

		private static async Task Main(string[] args)
		{
			StartBots();
			while (true)
			{ }
		}

		private static async void StartBots()
		{

			var settings = new JsonSerializerSettings();
			settings.Converters.Add(new LiftBotConverter());

			string botJson;
			using (StreamReader reader = new StreamReader(BotJsonPath))
			{
				botJson = reader.ReadToEnd();
			}


			LiftBot[] bots = JsonConvert.DeserializeObject<LiftBot[]>(botJson,settings);
			Console.ForegroundColor = ConsoleColor.Green;
			for (int i = 0; i < bots.Length; i++)
			{
				await bots[i].Start();
				await Console.Out.WriteLineAsync($"{i + 1}) {bots[i].Name} запущен");
			}

			string mysteryBotsJson;
			using (StreamReader reader = new StreamReader(MysteryBotsJsonPath))
			{
				mysteryBotsJson = reader.ReadToEnd();
			}


			LiftBot[] botsQ = JsonConvert.DeserializeObject<LiftBot[]>(mysteryBotsJson, settings);

			var botCreators = new Func<LiftBot, Task>[]
			{
				bot => new GigiThompsonBOT(bot).Start(),
				bot => new AlexeiBelan(bot).Start(),
				bot => new VladimirGorskov(bot).Start(),
				bot => new SheldonGoldberg(bot).Start(),
				bot => new ElonMusk(bot).Start(),
				bot => new SoyPorridge(bot).Start(),
				bot => new AlbertHughes(bot).Start(),
				bot => new Robotus(bot).Start()
			};

			for (int i = 0; i < botCreators.Length; i++)
			{
				await botCreators[i](botsQ[i]);
				await Console.Out.WriteLineAsync($"{i + 16}) {botsQ[i].Name} запущен");
			}





			//LiftBot[] botsQuest = JsonConvert.DeserializeObject<LiftBot[]>(mysteryBotsJson, settings);

			//GigiThompsonBOT gigiThompsonBOT = new GigiThompsonBOT(botsQuest[0]);
			//await gigiThompsonBOT.Start();
			//await Console.Out.WriteLineAsync(16 + ") " + botsQuest[0].Name + " запущен");

			//AlexeiBelan alexeiBelan = new AlexeiBelan(botsQuest[1]);
			//await alexeiBelan.Start();
			//await Console.Out.WriteLineAsync(17 + ") " + botsQuest[1].Name + " запущен");

			//VladimirGorskov vladimir = new VladimirGorskov(botsQuest[2]);
			//await vladimir.Start();
			//await Console.Out.WriteLineAsync(18 + ") " + botsQuest[2].Name + " запущен");

			//SheldonGoldberg sheldon = new SheldonGoldberg(botsQuest[3]);
			//await sheldon.Start();
			//await Console.Out.WriteLineAsync(19 + ") " + botsQuest[3].Name + " запущен");

			//ElonMusk elonMusk = new ElonMusk(botsQuest[4]);
			//await elonMusk.Start();
			//await Console.Out.WriteLineAsync(20 + ") " + botsQuest[4].Name + " запущен");

			//SoyPorridge soyPorridge = new SoyPorridge(botsQuest[5]);
			//await soyPorridge.Start();
			//await Console.Out.WriteLineAsync(21 + ") " + botsQuest[5].Name + " запущен");

			//AlbertHughes albertHughes = new AlbertHughes(botsQuest[6]);
			//await albertHughes.Start();
			//await Console.Out.WriteLineAsync(22 + ") " + botsQuest[6].Name + " запущен");

			//Robotus robotus = new Robotus(botsQuest[7]);
			//await robotus.Start();
			//await Console.Out.WriteLineAsync(23 + ") " + botsQuest[7].Name + " запущен");

		}
	}
}