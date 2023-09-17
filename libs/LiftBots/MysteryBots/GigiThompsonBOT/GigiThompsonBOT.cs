using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleApp1.libs.LiftBots.MysteryBots.GigiThompsonBOT
{
    internal class GigiThompsonBOT : LiftBot
    {

        public GigiThompsonBOT(string type, string name, string token, Geolocation geolocation, string path2Text) : base(type, name, token, geolocation, path2Text) { }
        public GigiThompsonBOT(LiftBot bot) : base(bot) { }

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

        public override async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var userMsg = update.Message;
            var chatID = update.Message.Chat.Id;
            string botMsg = "Короче," + update.Message.Chat.FirstName + ", я тебя спас и в благородство играть не буду: отгадаешь для меня пару знаменитостей — " +
                "и мы в расчете. Заодно посмотрим, как быстро у тебя башка после дистанционного обучения прояснится. " +
                "А по твоей теме постараюсь разузнать. Хрен его знает, на кой ляд тебе эта Джиджи Томпсон сдался, но я в чужие дела не лезу, хочешь квестики выполнять, твои дела...";

			if (userMsg.Text.Contains("start"))
            {
                await botClient.SendTextMessageAsync(chatID, botMsg);
                await botClient.SendPhotoAsync(
                    chatId: chatID,
                    photo: InputFile.FromUri("https://ya.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fcdn.nur.kz%2Fimages%2F1120%2Fpogudx2u6fbe1b3pv.jpeg&lr=4&pos=0&rpt=simage&text=%D0%B1%D1%83%D0%B7%D0%BE%D0%B2%D0%B0"),
                    parseMode: ParseMode.Html
				);
            }

            if (userMsg.Text.ToLower().Contains("бузова"))
            {
				await botClient.SendTextMessageAsync(chatID, "Ого, молодец, давай дальше");
				await botClient.SendPhotoAsync(
	                chatId: chatID,
	                photo: InputFile.FromUri("https://ya.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fwww.topnews.ru%2Fwp-content%2Fuploads%2F2022%2F03%2F7ea352407fcd3d67446dbce9f8652208.jpg&lr=4&pos=15&rpt=simage&text=%D0%BC%D0%B8%D0%BB%D0%BE%D1%85%D0%B8%D0%BD"),
	                parseMode: ParseMode.Html
                );
			}
			if (userMsg.Text.ToLower().Contains("милохин"))
			{
				await botClient.SendTextMessageAsync(chatID, "Снова в точку, следующий");
				await botClient.SendPhotoAsync(
					chatId: chatID,
					photo: InputFile.FromUri("https://ya.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fscontent-hel3-1.cdninstagram.com%2Fv%2Ft51.2885-15%2Fe35%2F271827756_313000127424138_5308672514375648762_n.webp.jpg%3F_nc_ht%3Dscontent-hel3-1.cdninstagram.com%26_nc_cat%3D106%26_nc_ohc%3D_F4cJcPE-VkAX_zPJyi%26edm%3DAABBvjUBAAAA%26ccb%3D7-4%26oh%3D00_AT84KkqSnll0x_Xj3bE8eoPuBpE_YmIjCAGYzzx2hPInEQ%26oe%3D61E79540%26_nc_sid%3D83d603&lr=4&pos=1&rpt=simage&text=%D0%B2%D0%BB%D0%B0%D0%B4%20%D0%B04"),
					parseMode: ParseMode.Html
				);
			}
			if (userMsg.Text.ToLower().Contains("а4") || userMsg.Text.ToLower().Contains("влад а4") || userMsg.Text.ToLower().Contains("a4"))
			{
				await botClient.SendTextMessageAsync(chatID, "Ты реально их знаешь?...");
				await botClient.SendPhotoAsync(
					chatId: chatID,
					photo: InputFile.FromUri("https://ya.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fsun1-25.userapi.com%2Fimpg%2Fnx9_Nk78Ezw3EIaBK3dT3Qdb0Z4cNfKsuaQhFw%2FFT5yFg4OP5c.jpg%3Fsize%3D1080x1080%26quality%3D96%26sign%3D04b92ed39427a025d7f17e55b3ba7075%26c_uniq_tag%3DxgT8leULBur69Joa7ILD4QCx5yq58XQcQ1JnsgXnAd0%26type%3Dalbum&lr=4&pos=34&rpt=simage&text=%D0%B4%D0%B0%D1%88%D0%B0%20%D0%BA%D0%BE%D1%80%D0%B5%D0%B9%D0%BA%D0%B0%20"),
					parseMode: ParseMode.Html
				);
			}

			if (userMsg.Text.ToLower().Contains("корейка") || userMsg.Text.ToLower().Contains("даша корейка"))
			{
				await botClient.SendTextMessageAsync(chatID, "Чем ты вообще в жизни занимаешься?... Сходи лучше книжку какую-нибудь умную почитай....");
				await botClient.SendTextMessageAsync(chatID, "Ладно... Как и обещала вот твоя метка к квесту.Удачи.");
				await botClient.SendLocationAsync(chatID, Coordinates.Latitude, Coordinates.Longitude);
			}


			if (userMsg.Text != string.Empty && userMsg.Text != "/start")
			{
				Random random = new Random();
				await botClient.SendTextMessageAsync(chatID, ifWrongAnswer[random.Next(9)].ToString());
			}
		}
    }
}
