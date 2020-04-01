using Newtonsoft.Json;

namespace CosmosExample.Models
{
	public class Book
	{
		public string Isbn { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Publisher { get; set; }
	}
}
