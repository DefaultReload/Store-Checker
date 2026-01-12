using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {
        Console.Write("Enter store name: ");
        string storeName = Console.ReadLine();

        string apiKey = "YOUR_GOOGLE_API_KEY";
        string url = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query={storeName}&key={apiKey}";

        using HttpClient client = new HttpClient();
        string response = await client.GetStringAsync(url);

        JObject json = JObject.Parse(response);
        var results = json["results"];

        if (results == null || results.Count() == 0)
        {
            Console.WriteLine("Store not found");
            return;
        }

        var store = results[0];

        string businessStatus = store["business_status"]?.ToString();
        bool? openNow = store["opening_hours"]?["open_now"]?.ToObject<bool>();

        if (businessStatus == "CLOSED_PERMANENTLY")
        {
            Console.WriteLine("Store is PERMANENTLY CLOSED");
        }
        else if (openNow == true)
        {
            Console.WriteLine("Store is currently OPEN");
        }
        else if (openNow == false)
        {
            Console.WriteLine("Store is currently CLOSED");
        }
        else
        {
            Console.WriteLine("Opening status unavailable");
        }
    }
}
