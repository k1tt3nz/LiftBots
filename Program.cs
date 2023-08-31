using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using static ConsoleApp1.BotInQuestion;

namespace ConsoleApp1
{
	internal class Program
	{

		static void Main(string[] args)
		{
			//int[] a = { 1, 2 };
			//List<IProduct> list = new List<IProduct>();
			//foreach (int i in a)
			//{
			//	if(i == 1)
			//	{
			//		IProduct product = new CreatorA().FactoryMethod("asd", 1);
			//		list.Add(product);
			//	}
			//	else
			//	{
			//		IProduct product = new CreatorB().FactoryMethod("qwe", 2);
			//		list.Add(product);
			//	}
			//}

			//foreach (IProduct product in list)
			//{
			//	product.PrintType();
			//}

			//var settings = new JsonSerializerSettings();
			//settings.Converters.Add(new ProductConverter());

			//IProduct product1 = new CreatorA().FactoryMethod("asd", 1);
			//IProduct product2 = new CreatorB().FactoryMethod("asd", 2);
			//List<IProduct> list = new List<IProduct>
			//{
			//	product1,
			//	product2
			//};
			
			//string jsonStr = JsonConvert.SerializeObject(list);
   //         Console.WriteLine(jsonStr);

			//List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonStr,settings);
   //         foreach(IProduct product in products)
			//{
			//	product.PrintType();
			//}


            //Console.WriteLine(products);

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

			for(int i = 0; i < bots.Length; i++)
			{
				await bots[i].Start();
			}
        }
	}
}