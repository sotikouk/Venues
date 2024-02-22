using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Venues.Model;

public class VenuePlaces
{
    public async static Task<List<Result>> GetPois(double latitude, double longitude)
    {
        //using System.Net.Http.Headers;
        List<Result> poiPins = new List<Result>();
        string uri = "https://api.foursquare.com/v3/places/search?ll={0}%2C{1}";
        string url = string.Format(uri, latitude, longitude);
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Add("Accept", "application/json");
        request.Headers.TryAddWithoutValidation("Authorization", Constants.fourSqrKey);

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
            var venueRoot = JsonConvert.DeserializeObject<Poi>(body);
            poiPins = venueRoot.results as List<Result>;
        }

        return poiPins;
    }
}