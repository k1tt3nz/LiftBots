using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	internal class Team
	{
		public long ChatId { get; set; }
		public string TeamName { get; set; }

		public Team(long chatId, string teamName)
		{
			ChatId = chatId;
			TeamName = teamName;
		}
	}
}
