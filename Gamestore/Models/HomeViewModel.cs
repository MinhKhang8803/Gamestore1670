using Gamestore.Areas.Admin.Models;
namespace Gamestore.Models
{
	public class HomeViewModel
	{
		public List<Game>? NewGame { get; set; }

		public List<Game>? HotGame { get; set; }
    }
}

